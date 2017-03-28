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
using System.Data.SqlClient;

namespace CapaPresentacion
{
    public partial class FrmArticulo : Form
    {
        private bool IsNuevo = false;
        private bool IsEditar = false;

        //ESTE ES EL PARAMETRO PARA ENVIARA DE UN FORMULARIO A OTRO
        /*ES DECIR QUEREMO COMUNICARNOS CON EL FORMULARIO VISTA CATEGORIA POR QUE TENEMOS ID DE ESA TABLA
         **/
        private static FrmArticulo _Instancia;

        // si no tiene istancia creo una intacia de formulario y la envia o sino lo envia
        public static FrmArticulo GetInstacia()
        {
            if ( _Instancia == null)
                // si no tiene istancia creo el articulo
            {
                _Instancia = new FrmArticulo();// crear una istacia que esta es una clase
            }
            return _Instancia;

        }

        public void SetCategoria(string idCategoria,string nombre)
        {
            this.txtIdCategoria.Text = idCategoria;
            this.txtCategoria.Text = nombre;
        } 




        public FrmArticulo()
        {
            InitializeComponent();
            cuadro_informativo();
            this.llenarComboPresentacion();
       }
        private void cuadro_informativo()
        {
            //SetTOOLtIP establecer información sobre herramientas
            this.ttMenaje.SetToolTip(this.txtNombre, "ingrese el nombre del Articulo");
            this.ttMenaje.SetToolTip(this.pxImagen, "selecione la imagen del Articulo");
            this.ttMenaje.SetToolTip(this.txtIdCategoria, "selecione la categoria del articulo");
            this.ttMenaje.SetToolTip(this.cboIdPresentacion, "selecione la presentacion del articulo");

            this.txtIdCategoria.Visible= false;
            this.txtCategoria.ReadOnly = true;
        }
        private void MensajeError(String mensaje)
        {
            MessageBox.Show(mensaje,"Sistema de Ventas", MessageBoxButtons.OK,MessageBoxIcon.Error);
        }
        private void mensajeOK (string mensaje)
        {
            MessageBox.Show(mensaje,"Sistema de Ventas",MessageBoxButtons.OK,MessageBoxIcon.Information);
        }
        private void Limpiar()
        {
            this.txtCodigo.Text = string.Empty;
            this.txtNombre.Text = string.Empty;
            this.txtDescripcion.Text = string.Empty;
            this.txtIdCategoria.Text = string.Empty;
            this.txtCategoria.Text = string.Empty;
            this.txtIdArticulo.Text = string.Empty;
            this.pxImagen.Image = global::CapaPresentacion.Properties.Resources.file;
        }
        private void Habilitar(bool valor)
        {
            this.txtCodigo.Text = string.Empty;
            this.txtNombre.ReadOnly = !valor;
            this.txtDescripcion.ReadOnly = !valor;

            this.btnBuscarCategoria.Enabled = valor;
            this.cboIdPresentacion.Enabled = valor;
            this.btnCargar.Enabled = valor;
            this.btnCargar.Enabled = valor;
            this.btnLimpiar.Enabled = valor;

            this.txtIdArticulo.ReadOnly = !valor;
            }
        private void Botones()
        {
            if(this.IsNuevo  || this.IsEditar)
            {
                this.Habilitar(true);
                this.btnNuevo.Enabled = false;
                this.btnGuardar.Enabled = true;
                this.btnEditar.Enabled = false;
                this.btnCancelar.Enabled = true;
            }else
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
            //ocultando los datos como eliminar,id articulo,idcategia,idpresentacion
            this.dataListado.Columns[0].Visible = false;
           // this.dataListado.Columns[1].Visible = false;
         //   this.dataListado.Columns[6].Visible = false;
          //  this.dataListado.Columns[8].Visible = false;
        }
        private void Mostrar()
        {
            this.dataListado.DataSource = NArticulo.Mostrar();
            this.OcultarColumnas();
            lblTotal.Text = "Total de registros" + Convert.ToString(dataListado.Rows.Count);

        }
        private void BuscarNombre()
        {
            this.dataListado.DataSource = NArticulo.BuscarNombre(this.txtBuscarNombre.Text);
            this.OcultarColumnas();
            lblTotal.Text = "Total de registros" + Convert.ToString(dataListado.Rows.Count);
        }
        private void llenarComboPresentacion()
        {
            cboIdPresentacion.DataSource = NPresentacion.Mostrar();
            cboIdPresentacion.ValueMember = "IDPRESENTACION";
            cboIdPresentacion.DisplayMember = "NOMBRE";
        }
        private void btnCargar_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();

