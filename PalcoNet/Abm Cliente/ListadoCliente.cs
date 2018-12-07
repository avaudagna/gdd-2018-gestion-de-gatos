﻿using MaterialSkin;
using MaterialSkin.Controls;
using PalcoNet.Modelo;
using PalcoNet.Repositorios;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PalcoNet.AbmCliente
{
    public partial class ListadoCliente : MaterialForm
    {
        DataTable tabla_clientes = new DataTable();
        public ListadoCliente(Char modo)
        {
            InitializeComponent();

            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
            materialSkinManager.ColorScheme = new ColorScheme(Primary.BlueGrey800, Primary.BlueGrey900, Primary.BlueGrey500, Accent.LightBlue200, TextShade.WHITE);

            

            initColumns();


            data_clientes.Columns[1].Name = "Razon social";
            data_clientes.Columns[2].Name = "Telefono";
            data_clientes.Columns[3].Name = "Email";

            tabla_clientes.Columns.Add("DNI", typeof(string));
            tabla_clientes.Columns.Add("Nombre", typeof(string));
            tabla_clientes.Columns.Add("Apellido", typeof(string));
            tabla_clientes.Columns.Add("Habilitado", typeof(string));

            if (modo == 'B')
                btn_modificar.Hide();
            else
                switch_habilitacion.Hide();
        }

        private void initColumns()
        {
            DataGridViewColumn colTipoDoc = new DataGridViewColumn();
            colTipoDoc.HeaderText = "Tipo doc";
            data_clientes.Columns.Add(colTipoDoc);
            DataGridViewColumn colDoc = new DataGridViewColumn();
            colDoc.HeaderText = "Documento";
            data_clientes.Columns.Add(colDoc);
            DataGridViewColumn colCuil = new DataGridViewColumn();
            colCuil.HeaderText = "Cuil";
            data_clientes.Columns.Add(colCuil);
            DataGridViewColumn colNombre = new DataGridViewColumn();
            colNombre.HeaderText = "Nombre";
            data_clientes.Columns.Add(colNombre);
            DataGridViewColumn colApellido = new DataGridViewColumn();
            colApellido.HeaderText = "Apellido";
            data_clientes.Columns.Add(colApellido);
            DataGridViewColumn colEmail = new DataGridViewColumn();
            colEmail.HeaderText = "Email";
            data_clientes.Columns.Add(colEmail);
            DataGridViewColumn colCalle = new DataGridViewColumn();
            colCalle.HeaderText = "Calle";
            data_clientes.Columns.Add(colCalle);
            DataGridViewColumn colNumero = new DataGridViewColumn();
            colNumero.HeaderText = "Numero";
            data_clientes.Columns.Add(colNumero);
            DataGridViewColumn colCiudad = new DataGridViewColumn();
            colCiudad.HeaderText = "Localidad";
            data_clientes.Columns.Add(colCiudad);
            DataGridViewColumn colCP = new DataGridViewColumn();
            colCP.HeaderText = "Coidigo postal";
            data_clientes.Columns.Add(colCP);
        }

        private void btn_buscar_Click(object sender, EventArgs e)
        {
            if (!Regex.IsMatch(tx_dni.Text, @"^[0-9]{1,8}$") && !string.IsNullOrEmpty(tx_dni.Text))
            {
                MessageBox.Show("Ingrese un DNI válido.");
                return;
            }
            if (!Regex.IsMatch(tx_nombre.Text, @"^[a-zA-Z\s]{1,30}$") && !string.IsNullOrEmpty(tx_nombre.Text))
            {
                MessageBox.Show("Ingrese un nombre válido.");
                return;
            }
            if (!Regex.IsMatch(tx_apellido.Text, @"^[a-zA-Z\s]{1,30}$") && !string.IsNullOrEmpty(tx_apellido.Text))
            {
                MessageBox.Show("Ingrese un apellido válido.");
                return;
            }
            tabla_clientes.Clear();
            List<Cliente> clientes = ClienteRepositorio.getClientes(tx_dni.Text, tx_nombre.Text, tx_apellido.Text);
            foreach (Cliente cliente in clientes)
            {
                String hab = cliente.Habilitado ? "Si" : "No";
                String[] row = new String[] { cliente.NumeroDocumento.ToString(), cliente.nombre, cliente.Apellido, hab };
                tabla_clientes.Rows.Add(row);
            }
            refreshValues();
            if (this.data_clientes.RowCount > 0)
            {
                DataGridViewRow r = this.data_clientes.SelectedRows[0];
                if (r.Cells["Habilitado"].Value.ToString() == "No")
                {
                    switch_habilitacion.Text = "Habilitar";
                }
                else
                {
                    switch_habilitacion.Text = "Inhabilitar";
                }
            }

        }
        public void refreshValues()
        {
            data_clientes.DataSource = tabla_clientes;
        }


        private void grupo_filtros_Enter(object sender, EventArgs e)
        {

        }

        private void data_clientes_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (data_clientes.SelectedRows.Count != 0)
            {
                DataGridViewRow row = this.data_clientes.SelectedRows[0];
                if (row.Cells["Habilitado"].Value.ToString() == "No")
                {
                    switch_habilitacion.Text="Habilitar";
                }
                else
                {
                    switch_habilitacion.Text="Inhabilitar";
                }
            }
        }

        private void btn_limpiar_Click(object sender, EventArgs e)
        {
            tabla_clientes.Rows.Clear();
            refreshValues();
        }

        private void btn_atras_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void btn_modificar_Click(object sender, EventArgs e)
        {
            if (this.data_clientes.RowCount > 0)
            {
                Int32 dni = Convert.ToInt32(this.data_clientes.SelectedRows[0].Cells["DNI"].Value.ToString());
                this.Hide();
                new ModificacionCliente(dni).Show();
            }
            else
                MessageBox.Show("Busque y seleccione un cliente antes.");
        }

        private void switch_habilitacion_Click(object sender, EventArgs e)
        {
            if (this.data_clientes.RowCount > 0)
            {
                if (this.data_clientes.SelectedRows[0].Cells["Habilitado"].Value.ToString() == "Si")
                {
                    ClienteRepositorio.eliminarCliente(Convert.ToInt32(this.data_clientes.SelectedRows[0].Cells["DNI"].Value.ToString()));
                }
                else
                {
                    ClienteRepositorio.habilitarCliente(Convert.ToInt32(this.data_clientes.SelectedRows[0].Cells["DNI"].Value.ToString()));
                }
                object s = new object();
                EventArgs ea = new EventArgs();
                btn_buscar_Click(s, ea);
            }
            else
            {
                MessageBox.Show("Busque y seleccione un cliente antes.");
            }
        }

        private void data_clientes_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void ListadoCliente_Load(object sender, EventArgs e)
        {

        }

        

    }
}
