using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using VitalTrack.Data;
using VitalTrack.Models;

namespace VitalTrack.Views
{
    /// <summary>
    /// Interaction logic for PanelGraficas.xaml
    /// </summary>
    public partial class PanelGraficas : UserControl
    {
        private Paciente? pacienteSeleccionado;
        private int umbralActual = 0;
        private int numeroUmbralesActivos = 0;

        public PanelGraficas()
        {
            InitializeComponent();

            if (FindName("canvasGrafica") is Canvas cg)
            {
                // Redibujar en caso de evento de redimensionar el canvas, para que sea "responsive"
                cg.SizeChanged += (_, __) => RefrescarGraficaConstantesPaciente();

                // Capturar clics en el Canvas
                cg.AddHandler(UIElement.MouseLeftButtonDownEvent, new MouseButtonEventHandler(CanvasGrafica_MouseLeftButtonUp), true);
            }

            RefrescarListaPacientes();
        }

        private void gridPacientes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            pacienteSeleccionado = gridPacientes?.SelectedItem as Paciente;
            
            umbralActual = 0;
            numeroUmbralesActivos = 0;
            txtFecha.Text = "";

            RefrescarGraficaConstantesPaciente();
        }

        private void CanvasGrafica_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (sender is Canvas canvas)
            {
                if (numeroUmbralesActivos > 0)
                {
                    umbralActual = (umbralActual + 1) % numeroUmbralesActivos;
                    RefrescarGraficaConstantesPaciente();
                }
                else
                {
                    return;
                }
            }
        }


        private void RefrescarGraficaConstantesPaciente()
        {
            // Comprobar que el canvas está en el XAML: <Canvas x:Name="canvasGrafica" />
            if (FindName("canvasGrafica") is not Canvas canvasGrafica)
            {
                return;
            }

            // Limpiar el camvas
            canvasGrafica.Children.Clear();

            // Si no hay un paciente seleccionado, nada que dibujar
            if (pacienteSeleccionado == null)
            {
                return;
            }

            List<UmbralesPaciente> constantes;
            using (VitaltrackContext db = new VitaltrackContext())
            {
                constantes = db.UmbralesPacientes
                               .Where(x => x.PacienteId == pacienteSeleccionado.PacienteId)
                               .OrderByDescending(x => x.ActualizadoEn)
                               .ToList();
            }

            if (constantes.Count == 0)
            {
                return;
            }

            // Escoger las constnates a dibujar
            numeroUmbralesActivos = constantes.Count;
             
            var umbral = constantes[umbralActual];

            txtFecha.Text = umbral.CreadoEn.ToString();

            // Plantilla de la gráfica
            double width = canvasGrafica.ActualWidth > 0 ? canvasGrafica.ActualWidth : 600;
            double height = canvasGrafica.ActualHeight > 0 ? canvasGrafica.ActualHeight : 300;

            double leftMargin = 150; // Espaciado etiquetas
            double rightMargin = 24;
            double topMargin = 20;
            double bottomMargin = 30;

            double chartLeft = leftMargin;
            double chartRight = Math.Max(chartLeft + 50, width - rightMargin);
            double chartTop = topMargin;
            double chartBottom = Math.Max(chartTop + 50, height - bottomMargin);

            // Preparar información 
            var rows = new List<(string Label, double? Min, double? Max, double DomainMin, double DomainMax, Brush Brush)>
            {
                ("Tensión Sistólica (mmHg)",  ToD(umbral.MinTensionSistolica),  ToD(umbral.MaxTensionSistolica),  50, 200, Brushes.SteelBlue),
                ("Tensión Diastólica (mmHg)", ToD(umbral.MinTensionDiastolica), ToD(umbral.MaxTensionDiastolica), 30, 130, Brushes.CadetBlue),
                ("Frecuencia Card. (bpm)",    ToD(umbral.MinFrecuenciaCardiaca),ToD(umbral.MaxFrecuenciaCardiaca),30, 220, Brushes.Orange),
                ("SpO₂ (%)",                  ToD(umbral.MinSpo2),              ToD(umbral.MaxSpo2),              70, 100, Brushes.MediumSeaGreen),
                ("Temperatura (°C)",          ToD(umbral.MinTemperaturaC),      ToD(umbral.MaxTemperaturaC),      30, 45,  Brushes.IndianRed),
                ("Glucosa (mg/dL)",           ToD(umbral.MinGlucosa),           ToD(umbral.MaxGlucosa),           40, 400, Brushes.MediumOrchid),
            };

            //  Calcular la separación vertical
            var visibleRows = rows.Where(r => r.Min.HasValue && r.Max.HasValue && r.Min <= r.Max).ToList();
            if (visibleRows.Count == 0)
            {
                return;
            }

            double rowHeight = (chartBottom - chartTop) / visibleRows.Count;
            double barHeight = Math.Min(18, Math.Max(8, rowHeight * 0.35));
            double y = chartTop;

            // Dibujar las líneas verticales (0%, 25%, 50%, 75%, 100%)
            void DrawGrid()
            {
                var gridPercents = new[] { 0.0, 0.25, 0.5, 0.75, 1.0 };
                foreach (var p in gridPercents)
                {
                    double x = Lerp(chartLeft, chartRight, p);
                    var line = new Line
                    {
                        X1 = x, X2 = x, Y1 = chartTop - 6, Y2 = chartBottom,
                        Stroke = p == 0.0 || Math.Abs(p - 1.0) < 1e-6 ? Brushes.Gray : Brushes.LightGray,
                        StrokeThickness = p == 0.0 || Math.Abs(p - 1.0) < 1e-6 ? 1.2 : 0.8,
                        StrokeDashArray = p is 0.5 ? new DoubleCollection { 2, 2 } : null
                    };
                    canvasGrafica.Children.Add(line);
                }
            }

            DrawGrid();

            int rowIndex = 0;
            foreach (var row in visibleRows)
            {
                double rowTop = y + rowHeight * rowIndex;
                double centerY = rowTop + rowHeight / 2;

                // Etiqueta izquierda
                var lbl = new TextBlock
                {
                    Text = row.Label,
                    Foreground = Brushes.Black,
                    FontSize = 12,
                    TextTrimming = TextTrimming.CharacterEllipsis,
                };
                Canvas.SetLeft(lbl, 4);
                Canvas.SetTop(lbl, centerY - lbl.FontSize * 0.7);
                canvasGrafica.Children.Add(lbl);

                // Ajustar al ancho de la gráfica
                double min = Math.Max(row.DomainMin, Math.Min(row.DomainMax, row.Min!.Value));
                double max = Math.Max(row.DomainMin, Math.Min(row.DomainMax, row.Max!.Value));

                double x1 = Map(min, row.DomainMin, row.DomainMax, chartLeft, chartRight);
                double x2 = Map(max, row.DomainMin, row.DomainMax, chartLeft, chartRight);

                // Referencia para este dato
                var baseline = new Line
                {
                    X1 = chartLeft, X2 = chartRight, Y1 = centerY, Y2 = centerY,
                    Stroke = Brushes.Gainsboro,
                    StrokeThickness = 4
                };
                canvasGrafica.Children.Add(baseline);

                // Barra de rango
                var rect = new Rectangle
                {
                    Fill = row.Brush,
                    RadiusX = 4,
                    RadiusY = 4,
                    Height = barHeight,
                    Width = Math.Max(2, x2 - x1),
                    Stroke = Brushes.DimGray,
                    StrokeThickness = 0.6
                };
                Canvas.SetLeft(rect, x1);
                Canvas.SetTop(rect, centerY - barHeight / 2);
                canvasGrafica.Children.Add(rect);

                // Etiquetas Min / Max l
                var minLbl = new TextBlock
                {
                    Text = $"{min:0.#}",
                    Foreground = Brushes.DimGray,
                    FontSize = 11
                };
                Canvas.SetLeft(minLbl, x1 - minLbl.FontSize * 1.2);
                Canvas.SetTop(minLbl, centerY - barHeight / 2 - minLbl.FontSize - 2);
                canvasGrafica.Children.Add(minLbl);

                var maxLbl = new TextBlock
                {
                    Text = $"{max:0.#}",
                    Foreground = Brushes.DimGray,
                    FontSize = 11
                };
                Canvas.SetLeft(maxLbl, x2 - maxLbl.ActualWidth);
                Canvas.SetTop(maxLbl, centerY - barHeight / 2 - maxLbl.FontSize - 2);
                canvasGrafica.Children.Add(maxLbl);

                rowIndex++;
            }

            // Etiquetas de conexto
            void DrawAxisLabels()
            {
                var minAxis = new TextBlock { Text = "min", Foreground = Brushes.Gray, FontSize = 11 };
                var maxAxis = new TextBlock { Text = "max", Foreground = Brushes.Gray, FontSize = 11 };

                Canvas.SetLeft(minAxis, chartLeft - 8);
                Canvas.SetTop(minAxis, chartBottom + 4);
                canvasGrafica.Children.Add(minAxis);

                Canvas.SetLeft(maxAxis, chartRight - 16);
                Canvas.SetTop(maxAxis, chartBottom + 4);
                canvasGrafica.Children.Add(maxAxis);
            }

            DrawAxisLabels();

            // Métodos auxiliares
            static double Map(double v, double d0, double d1, double r0, double r1)
            {
                if (Math.Abs(d1 - d0) < 1e-9) return r0;
                var t = (v - d0) / (d1 - d0);
                return r0 + t * (r1 - r0);
            }

            static double Lerp(double a, double b, double t) => a + (b - a) * t;

            static double? ToD<T>(T? v) where T : struct => v.HasValue ? Convert.ToDouble(v.Value) : null;
        }

        private void RefrescarListaPacientes()
        {
            List<Paciente> pacientes;
            using (VitaltrackContext db = new VitaltrackContext())
            {
                pacientes = db.Pacientes.Where(x => x.Activo == true).ToList();
            }

            if (pacienteSeleccionado == null && pacientes.Count > 0)
            {
                pacienteSeleccionado = pacientes[0];
            }

            gridPacientes.ItemsSource = pacientes;

            // Refresh graph after list load if a patient is selected
            RefrescarGraficaConstantesPaciente();
        }
    }
}
