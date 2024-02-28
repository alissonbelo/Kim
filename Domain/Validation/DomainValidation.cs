namespace Domain.Validation;

public class DomainValidation : Exception
{
    public DomainValidation(string error) : base(error)
    {
        
    }

    public static void When(bool hasError, string error)
    {
        if (hasError)
            throw new DomainValidation(error);
    }
    
    public static void DateIsValid(DateTime? date, string errorMessage)
    {
        if (date == null || !IsValidDate(date.ToString(), "dd/MM/yyyy"))
        {
            throw new DomainValidation(errorMessage);
        }
    }

    private static bool IsValidDate(string? dateString, string format)
    {
        DateTime dateResult;
        return DateTime.TryParseExact(dateString, format, null, System.Globalization.DateTimeStyles.None, out dateResult);
    }
}