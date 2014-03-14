using System.ComponentModel;
using System.Windows;

using GenericObjectVisualizer.TestUI.Annotations;

namespace GenericObjectVisualizer.TestUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private TestObject1 testObject;

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
            TestObject = new TestObject1();
        }

        public TestObject1 TestObject
        {
            get
            {
                return testObject;
            }
            set
            {
                testObject = value;
                OnPropertyChanged("TestObject");
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
