using System;
using System.ComponentModel;
using System.Windows;

using GenericObjectVisualizer.UiWrapper;

namespace GenericObjectVisualizer
{
    public partial class VisualizerWindow : Window, INotifyPropertyChanged
    {
        private object _viewModel;
        private VisualizerDisplayStyle _visualizerDisplayStyle;

        public VisualizerWindow(VisualizerDisplayStyle visualizerDisplayStyle)
        {
            InitializeComponent();
            DataContext = this;
            GovJson.Visibility = Visibility.Collapsed;
            GovXml.Visibility = Visibility.Collapsed;
            GovKeyValue.Visibility = Visibility.Collapsed;
            _visualizerDisplayStyle = visualizerDisplayStyle;
            switch (_visualizerDisplayStyle)
            {
                case VisualizerDisplayStyle.XML:
                    GovXml.Visibility = Visibility.Visible;
                    break;
                case VisualizerDisplayStyle.Json:
                    GovJson.Visibility = Visibility.Visible;
                    break;
                case VisualizerDisplayStyle.KeyValue:
                    GovKeyValue.Visibility = Visibility.Visible;
                    break;
                case VisualizerDisplayStyle.Undefined:
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

        public static Action VisualizeObject(object objectForVisualizing, VisualizerDisplayStyle visualizerDisplayStyle, VisualizerWindowStyle windowStyle = VisualizerWindowStyle.NonModal)
        {
            var visualizerWindow = new VisualizerWindow(visualizerDisplayStyle);
            visualizerWindow.ViewModel = objectForVisualizing;

            if (windowStyle == VisualizerWindowStyle.Modal)
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
