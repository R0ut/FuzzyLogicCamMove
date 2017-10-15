using System;
using System.Globalization;
using System.Windows.Data;

namespace ChartModule.Converters
{
    /// <summary>
    /// Inverse bool
    /// </summary>
    public class InverseBooleanConverter : IValueConverter
    {
        /// <summary>
        /// Inverse bool
        /// </summary>
        /// <param name="value">Bool value</param>
        /// <param name="targetType">Target type converter</param>
        /// <param name="parameter">Converter parameter</param>
        /// <param name="culture">CultureInfo parameter</param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (targetType != typeof(bool))
                throw new InvalidOperationException("The target must be a boolean");

            return !(bool)value;
        }

        /// <summary>
        /// Not implemented convert back method- there is no need to convert this values back
        /// </summary>
        /// <param name="value">Bool value</param>
        /// <param name="targetType">Target type converter</param>
        /// <param name="parameter">Converter parameter</param>
        /// <param name="culture">CultureInfo parameter</param>
        /// <returns></returns>

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
