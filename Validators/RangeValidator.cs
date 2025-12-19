using System.Globalization;
using System.Windows.Controls;

namespace pract_15.Validators
{
    public class RangeValidator : ValidationRule
    {
        public decimal Min { get; set; }
        public decimal Max { get; set; }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {

            //if (!decimal.TryParse(value?.ToString(), out decimal number))
            //    return new ValidationResult(false, "Введите число");

            //if (number < Min || number > Max)
            //    return new ValidationResult(false, $"Значение от {Min} до {Max}");

            return ValidationResult.ValidResult;
        }
    }
}
