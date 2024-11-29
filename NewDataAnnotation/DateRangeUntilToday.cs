using System.ComponentModel.DataAnnotations;

namespace AssetsPro.NewDataAnnotation
{
    public class DateRangeUntilToday : ValidationAttribute
    {
        private readonly DateTime minDate;
        public DateRangeUntilToday(string minDate)
        {
            if (!DateTime.TryParseExact(minDate, "MM-dd-yyyy", null, System.Globalization.DateTimeStyles.None, out this.minDate))
            {
                throw new ArgumentException("Invalid date format for minDate. Use 'MM-dd-yyyy'.");
            }
        }
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return ValidationResult.Success; // Allow null values if they should pass validation
            }
            if (value is DateTime datavalue)
            {
                if (datavalue < minDate || datavalue > DateTime.Today)
                {
                    var errorMessage = $"Date must be between {minDate.ToShortDateString()} and {DateTime.Today.ToShortDateString()}";
                    return new ValidationResult(errorMessage);
                }
            }
            return ValidationResult.Success;
        }
    }
}
