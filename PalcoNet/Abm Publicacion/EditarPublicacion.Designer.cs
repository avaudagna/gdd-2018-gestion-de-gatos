﻿namespace PalcoNet.AbmPublicaciones
{
    partial class EditarPublicacion
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btn_cambiar_estado = new System.Windows.Forms.Button();
            this.btn_editar = new System.Windows.Forms.Button();
            this.btn_atras = new System.Windows.Forms.Button();
            this.btn_limpiar = new System.Windows.Forms.Button();
            this.grupo_filtros = new System.Windows.Forms.GroupBox();
            this.txt_razon_social = new System.Windows.Forms.TextBox();
            this.btn_buscar = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.data_empresas = new System.Windows.Forms.DataGridView();
            this.grupo_filtros.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.data_empresas)).BeginInit();
            this.SuspendLayout();
            // 
            // btn_cambiar_estado
            // 
            this.btn_cambiar_estado.Enabled = false;
            this.btn_cambiar_estado.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_cambiar_estado.Location = new System.Drawing.Point(335, 421);
            this.btn_cambiar_estado.Margin = new System.Windows.Forms.Padding(2);
            this.btn_cambiar_estado.Name = "btn_cambiar_estado";
            this.btn_cambiar_estado.Size = new System.Drawing.Size(157, 28);
            this.btn_cambiar_estado.TabIndex = 20;
            this.btn_cambiar_estado.Text = "Deshabilitar/Habilitar";
            this.btn_cambiar_estado.UseVisualStyleBackColor = true;
            // 
            // btn_editar
            // 
            this.btn_editar.Enabled = false;
            this.btn_editar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_editar.Location = new System.Drawing.Point(211, 421);
            this.btn_editar.Margin = new System.Windows.Forms.Padding(2);
            this.btn_editar.Name = "btn_editar";
            this.btn_editar.Size = new System.Drawing.Size(74, 28);
            this.btn_editar.TabIndex = 19;
            this.btn_editar.Text = "Editar";
            this.btn_editar.UseVisualStyleBackColor = true;
            this.btn_editar.Click += new System.EventHandler(this.btn_editar_Click);
            // 
            // btn_atras
            // 
            this.btn_atras.Location = new System.Drawing.Point(12, 421);
            this.btn_atras.Margin = new System.Windows.Forms.Padding(2);
            this.btn_atras.Name = "btn_atras";
            this.btn_atras.Size = new System.Drawing.Size(74, 28);
            this.btn_atras.TabIndex = 18;
            this.btn_atras.Text = "Atras";
            this.btn_atras.UseVisualStyleBackColor = true;
            // 
            // btn_limpiar
            // 
            this.btn_limpiar.Location = new System.Drawing.Point(562, 421);
            this.btn_limpiar.Margin = new System.Windows.Forms.Padding(2);
            this.btn_limpiar.Name = "btn_limpiar";
            this.btn_limpiar.Size = new System.Drawing.Size(74, 28);
            this.btn_limpiar.TabIndex = 17;
            this.btn_limpiar.Text = "Limpiar";
            this.btn_limpiar.UseVisualStyleBackColor = true;
            // 
            // grupo_filtros
            // 
            this.grupo_filtros.BackColor = System.Drawing.Color.White;
            this.grupo_filtros.Controls.Add(this.txt_razon_social);
            this.grupo_filtros.Controls.Add(this.btn_buscar);
            this.grupo_filtros.Controls.Add(this.label1);
            this.grupo_filtros.Location = new System.Drawing.Point(11, 74);
            this.grupo_filtros.Margin = new System.Windows.Forms.Padding(2);
            this.grupo_filtros.Name = "grupo_filtros";
            this.grupo_filtros.Padding = new System.Windows.Forms.Padding(2);
            this.grupo_filtros.Size = new System.Drawing.Size(623, 71);
            this.grupo_filtros.TabIndex = 16;
            this.grupo_filtros.TabStop = false;
            this.grupo_filtros.Text = "Filtros de Busqueda";
            // 
            // txt_razon_social
            // 
            this.txt_razon_social.Location = new System.Drawing.Point(64, 31);
            this.txt_razon_social.Margin = new System.Windows.Forms.Padding(2);
            this.txt_razon_social.Name = "txt_razon_social";
            this.txt_razon_social.Size = new System.Drawing.Size(328, 20);
            this.txt_razon_social.TabIndex = 15;
            // 
            // btn_buscar
            // 
            this.btn_buscar.Location = new System.Drawing.Point(478, 23);
            this.btn_buscar.Margin = new System.Windows.Forms.Padding(2);
            this.btn_buscar.Name = "btn_buscar";
            this.btn_buscar.Size = new System.Drawing.Size(74, 34);
            this.btn_buscar.TabIndex = 14;
            this.btn_buscar.Text = "Buscar";
            this.btn_buscar.UseVisualStyleBackColor = true;
            this.btn_buscar.Click += new System.EventHandler(this.btn_buscar_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(27, 34);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(33, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Titulo";
            // 
            // data_empresas
            // 
            this.data_empresas.BackgroundColor = System.Drawing.SystemColors.ActiveCaption;
            this.data_empresas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.data_empresas.Location = new System.Drawing.Point(12, 158);
            this.data_empresas.Margin = new System.Windows.Forms.Padding(2);
            this.data_empresas.Name = "data_empresas";
            this.data_empresas.ReadOnly = true;
            this.data_empresas.RowTemplate.Height = 24;
            this.data_empresas.Size = new System.Drawing.Size(626, 250);
            this.data_empresas.TabIndex = 15;
            // 
            // EditarPublicacion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(645, 480);
            this.Controls.Add(this.btn_cambiar_estado);
            this.Controls.Add(this.btn_editar);
            this.Controls.Add(this.btn_atras);
            this.Controls.Add(this.btn_limpiar);
            this.Controls.Add(this.grupo_filtros);
            this.Controls.Add(this.data_empresas);
            this.Name = "EditarPublicacion";
            this.Text = "Editar Publicacion";
            this.grupo_filtros.ResumeLayout(false);
            this.grupo_filtros.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.data_empresas)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_cambiar_estado;
        private System.Windows.Forms.Button btn_editar;
        private System.Windows.Forms.Button btn_atras;
        private System.Windows.Forms.Button btn_limpiar;
        private System.Windows.Forms.GroupBox grupo_filtros;
        private System.Windows.Forms.TextBox txt_razon_social;
        private System.Windows.Forms.Button btn_buscar;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView data_empresas;
    }
}