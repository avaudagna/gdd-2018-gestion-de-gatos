﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PalcoNet.Repositorios;
using PalcoNet.Modelo;
using MaterialSkin.Controls;
using MaterialSkin;
using System.Text.RegularExpressions;
using PalcoNet.AbmDomicilio;

namespace PalcoNet.AbmEmpresa
{
    public partial class ModificacionEmpresa : MaterialForm
    {
        Empresa empresa;
        public ModificacionEmpresa(Empresa empresa)
        {
            InitializeComponent();
            this.empresa = empresa;

            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
            materialSkinManager.ColorScheme = new ColorScheme(Primary.BlueGrey800, Primary.BlueGrey900, Primary.BlueGrey500, Accent.LightBlue200, TextShade.WHITE);
            mostrarDatosEmpresa();
        }

        public void mostrarDatosEmpresa()
        {
            txtRazon.Text = empresa.RazonSocial;
            txtCuit.Text = empresa.Cuit;
            txtMail.Text = empresa.Email;
            txtTel.Text = empresa.Telefono;
            checkDeshabilitada.Checked = !empresa.Habilitada;
        }

        public void actualizarInstanciaEmpresa()
        {
            empresa.RazonSocial = txtRazon.Text;
            empresa.Cuit = txtCuit.Text;
            empresa.Email = txtMail.Text;
            empresa.Telefono = txtTel.Text;
            empresa.Habilitada = !checkDeshabilitada.Checked;
        }

        private bool verificaValidaciones()
        {
            errorProvider1.Clear();
            return formularioCompleto();
        }

         private void limpiarVentana()
        {
            var textboxes = grupo_empresa.Controls.OfType<TextBox>();
            foreach (TextBox textbox in textboxes)
            {
                textbox.Clear();
            }
            var comboboxes = grupo_empresa.Controls.OfType<ComboBox>();
            foreach (ComboBox combo_box in comboboxes)
            {
                combo_box.ResetText();
            }
            

        }
         private bool formularioCompleto()
         {
             bool error = true;

             var controles = grupo_empresa.Controls;
             controles.Remove(controles.Find("checkDeshabilitada", false)[0]);
             foreach (Control control in controles)
             {
                 if (string.IsNullOrWhiteSpace(control.Text))
                 {
                     errorProvider1.SetError(control, "Por favor complete el campo");
                     error = false;
                 }
             }
             return error;
         }

        private void button1_Click(object sender, EventArgs e)
         {
            grupo_empresa.Enabled = false;
            if (!verificaValidaciones())
            {
                grupo_empresa.Enabled = true;
                return;
            }
            try
            {
                actualizarInstanciaEmpresa();
                EmpresasRepositorio.actualizar(empresa);
                MessageBox.Show("La empresa ha sido modificada exitosamente", "Modificacion de empresa", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            grupo_empresa.Enabled = true;
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void btn_modificar_domicilio_Click(object sender, EventArgs e)
        {
            ModificarDomicilio modificarDomicilio = new ModificarDomicilio(empresa.Domicilio);
            modificarDomicilio.ShowDialog();
        }
    }
}
