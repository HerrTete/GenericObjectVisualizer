using GenericObjectVisualizer.Converter;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace GenericObjectVisualizer
{
    public class GovKeyValue : Control
    {
        static GovKeyValue()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(GovKeyValue), new FrameworkPropertyMetadata(typeof(GovKeyValue)));
        }

        private ListView _targetListView = null;

        private Button _applyButton = null;

        private Button _resetButton = null;

        private KeyValueConverter _converter = new KeyValueConverter();

        private List<PropertyVisualizerInformations> _keyValue = null;

        private bool _readOnly;

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            var uiElement = GetTemplateChild("listViewTarget");
            _targetListView = uiElement as ListView;
            uiElement = GetTemplateChild("btnApplyChanges");
            _applyButton = uiElement as Button;
            uiElement = GetTemplateChild("btnReset");
            _resetButton = uiElement as Button;
            if (_applyButton != null)
            {
                _applyButton.Click += ApplyButton_Click;
                _applyButton.Visibility = _readOnly ? Visibility.Collapsed : Visibility.Visible;
            }
            if (_resetButton != null)
            {
                _resetButton.Click += ResetButton_Click;
                _resetButton.Visibility = _readOnly ? Visibility.Collapsed : Visibility.Visible;
            }
            SetTextBoxContent();
        }

        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            SetTextBoxContent();
        }

        private void ApplyButton_Click(object sender, RoutedEventArgs e)
        {
            var newTargetObject = KeyValueConverter.ConvertToObject(_keyValue, Content);
            if (newTargetObject != null)
            {
                Content = null;
                Content = newTargetObject;
            }
        }

        internal void SetTextBoxContent()
        {
            if (_targetListView != null)
            {
                _keyValue = KeyValueConverter.ConvertFromObject(Content);
                _targetListView.ItemsSource = _keyValue;
            }
        }

        public static readonly DependencyProperty ContentProperty = DependencyProperty.Register(
            "Content",
            typeof(object),
            typeof(GovKeyValue),
            new PropertyMetadata(default(object), PropertyChangedCallback));

        private static void PropertyChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            var depOb = dependencyObject as GovKeyValue;
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

        public bool ReadOnly
        {
            get
            {
                return _readOnly;
            }
            set
            {
                _readOnly = value;
                var visibility = Visibility.Collapsed;
                if (!_readOnly)
                {
                    visibility = Visibility.Visible;
                }
                if (_applyButton != null && _resetButton != null)
                {
                    _applyButton.Visibility = visibility;
                    _resetButton.Visibility = visibility;
                }
            }
        }
    }
}
