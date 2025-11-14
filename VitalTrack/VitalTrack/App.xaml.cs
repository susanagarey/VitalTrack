using Microsoft.Extensions.Configuration;
using System.Configuration;
using System.Data;
using System.IO;
using System.Windows;

namespace VitalTrack
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        // Parámetros de configuración de la app
        public static IConfiguration Configuration { get; private set; } = default!;
        public static string CarpetaFotos => Configuration["Images:CarpetaFotos"] ?? Path.Combine(AppContext.BaseDirectory, "Images", "Fotos");
        public static string FotoPorDefecto => Configuration["Images:FotoPorDefecto"] ?? "desconocido.png";

        static App()
        {
            // Cargar appsettings.json
            Configuration = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            AsegurarExistenciaCarpetaFotos();
        }

        private static void AsegurarExistenciaCarpetaFotos()
        {
            try
            {
                Directory.CreateDirectory(CarpetaFotos);
            }
            catch (Exception ex)
            {
                // Si la carpeta de red no está disponible, usar una local
                string local = Path.Combine(AppContext.BaseDirectory, "Images", "Fotos");
                Directory.CreateDirectory(local);
                Configuration["Images:CarpetaFotos"] = local;
                MessageBox.Show($"No se puede acceder a la carpeta de red.\nSe usará local:\n{local}\n{ex.Message}",
                                "Carpeta fotos inaccesible", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private static void AsegurarExistenciaFotoPorDefecto()
        {
            try
            {
                string destinoPorDefecto = Path.Combine(CarpetaFotos, FotoPorDefecto);
                if (!File.Exists(destinoPorDefecto))
                {
                    // Tenemos una copia de la foto por defecto en la carpeta local de la app
                    string carpetaLocal = Path.Combine(AppContext.BaseDirectory, "Images", "Fotos", FotoPorDefecto);
                    if (File.Exists(carpetaLocal))
                    {
                        File.Copy(carpetaLocal, destinoPorDefecto);
                    }
                }
            }
            catch { /* Non-critical */ }
        }
    }
}
