using System;
using System.ComponentModel;
using System.Windows;

namespace GenericObjectVisualizer
{
    public partial class VisualizerWindow : Window, INotifyPropertyChanged
    {
        private object _viewModel;
        private VisualizerWindowStyle _visualizerWindowStyle;

        public VisualizerWindow(VisualizerWindowStyle visualizerWindowStyle)
        {
            InitializeComponent();
            DataContext = this;
            GovJson.Visibility = Visibility.Collapsed;
            GovXml.Visibility = Visibility.Collapsed;
            GovKeyValue.Visibility = Visibility.Collapsed;
            _visualizerWindowStyle = visualizerWindowStyle;
            switch (_visualizerWindowStyle)
            {
                case VisualizerWindowStyle.XML:
                    GovXml.Visibility = Visibility.Visible;
                    break;
                case VisualizerWindowStyle.Json:
                    GovJson.Visibility = Visibility.Visible;
                    break;
                case VisualizerWindowStyle.KeyValue:
                    GovKeyValue.Visibility = Visibility.Visible;
                    break;
                case VisualizerWindowStyle.Undefined:
                    GovXml.Visibility = Visibility.Visible;
                    GovJson.Visibility = Visibility.Visible;
                    GovKeyValue.Visibility = Visibility.Visible;
                    break;
            }
        }

        public VisualizerWindow()
        {
            InitializeComponent();
            DataContext = this;
        }

        public object ViewModel
        {
            get
            {
                return _viewModel;
            }
            set
            {
                _viewModel = value;
                OnPropertyChanged("ViewModel");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public static Action VisualizeObject(object objectForVisualizing, VisualizerWindowStyle visualizerWindowStyle, bool modal = false)
        {
            var visualizerWindow = new VisualizerWindow(visualizerWindowStyle);
            visualizerWindow.ViewModel = objectForVisualizing;
            if (modal)
            {
                visualizerWindow.ShowDialog();

            }
            else
            {
                visualizerWindow.Show();
            }
            return visualizerWindow.Close;
        }
    }
}
