using System.ComponentModel.DataAnnotations;

namespace BankWEB.Attributes
{
    public class SelectedAccountAttribute : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            return value != null && (int)value != -1;
        }
    }
}
