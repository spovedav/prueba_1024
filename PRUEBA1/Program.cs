using PRUEBA.CORE.Interfaces.Repositorio;
using PRUEBA1.CORE.Utilities;
using System.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Data;
using System.Data.SqlClient;

namespace PRUEBA1
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            INIT();

            var services = ConfigureServices();
            var serviceProvider = services.BuildServiceProvider();

            // Inicia la aplicación pasando el servicio necesario al formulario
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            
            Application.Run(serviceProvider.GetRequiredService<Form1>());

            //Application.Run(new Form1());
        }

        private static void INIT()
        {
            ConfigInicial.fechaIncio = DateTime.Now;
            CreateConexion();
        }

        private static IServiceCollection ConfigureServices()
        {
            var services = new ServiceCollection();

            services.AddSingleton<IDbConnection>(new SqlConnection(ConfigInicial.CadenaConexion));

            // Registra el repositorio genérico
            services.AddScoped(typeof(IRepository<>), typeof(PRUEBA.CORE.Repositorio.Repository<>));

            // Registra el repositorio específico
            services.AddScoped<IUsuarioRepositorio, PRUEBA.CORE.Repositorio.UsuarioRepositorio>();

            // Registra el formulario principal
            services.AddScoped<Form1>(); // Suponiendo que `Form1` es tu formulario principal.

            return services;
        }

        private static void CreateConexion()
        {
            string Source = ConfigurationManager.AppSettings["Base.Source"];
            string UsuarioId = ConfigurationManager.AppSettings["Base.UsuarioId"];
            string Password = ConfigurationManager.AppSettings["Base.Password"];
            string Initial = ConfigurationManager.AppSettings["Base.Initial"];
            string Timeout = ConfigurationManager.AppSettings["Base.Timeout"];
            string Security = ConfigurationManager.AppSettings["Base.Security"];
            ConfigInicial.CadenaConexion = $"Data Source={Source};Initial Catalog={Initial};Integrated Security={Security}";
        }
    }
}