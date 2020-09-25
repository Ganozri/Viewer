using System;
using System.Collections.Generic;
using System.ComponentModel;
using LiveCharts.Defaults;
using LiveCharts.Geared;

namespace Medusa.Analyze1553B.UI
{
    public class ScrollableViewModel : INotifyPropertyChanged
    {
        private int _from;
        private int _to;

        public ScrollableViewModel()
        {

            int x = 180000;
            double[] arr = new double[] { 1, 1, 6, 5, 4, 32, 32, 32, 32, 32, 32, 1, 1, 6, 6, 6, 8, 12 };

            double[] d = new double[x];

            for (int i = 0; i < 10000; i++)
            {
                for (int j = 0; j < arr.Length; j++)
                {
                    d[j + (i * (arr.Length - 1))] = arr[j];
                }
            }

            Values = d.AsGearedValues();
            for (int i = 0; i < 100; i++)
            {
                Values.Add(32);
            }

            Values.WithQuality(Quality.Low);

            From = 0;
            To = 100;
        }

        public GearedValues<double> Values { get; set; }

        public int From
        {
            get { return _from; }
            set
            {
                _from = value;
                OnPropertyChanged("From");
            }
        }
        public int To
        {
            get { return _to; }
            set
            {
                _to = value;
                OnPropertyChanged("To");
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
