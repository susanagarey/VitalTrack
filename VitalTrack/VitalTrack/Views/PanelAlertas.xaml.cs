using Microsoft.EntityFrameworkCore;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using VitalTrack.Data;
using VitalTrack.Models;
using VitalTrack.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace VitalTrack.Views
{
    /// <summary>
    /// Interaction logic for PanelAlertas.xaml
    /// </summary>
    public partial class PanelAlertas : UserControl
    {
        public PanelAlertas()
        {
            InitializeComponent();
            RefrescarListaAlertas();
        }


        private void RefrescarListaAlertas()
        {
            List<AlertasViewModel> listaAlertas;
            using (VitaltrackContext db = new VitaltrackContext())
            {
                listaAlertas = db.Alertas.
                                  Include(x => x.Paciente).
                                  Include(x=> x.ReconocidaPorNavigation).
                                  Select(x => new AlertasViewModel
                                  {
                                      AlertaId               = x.AlertaId,
                                      PacienteId             = x.PacienteId,
                                      TipoAlerta             = x.TipoAlerta,
                                      Severidad              = x.Severidad,
                                      Mensaje                = x.Mensaje,
                                      DisparadaEn            = x.DisparadaEn,
                                      Reconocida             = x.Reconocida,
                                      ReconocidaEn           = x.ReconocidaEn,
                                      ReconocidaPor          = x.ReconocidaPor,
                                      CreadoEn               = x.CreadoEn,
                                      ActualizadoEn          = x.ActualizadoEn,
                                      NombreCompletoPaciente = x.Paciente.Nombre + " " + x.Paciente.Apellidos,
                                      NombreCompletoUsuario  = x.ReconocidaPorNavigation.Nombre + " " + x.ReconocidaPorNavigation.Apellidos
                                  }).ToList();
            }

            gridAlertas.ItemsSource = listaAlertas;

            gridAlertas.SelectedItem = listaAlertas.Count > 0 ? listaAlertas[0] : null;
        }
    }
}
