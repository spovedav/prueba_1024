using Dapper;
using PRUEBA.CORE.DTOs.Reporte;
using PRUEBA.CORE.Interfaces.Repositorio;
using PRUEBA1.CORE.Constante;
using PRUEBA1.CORE.Interfaces;
using PRUEBA1.CORE.Servicios;
using PRUEBA1.CORE.Utilities;
using System.Data;
using System.Windows.Forms;

namespace PRUEBA1
{
    public partial class Form1 : Form
    {
        private readonly IUsuarioRepositorio _usuarioRepositorio;
        private readonly IDbConnection dbConnection;

        public Form1(IUsuarioRepositorio usuarioRepositorio, IDbConnection dbConnection)
        {
            this._usuarioRepositorio = usuarioRepositorio ?? throw new ArgumentNullException(nameof(usuarioRepositorio));
            this.dbConnection = dbConnection ?? throw new ArgumentNullException(nameof(dbConnection));
            InitializeComponent();
            CargarDatosIniciales();     
        }

        private void CargarDatosIniciales()
        {
            // Establecer las propiedades del ToolTip
            toolTipAyuda.ToolTipTitle = "Información";
            toolTipAyuda.IsBalloon = true;  // Si deseas un estilo de globo
            toolTipAyuda.SetToolTip(dtp_RangoFecha, "De X fecha a Y fecha");
            lblVersion.Text = SistemaConst.version;

            //CargarDataGridView();

            dtp_RangoFecha.MinDate = ConfigInicial.fechaIncio.GetValueOrDefault(); // No permitir seleccionar una fecha futura
            dtp_RangoFecha.Value = dtp_RangoFecha.MinDate.AddDays(1); // Fecha predeterminada congelada
        }

        private void CargarDataGridView(DateTime? FechaSelect = null)   
        {
            dataGridView1.Rows.Clear();

            DateTime hoy = FechaSelect ?? DateTime.Now.AddDays(1);

            IFechaOptions fechaOptions = new FechaOptions();

            var resulatdoRnago = fechaOptions.ObtenerDiasSeman(hoy);

            var ress = _usuarioRepositorio.GetAll();

            var fechas = resulatdoRnago.Where(x => x.Key != "Usuario").ToList();
            var parametros = new
            {
                FechaInicio = fechas.FirstOrDefault().Value.fecha.GetValueOrDefault().Date,
                FechaFin = fechas.LastOrDefault().Value.fecha.GetValueOrDefault().Date
            };

            foreach (var item in resulatdoRnago)
            {
                dataGridView1.Columns.Add(item.Key, item.Value.formato);
            }

            var resultado = dbConnection.Query<DatosConsultaDto>("EXEC dbo.GetDataByDateRange @FechaInicio, @FechaFin", parametros);

            if (resultado.Any())
            {
                foreach (var item in resultado)
                {
                    dataGridView1.Rows.Add(
                                            item.Usuario, 
                                            item.Lunes, 
                                            item.Martes, 
                                            item.Miércoles, 
                                            item.Jueves, 
                                            item.Viernes, 
                                            item.Sábado, 
                                            item.Domingo
                                          );
                }
            }
            
        }

        private void excelToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void soporteToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void dtp_RangoFecha_ValueChanged(object sender, EventArgs e)
        {
            var valor = dtp_RangoFecha.Value;
            CargarDataGridView(valor);
        }
    }
}