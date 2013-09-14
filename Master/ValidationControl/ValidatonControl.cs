using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Text.RegularExpressions;

namespace ValidationControl
{
	public class ValidationControl : TextBox
	{
        public static readonly DependencyProperty Is_valid_prpt =
    DependencyProperty.Register("IsValid", typeof(bool), typeof(ValidationControl), new PropertyMetadata(true, new PropertyChangedCallback(ValidationControl.OnIs_valid_prptChanged)));

        public static readonly DependencyProperty Val_rule_prpt =
      DependencyProperty.Register("ValidationRule", typeof(I_Val_Rule), typeof(ValidationControl), new PropertyMetadata(new PropertyChangedCallback(ValidationControl.OnVal_rule_prptChanged)));

        public static readonly DependencyProperty Val_Content_prpt =
      DependencyProperty.Register("ValidationContent", typeof(object), typeof(ValidationControl), new PropertyMetadata("Incorrect Value", new PropertyChangedCallback(ValidationControl.OnVal_Content_prptChanged)));

        public static readonly DependencyProperty Val_Symbol_prpt =
     DependencyProperty.Register("ValidationSymbol", typeof(object), typeof(ValidationControl), new PropertyMetadata("!", new PropertyChangedCallback(ValidationControl.OnVal_Symbol_prptChanged)));

        public static readonly DependencyProperty Val_ContentStyle_prpt =
      DependencyProperty.Register("ValidationContentStyle", typeof(Style), typeof(ValidationControl), null);

        public bool IsValid
        {
            get { return (bool)base.GetValue(Is_valid_prpt); }
            set { base.SetValue(Is_valid_prpt, value); }
        }
        
        public Style ValidationContentStyle
        {
            get { return base.GetValue(Val_ContentStyle_prpt) as Style; }
            set { base.SetValue(Val_ContentStyle_prpt, value); }
        }

        public object ValidationContent
		{
            get { return base.GetValue(Val_Content_prpt) as object; }
            set { base.SetValue(Val_Content_prpt, value); }
		}

        public object ValidationSymbol
        {
            get { return base.GetValue(Val_Symbol_prpt) as object; }
            set { base.SetValue(Val_Symbol_prpt, value); }
        }

        public I_Val_Rule ValidationRule
        {
            get { return base.GetValue(Val_rule_prpt) as I_Val_Rule; }
            set { base.SetValue(Val_rule_prpt, value); }
        }

		public ValidationControl()
		{
			DefaultStyleKey = typeof(ValidationControl);
		}

		public override void OnApplyTemplate()
		{
			base.OnApplyTemplate();
		}

		protected override void OnLostFocus(RoutedEventArgs e)
		{
            bool isInputValid = this.ValidationRule.my_validate(this.Text);
            this.IsValid = isInputValid;
            if(this.IsValid==false)
            {
                MessageBox.Show("invalid data");
                this.Text = "";
            }
            
			base.OnLostFocus(e);
		}

        private void ChangeVisualState(bool useTransitions)
        {
            if (!this.IsValid)
            {
                VisualStateManager.GoToState(this, "InValid", useTransitions);
            }
            else
            {
                VisualStateManager.GoToState(this, "Valid", useTransitions);
            }
        }

        private static void OnIs_valid_prptChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ValidationControl control = d as ValidationControl;
            bool newValue = (bool)e.NewValue;
            control.ChangeVisualState(false);
            //add some additional logic here.
        }

        private static void OnVal_Symbol_prptChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            //you can add some additional logic here.
        }

        private static void OnVal_Content_prptChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            //you can add some additional logic here.
        }

        private static void OnVal_rule_prptChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            //you can add some additional logic here.
        }
	}

	public interface I_Val_Rule
	{
		bool my_validate(string input);
	}

	public class Regex_Val_Rule : I_Val_Rule
	{
		public Regex_Val_Rule(string pattern)
		{
			this.Pattern = pattern;
		}

		public string Pattern
		{
			get;
			private set;
		}

		public bool my_validate(string input)
		{
			return Regex.IsMatch(input, this.Pattern);
		}
	}
}
