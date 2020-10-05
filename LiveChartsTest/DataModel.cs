using LiveCharts;
using LiveCharts.Configurations;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Threading;

namespace LiveChartsTest
{
    public class DataModel : INotifyPropertyChanged
{
  public DataModel()
  {
    this.ChartValues = new ChartValues<ObservablePoint>();
    this.XMax = 360;
    this.XMin = 0;

    // Initialize the sine graph
    for (double x = this.XMin; x <= this.XMax; x++)
    {
      var point = new ObservablePoint() {X = x, Y = Math.Sin(x * Math.PI / 180)};
      this.ChartValues.Add(point);
    }

    this.SeriesCollection = new SeriesCollection
    {
      new LineSeries
      {
        Configuration = new CartesianMapper<ObservablePoint>()
          .X(point => point.X)
          .Y(point => point.Y)
          .Stroke(point => point.Y > 0.3 ? Brushes.Red : Brushes.LightGreen)
          .Fill(point => point.Y > 0.3 ? Brushes.Red : Brushes.LightGreen),
        Title = "Sine Graph",
        Values = this.ChartValues,
        PointGeometry = null
      }
    };

    Task.Run(async () => await StartSineGenerator());
  }

  private async Task StartSineGenerator()
  {
    while (true)
    {
      // Use Dispatcher.InvokeAsync with DispatcherPriority.Background 
      // to improve UI responsiveness
      Application.Current.Dispatcher.InvokeAsync(
        () =>
        {
          // Shift item data (and not the items) to the left
          for (var index = 0; index < this.ChartValues.Count - 1; index++)
          {
            ObservablePoint currentPoint = this.ChartValues[index];
            ObservablePoint nextPoint = this.ChartValues[index + 1];
            currentPoint.X = nextPoint.X;
            currentPoint.Y = nextPoint.Y;
          }

          // Add the new reading
          ObservablePoint newPoint = this.ChartValues[this.ChartValues.Count - 1];
          newPoint.X += 1;
          newPoint.Y = Math.Sin(newPoint.X * Math.PI / 180);

         // Update axis min/max
          this.XMax = newPoint.X;
          this.XMin = this.ChartValues[0].X;
        },
        DispatcherPriority.Background);

      await Task.Delay(TimeSpan.FromMilliseconds(50));
    }
  }

  private double xMax;
  public double XMax
  {
    get => this.xMax;
    set
    {
      this.xMax = value;
      OnPropertyChanged();
    }
  }

  private double xMin;
  public double XMin
  {
    get => this.xMin;
    set
    {
      this.xMin = value;
      OnPropertyChanged();
    }
  }

  public ChartValues<ObservablePoint> ChartValues { get; set; }
  public SeriesCollection SeriesCollection { get; set; }
  public Func<double, string> LabelFormatter => value => value.ToString("F");

  public event PropertyChangedEventHandler PropertyChanged;
  protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
  {
    this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
  }
}
}
