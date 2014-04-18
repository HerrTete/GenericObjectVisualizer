using System.ComponentModel;
using System.Windows;

namespace GenericObjectVisualizer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class VisualizerDialog : Window, INotifyPropertyChanged
    {
        private object _viewModel;

        public VisualizerDialog()
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
    }
}
