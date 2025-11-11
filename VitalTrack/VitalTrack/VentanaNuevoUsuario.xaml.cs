using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace VitalTrack
{
    /// <summary>
    /// Interaction logic for VentanaNuevoUsuario.xaml
    /// </summary>
    public partial class VentanaNuevoUsuario : Window
    {
        public VentanaNuevoUsuario()
        {
            InitializeComponent();
        }

        private void btnCargarFoto_Click(object sender, RoutedEventArgs e)
        {
            var dialogo = new OpenFileDialog();
            dialogo.Title = "Seleccione un fichero gráfico";
            dialogo.Filter = "Png (*.png)|*.jpg|Jpeg (*.jpg)|*.jpg";

            if (dialogo.ShowDialog() == true)
            {
                string trayectoria = dialogo.FileName;
                MessageBox.Show($"You chose: {trayectoria}");
            }
        }

        private void btnSalir_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
