using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using CapaNegocios;

namespace CapaPresentacion
{
    public partial class frmPresentacion : Form
    {
        private bool isNuevo = false;
        private bool isEditar = false;

        public frmPresentacion()
        {
            InitializeComponent();
            this.ttMenaje.SetToolTip(this.txtNombre, "ingrese nombre de presentacin");

        }
        private void MensajeOk(string mensaje)
        {
            MessageBox.Show(mensaje, "systema de ventas", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private void MensajeError(string mensaje)
        {
            MessageBox.Show(mensaje, "sistema de ventas", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }
        private void Limpiar()
        {
            this.txtNombre.Text = string.Empty;
            this.txtDescripcion.Text = string.Empty;
            this.txtIdPresentacion.Text = string.Empty;
        }

        private void Habilitar(bool valor)
        {
            this.txtNombre.ReadOnly = !valor;
            this.txtDescripcion.ReadOnly =! valor;
            this.txtIdPresentacion.ReadOnly = !valor;
        }

        private void Botones()
        {
            if (this.isNuevo || isEditar)
            {
                this.Habilitar(true);
                this.btnNuevo.Enabled = false;
                this.btnGuardar.Enabled = true;
                this.btnEditar.Enabled = false;
                this.btnCancelar.Enabled = true;

            }
            else
            {
                this.Habilitar(false);
                this.btnNuevo.Enabled = true;
                this.btnGuardar.Enabled = false;
                this.btnEditar.Enabled = true;
                this.btnCancelar.Enabled = false;
            }

        }
        private void OcultarColumnas()
        {
            this.dataListado.Columns[0].Visible = false;
          // this.dataListado.Columns[1].Visible = false;
        }
        private void Mostrar()
        {
            this.dataListado.DataSource = NPresentacion.Mostrar();
            this.OcultarColumnas();
            lblTotal.Text = "total de registros : " + Convert.ToString(dataListado.Rows.Count);
        }
        private void BuscarNombre()
        {
            this.dataListado.DataSource = NPresentacion.BuscarNombre(this.txtBuscarNombre.Text);
            this.OcultarColumnas();
            lblTotal.Text = "total de  registros " + Convert.ToString(dataListado.Rows.Count);
        }



        private void frmPresentacion_Load(object sender, EventArgs e)
        {
            this.Top = 0;
            this.Left = 0;

            this.Mostrar();
            this.Habilitar(false);
            this.Botones();

        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            this.BuscarNombre();
        }

        private void txtBuscarNombre_TextChanged(object sender, EventArgs e)
        {
            this.BuscarNombre();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            this.isNuevo = true;
            this.isEditar = false;
            this.Botones();
            this.Limpiar();
            this.Habilitar(true);
            this.txtNombre.Focus();

        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                string rpta = "";
                if (this.txtNombre.Text == string.Empty)
                {
                    MensajeError("falta ingresar el nombre del alumno,seran remarcados");
                    errorIcono.SetError(txtNombre, "ingrese el nombre ");
                }
                else
                {
                    if (this.isNuevo)
                    {
                        rpta = NPresentacion.Insertar(this.txtNombre.Text.Trim().ToUpper(), this.txtDescripcion.Text.Trim());
                    }
                    else
                    {
                        rpta = NPresentacion.Editar(Convert.ToInt32(this.txtIdPresentacion.Text), this.txtNombre.Text.Trim().ToUpper(), this.txtDescripcion.Text.TrimEnd());
                    }
                    if (rpta.Equals("OK"))
                    {
                        if (this.isNuevo)
                        {
                            this.MensajeOk("Se inserto correctamente");
                        }
                        else
                        {
                            this.MensajeOk("Se Actualizo correctamente");
                        }

                    }
                    else
                    {
                        this.MensajeError(rpta);
                    }
                    this.isNuevo = false;
                    this.isEditar = false;
                    this.Botones();
                    this.Limpiar();
                    this.Mostrar();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }

        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (!this.txtIdPresentacion.Text.Equals(""))
            {
                this.isEditar = true;
                this.Botones();
                this.Habilitar(true);
                //this.btnGuardar.Enabled = true;
            }
            else
            {
                this.MensajeError("Deve de selecionar el registro a modicar");
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.isNuevo = false;
            this.isEditar = true;
            this.Botones();
            this.Limpiar();
            this.Habilitar(false);
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult Option;
                Option = MessageBox.Show("realmenteq quier eliminar los registros", "Sistema de ventas", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (Option == DialogResult.OK)
                {
                    string codigo;
                    string rpta = "";

                    foreach (DataGridViewRow row in dataListado.Rows)
                    {
                        if (Convert.ToBoolean(row.Cells[0].Value))
                        {
                            codigo = Convert.ToString(row.Cells[1].Value);
                            rpta = NPresentacion.Eliminar(Convert.ToInt32(codigo));


                            if (rpta.Equals("OK"))
                            {
                                this.MensajeError("SE ELIMINO CORRECTAMENTE");
                            }
                            else
                            {
                                this.MensajeError(rpta);
                            }
                        }

                    }
                    this.Mostrar();

                }


            }
            catch (Exception EX)
            {
                MessageBox.Show(EX.Message + EX.StackTrace);
            }

        }

        private void chkEliminar_CheckedChanged(object sender, EventArgs e)
        {
            if (chkEliminar.Checked)
            {
                this.dataListado.Columns[0].Visible = true;

            }
            else
            {
                this.dataListado.Columns[0].Visible = false;
            }
        }

        private void dataListado_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dataListado.Columns["Eliminar"].Index)
            {
                DataGridViewCheckBoxCell chkEliminar = (DataGridViewCheckBoxCell)dataListado.Rows[e.RowIndex].Cells["Eliminar"];
                chkEliminar.Value = !Convert.ToBoolean(chkEliminar.Value);
            }
        }

        private void dataListado_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            this.txtIdPresentacion.Text = Convert.ToString(this.dataListado.CurrentRow.Cells["IDPRESENTACION"].Value);
            this.txtNombre.Text = Convert.ToString(this.dataListado.CurrentRow.Cells["NOMBRE"].Value);
            this.txtDescripcion.Text = Convert.ToString(this.dataListado.CurrentRow.Cells["DESCRIPCION"].Value);

            this.tabControl1.SelectedIndex = 1;
        }

        private void dataListado_DoubleClick(object sender, EventArgs e)
        {

        }
    }

}
