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
    public partial class FrmCategoria : Form
    {

        private bool isNuevo = false;
        private bool isEditar = false;

        public FrmCategoria()
        {
            InitializeComponent();
            this.ttMenaje.SetToolTip(this.txtNombre,"ingrese el nombre de la categoria");
        }

        private void mensajeOK(String mensaje)
        {
            MessageBox.Show(mensaje,"Sistemas de Ventas", MessageBoxButtons.OK,MessageBoxIcon.Information);
        }
        private void mensajeError(String mensaje)
        {
            MessageBox.Show(mensaje,"Sistema  de ventas",MessageBoxButtons.OK,MessageBoxIcon.Error);
        }
        private void limpiar()
        {
            this.txtNombre.Text = string.Empty;
            this.txtDescripcion.Text = string.Empty;
            this.txtIdCategoria.Text = string.Empty;           

        }
        private void Habilitar(bool valor)
        {
            this.txtNombre.ReadOnly = !valor;
            this.txtDescripcion.ReadOnly = !valor;
            this.txtIdCategoria.ReadOnly = !valor;
        }

        private void Botones()
        {
            if(this.isNuevo || this.isEditar)
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
                this.btnNuevo.Enabled =true;
                this.btnGuardar.Enabled = false;
                this.btnEditar.Enabled = true;
                this.btnCancelar.Enabled = false;
            }            
        }

        private void OcultarColumnas()
        {
            //en esto tenemos que tener encuenta dque esl procedimiento tiene 3 campos peor la tabla tien 4
            this.dataListado.Columns[0].Visible = false;// check
           // this.dataListado.Columns[1].Visible = true;// ocultado el id
        }
        private void Mostrar()
        {
            this.dataListado.DataSource = NCategoria.Mostrar();
            this.OcultarColumnas();
            lblTotal.Text = "total de registros " + Convert.ToString(dataListado.Rows.Count);
        }

        private void BuscarNombre()
        {
            this.dataListado.DataSource = NCategoria.BuscarNombre(this.txtBuscarNombre.Text);
            this.OcultarColumnas();
            lblTotal.Text = "Total de Registros :"+ Convert.ToString(dataListado.Rows.Count);


        }

        private void FrmCategoria_Load(object sender, EventArgs e)
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

       

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            this.isNuevo = true;
            this.isEditar = false;
            this.Botones();
            this.limpiar();
            this.Habilitar(true);
            this.txtBuscarNombre.Focus();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                string rpta = "";
                if (this.txtNombre.Text == string.Empty)
                {
                    mensajeError("falta ingresar el nombre del alumno,seran remarcados");
                    errorIcono.SetError(txtNombre, "ingrese el nombre ");
                }
                else
                {
                    if (this.isNuevo)
                    {
                        rpta = NCategoria.Insertar(this.txtNombre.Text.Trim().ToUpper(),this.txtDescripcion.Text.Trim());
                    }else
                    {
                        rpta = NCategoria.Editar(Convert.ToInt32(this.txtIdCategoria.Text), this.txtNombre.Text.Trim().ToUpper(),this.txtDescripcion.Text.Trim());
                    }
                    if(rpta.Equals("OK"))
                    {
                        if (this.isNuevo)
                        {
                            this.mensajeOK("Se inserto correctamente");
                        }
                        else
                        {
                            this.mensajeOK("Se Actualizo correctamente");
                        }

                    }
                    else
                    {
                        this.mensajeError(rpta);
                    }
                    this.isNuevo = false;
                    this.isEditar = false;
                    this.Botones();
                    this.limpiar();
                    this.Mostrar();
                }

            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message+ ex.StackTrace);
            }

        }
        private void dataListado_DoubleClick(object sender, EventArgs e)
        {
            // Permite devolver los valores que tienen cada columna listo a cada un o de texto respectivo
            this.txtIdCategoria.Text = Convert.ToString(this.dataListado.CurrentRow.Cells["IDCATEGORIA"].Value);
            this.txtNombre.Text = Convert.ToString(this.dataListado.CurrentRow.Cells["NOMBRE"].Value);
            this.txtDescripcion.Text = Convert.ToString(this.dataListado.CurrentRow.Cells["DESCRIPCION"].Value);

            this.tabControl1.SelectedIndex = 1;
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (!this.txtIdCategoria.Text.Equals(""))
            {
                this.isEditar = true;
                this.Botones();
                this.Habilitar(true);
               
            }else
            {
                this.mensajeError("Deve de selecionar el registro a modicar");
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.isNuevo = false;
            this.isEditar = false;
            this.Botones();
            this.limpiar();
            this.Habilitar(false);
        }

        private void chkEliminar_CheckedChanged(object sender, EventArgs e)
        {
            if (chkEliminar.Checked)
            {
                this.dataListado.Columns[0].Visible=true;

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

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult Option;
                Option = MessageBox.Show("realmente quiere eliminar los registros","Sistema de ventas",MessageBoxButtons.OKCancel,MessageBoxIcon.Question);
                if (Option == DialogResult.OK)
                {
                    string codigo;
                    string rpta = "";

                    foreach (DataGridViewRow row in dataListado.Rows)
                    {
                        if (Convert.ToBoolean(row.Cells[0].Value))
                        {
                            codigo = Convert.ToString(row.Cells[1].Value);
                            rpta = NCategoria.Eliminar(Convert.ToInt32( codigo));


                            if (rpta.Equals("OK"))
                            {
                                this.mensajeOK("SE ELIMINO CORRECTAMENTE");
                            }else
                            {
                                this.mensajeError(rpta);
                            }
                        }

                    }
                    this.Mostrar();

                }


            }catch(Exception EX)
            {
                MessageBox.Show(EX.Message + EX.StackTrace);
            }
            
        }
       
        private void txtBuscarNombre_TextChanged(object sender, EventArgs e)
        {
            this.BuscarNombre();
        }
    }
}
