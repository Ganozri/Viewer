using System;
using System.Collections.Generic;
using System.ComponentModel;
using LiveCharts.Defaults;
using LiveCharts.Geared;
namespace LiveChartsTest
{
    public class ScrollableViewModel : INotifyPropertyChanged
    {
        private Func<double, string> _formatter;
        private double _from;
        private double _to;

        public ScrollableViewModel()
        {

            int x = 20000;
            double[] arr = new double[] {1,1,6,5,4,32,32,32,32,32,32,1,1,6,6,6,8,12 }; 
            
            double[] d = new double[x];

            for (int i = 0; i < 1000; i++)
            {
                for (int j = 0; j < arr.Length;j++)
                {
                    d[j+(i * (arr.Length-1))] = arr[j];
                }
            }
           

            Values = d.AsGearedValues();

            Values.WithQuality(Quality.Low);

            From = 0;
            To = 100;
        }

        public object Mapper { get; set; }
        public GearedValues<double> Values { get; set; }

        public double From
        {
            get { return _from; }
            set
            {
                _from = value;
                OnPropertyChanged("From");
            }
        }
        public double To
        {
            get { return _to; }
            set
            {
                _to = value;
                OnPropertyChanged("To");
            }
        }

        public Func<double, string> Formatter
        {
            get { return _formatter; }
            set
            {
                _formatter = value;
                OnPropertyChanged("Formatter");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            if (PropertyChanged != null)
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
