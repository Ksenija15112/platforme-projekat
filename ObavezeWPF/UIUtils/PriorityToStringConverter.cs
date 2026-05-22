using ObavezeWPF.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace ObavezeWPF.UIUtils
{
    public class PriorityToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) { return ""; }
            if (targetType != typeof(string))
            {
                throw new ArgumentException("Can only convert to String.");
            }
            if (value.GetType() != typeof(Priority))
            {
                throw new ArgumentException("Can only convert Priority values.");
            }

            var priorityItem = (Priority)value;
            if(priorityItem == Priority.VeryHigh)
            {
                return "Very High";
            }
            else
            {
                return priorityItem.ToString();
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