            DialogResult result = dialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                this.pxImagen.SizeMode = PictureBoxSizeMode.StretchImage;
                this.pxImagen.Image = Image.FromFile(dialog.FileName);
            }
           

        }
        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();

            DialogResult result = new DialogResult();

            if (result == DialogResult.OK)
            {
                this.pxImagen.SizeMode = PictureBoxSizeMode.StretchImage;
                this.pxImagen.Image = global::CapaPresentacion.Properties.Resources.file;
            }
           


        }
        private void FrmArticulo_Load(object sender, EventArgs e)
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
                            rpta = NArticulo.Eliminar(Convert.ToInt32(codigo));


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

        private void txtBuscarNombre_TextChanged(object sender, EventArgs e)
        {
         
            this.BuscarNombre();
   
    }

        private void btnNuevo_Click(object sender, EventArgs e)
        {

            this.IsNuevo = true;
            this.IsEditar = false;
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
                if (this.txtNombre.Text == string.Empty || this.txtIdCategoria.Text==string.Empty || this.txtCodigo.Text == string.Empty) 
                {
                    MensajeError("falta ingresar el nombre del alumno,seran remarcados");
                    errorIcono.SetError(txtNombre, "ingrese un valor ");
                    errorIcono.SetError(txtIdCategoria, "ingrese un valor ");
                    errorIcono.SetError(txtCodigo, "ingrese un valor ");
                }
                else
                {
                    System.IO.MemoryStream ms = new System.IO.MemoryStream();
                    this.pxImagen.Image.Save(ms,System.Drawing.Imaging.ImageFormat.Png);
                    this.pxImagen.Image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                    this.pxImagen.Image.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);

                    byte[] imagen = ms.GetBuffer();

                    if (this.IsNuevo)
                    {
                        rpta = NArticulo.Insertar(this.txtCodigo.Text.Trim(), this.txtNombre.Text.Trim(),
                            this.txtDescripcion.Text.Trim(), imagen,
                            Convert.ToInt32(this.txtIdArticulo.Text.Trim()), Convert.ToInt32(this.cboIdPresentacion.SelectedValue));
                    }
                    else
                    //editar
                    {
                        rpta = NArticulo.Editar(Convert.ToInt32(this.txtIdArticulo.Text), this.txtCodigo.Text.Trim(), this.txtNombre.Text.Trim(),
                            this.txtDescripcion.Text.Trim(), imagen,
                            Convert.ToInt32(this.txtIdArticulo.Text.Trim()), Convert.ToInt32(this.cboIdPresentacion.SelectedValue));
                    }
                    if (rpta.Equals("OK"))
                    {
                        if (this.IsNuevo)
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
                        this.MensajeError(rpta);
                    }
                    this.IsNuevo= false;
                    this.IsEditar = false;
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
            if (!this.txtIdCategoria.Text.Equals(""))
            {
                this.IsEditar = true;
                this.Botones();
                this.Habilitar(true);

            }
            else
            {
                this.MensajeError("Deve de selecionar el registro a modicar");
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.IsNuevo = false;
            this.IsEditar = true;
            this.Botones();
            this.Limpiar();
            this.Habilitar(false);
        }

        private void dataListado_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dataListado.Columns["Eliminar"].Index)
            {
                DataGridViewCheckBoxCell chkEliminar = (DataGridViewCheckBoxCell)dataListado.Rows[e.RowIndex].Cells["Eliminar"];
                chkEliminar.Value = !Convert.ToBoolean(chkEliminar.Value);
            }
        }

        private void dataListado_DoubleClick(object sender, EventArgs e)
        {
            this.txtIdArticulo.Text = Convert.ToString(this.dataListado.CurrentRow.Cells["IDARTICULO"].Value);
            this.txtCodigo.Text = Convert.ToString(this.dataListado.CurrentRow.Cells["CODIGO"].Value);
            this.txtNombre.Text = Convert.ToString(this.dataListado.CurrentRow.Cells["NOMBRE"].Value);
            this.txtDescripcion.Text = Convert.ToString(this.dataListado.CurrentRow.Cells["DESCRIPCION"].Value);

            //PARA LA IMGAEN
            byte[] imagenBuffer = (byte[])this.dataListado.CurrentRow.Cells["IMAGEN"].Value;
            System.IO.MemoryStream MS = new System.IO.MemoryStream(imagenBuffer);

            this.pxImagen.Image = Image.FromStream(MS);
            this.pxImagen.SizeMode = PictureBoxSizeMode.StretchImage;
            this.txtIdCategoria.Text = Convert.ToString(this.dataListado.CurrentRow.Cells["IDCATEGORIA"].Value);
            this.txtCategoria.Text = Convert.ToString(this.dataListado.CurrentRow.Cells["CATEGORIA"].Value);
            this.cboIdPresentacion.SelectedValue = Convert.ToString(this.dataListado.CurrentRow.Cells["IDPRESENTACION"].Value);


            this.tabControl1.SelectedIndex = 1;

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

        private void btnBuscarCategoria_Click(object sender, EventArgs e)
        {
            FrmVistaCategoria_articulo Form = new FrmVistaCategoria_articulo();
            Form.ShowDialog();

        }
    }
}
