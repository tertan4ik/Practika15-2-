using System.Globalization;
using System.Windows.Controls;

namespace pract_15.Validators
{
    public class PositiveNumberValidator : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            var val = value.ToString() ?? String.Empty;

            bool successInvariant = float.TryParse(
                val,
                System.Globalization.NumberStyles.Float, // Allows decimal point, exponents, etc.
                System.Globalization.CultureInfo.InvariantCulture,
                out float numberInvariant
            );


            if (!successInvariant)
                return new ValidationResult(false, "Введите число");

            if (numberInvariant <= 0.00)
                return new ValidationResult(false, "Число должно быть больше 0");
            

            return ValidationResult.ValidResult;
        }
    }
}
