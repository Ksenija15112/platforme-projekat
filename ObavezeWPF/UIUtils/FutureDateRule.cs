using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ObavezeWPF.UIUtils
{
    class FutureDateRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            var selectedDateTime = (DateTime)value;
            if(selectedDateTime >= DateTime.Now)
            {
                return ValidationResult.ValidResult;
            }
            else
            {
                return new ValidationResult(false, "Date must be in future!");
            }
        }
    }
}
