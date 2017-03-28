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
    public partial class FrmVistaCategoria_articulo : Form
    {
        public FrmVistaCategoria_articulo()
        {
            InitializeComponent();
        }
        private void OcultarColumnas()
        {
            //en esto tenemos que tener encuenta dque esl procedimiento tiene 3 campos peor la tabla tien 4
            this.dataListado.Columns[0].Visible = false;// check
          //  this.dataListado.Columns[1].Visible = false;// ocultado el id
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
            lblTotal.Text = "Total de Registros :" + Convert.ToString(dataListado.Rows.Count);


        }

        private void FrmVistaCategoria_articulo_Load(object sender, EventArgs e)
        {
            this.BuscarNombre();

        }
        private void txtBuscarNombre_TextChanged(object sender, EventArgs e)
        {
            this.Mostrar();
        }

        private void dataListado_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataListado_DoubleClick(object sender, EventArgs e)
        {
            FrmArticulo form = FrmArticulo.GetInstacia();
            String par1, par2;

            par1 = Convert.ToString(this.dataListado.CurrentRow.Cells["IDCATEGORIA"].Value);
            par2 = Convert.ToString(this.dataListado.CurrentRow.Cells["NOMBRE"].Value);
            form.SetCategoria(par1,par2);
            this.Hide();
        }

        //private void txtBuscarNombre_TextChanged_1(object sender, EventArgs e)
        //{
        //    this.Mostrar();
        //}
    }
}
