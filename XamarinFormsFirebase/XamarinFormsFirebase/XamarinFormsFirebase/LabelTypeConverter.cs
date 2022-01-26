using System;
using Xamarin.Forms;

namespace XamarinFormsFirebase
{
    public class LabelTypeConverter : Label
    {
        public static readonly BindableProperty MyTextProperty =
            BindableProperty.Create(nameof(MyText),
                typeof(string),
                typeof(LabelTypeConverter),
                propertyChanged: MyTextChanged);

        private static void MyTextChanged(BindableObject bindableObject, object oldValue, object newValue)
        {
            ((LabelTypeConverter)bindableObject).Text = newValue.ToString();
        }

        [TypeConverter(typeof(EnumToTextTypeConverter))]
        public string MyText
        {
            get => (string)GetValue(MyTextProperty);
            set => SetValue(MyTextProperty, value);
        }
    }

    public enum SubscribedStatus
    {
        Subbed = 0,
        NotSubbedBoo = 1,
        WillSubNow = 2
    }

    public class EnumToTextTypeConverter : TypeConverter
    {
        public override object ConvertFromInvariantString(string value)
        {
            string description = Enum.Parse(typeof(SubscribedStatus), value).ToString();
            return description;
        }
    }
}
