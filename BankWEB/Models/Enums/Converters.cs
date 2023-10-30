namespace BankWEB.Models.Enums
{
    public static class Converters
    {
        public static Dictionary<float, TimeSpan> DurationToPercents = new()
        {
            { 5.0f, TimeSpan.FromDays(30) },
            { 5.5f, TimeSpan.FromDays(90) },
            { 6.0f, TimeSpan.FromDays(180) },
            { 6.5f, TimeSpan.FromDays(365) },
            { 7.0f , TimeSpan.FromDays(1095) }
        };

        public static Dictionary<Status, float> StatusToCommission = new()
        {
            { Status.Individual, 3.0f },
            { Status.Entity, 4.0f },
            { Status.Company, 4.5f }
        };
    }
}
