﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Presentacion
{
    public partial class frmAcciones : Form, ISeleccionarChofer
    {
        public frmAcciones()
        {
            InitializeComponent();
        }

        public string miid;
        public string miNombre;

        public void RecuperarId(string id, string name)
        {
            miid = id;
            miNombre = name;
        }

       

        private void frmAcciones_Load(object sender, EventArgs e)
        {
           
        }

       
        private void CargarTicketsParticular()
        {
            if(txtValorDeBase.Text != string.Empty && cbMes.Text != string.Empty && cbAno.Text != string.Empty)
            {
                bool[] SemanasSeleccionadas = new bool[4];
                SemanasSeleccionadas[0] = chk1.Checked;
                SemanasSeleccionadas[1] = chk2.Checked;
                SemanasSeleccionadas[2] = chk3.Checked;
                SemanasSeleccionadas[3] = chk4.Checked;

                var deuda = new Entidades.Deuda();
                int nroModificaciones = 0;
                for (int i = 0; i < 4; i++)
                {
                    if (SemanasSeleccionadas[i] == false)
                        continue;
                    deuda.IdChofer = Convert.ToInt32(miid);
                    deuda.IdPago = null;
                    deuda.Monto = Convert.ToDouble(txtValorDeBase.Text);
                    int semana = i + 1;
                    //DateTime dt = 
                    deuda.Fecha = new DateTime(Convert.ToInt32(cbAno.Text), Convert.ToInt32(cbMes.Text), semana);

                    new Negocio.NDeuda().Insertar(deuda);
                    nroModificaciones++;
                }
                if (nroModificaciones > 0)
                    MessageBox.Show("Se Generaron " + nroModificaciones + " Tickets Correctamente");
                else
                    MessageBox.Show("No Hubo Modificaciones. Compruebe los Datos!");

            }
        }

        private void btnGenTickParticular_Click(object sender, EventArgs e)
        {
            CargarTicketsParticular();
        }

        private void txtValorDeBase_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            else if (Char.IsControl(e.KeyChar))
            {
                e.Handled = false;
            }
            else if (Char.IsSeparator(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }

            


        }

        private void btnSelChofer_Click(object sender, EventArgs e)
        {
            frmSeleccionarChofer frm = new frmSeleccionarChofer(this);
            frm.ShowDialog();
            lbl_nombre.Text = miNombre;
        }
    }
}