using System;
using System.Windows;
using System.Windows.Controls;

namespace GenericObjectVisualizer
{
    public class GovText : Control
    {
        static GovText()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(GovText), new FrameworkPropertyMetadata(typeof(GovText)));
        }

        private TextBox _targetTextbox = null;

        private Button _applyButton = null;

        private Button _resetButton = null;

        private IConverter _converter = null;

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            if (Format == DisplayFormat.Xml)
            {
                _converter = new XmlConverter();
            }
            else
            {
                _converter = new JsonConverter();
            }

            var uiElement = GetTemplateChild("txtBxTarget");
            _targetTextbox = uiElement as TextBox;
            uiElement = GetTemplateChild("btnApplyChanges");
            _applyButton = uiElement as Button;
            uiElement = GetTemplateChild("btnReset");
            _resetButton = uiElement as Button;
            if (_applyButton != null)
            {
                _applyButton.Click += ApplyButton_Click;
            }
            if (_resetButton != null)
            {
                _resetButton.Click += ResetButton_Click;
            }
            SetTextBoxContent();
        }

        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            SetTextBoxContent();
        }

        private void ApplyButton_Click(object sender, RoutedEventArgs e)
        {
            var newTargetObject = _converter.ConvertToObject(_targetTextbox.Text, Content.GetType());
            if (newTargetObject != null)
            {
                Content = newTargetObject;
            }
        }

        internal void SetTextBoxContent()
        {
            if (_targetTextbox != null)
            {
                _targetTextbox.Text = _converter.ConvertFromObject(Content);
            }
        }

        public static readonly DependencyProperty ContentProperty = DependencyProperty.Register(
            "Content",
            typeof(object),
            typeof(GovText),
            new PropertyMetadata(default(object), PropertyChangedCallback));

        private static void PropertyChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            var depOb = dependencyObject as GovText;
            if (depOb != null)
            {
                depOb.SetTextBoxContent();
            }
        }

        public object Content
        {
            get
            {
                return (object)GetValue(ContentProperty);
            }
            set
            {
                SetValue(ContentProperty, value);
            }
        }

        public DisplayFormat Format { get; set; }
    }
}
