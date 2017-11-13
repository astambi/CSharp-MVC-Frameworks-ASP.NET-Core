namespace CarDealer.Web.Infrastructure.Extensions
{
    public static class StringExtentions
    {
        private const string CurrencyFormat = "C2";
        private const string NumberFormat = "N0";
        private const string PercentageFormat = "P2";

        public static string ToNumber(this long number)
        {
            return number.ToString(NumberFormat);
        }

        public static string ToCurrency(this decimal price)
        {
            return price.ToString(CurrencyFormat);
        }

        public static string ToPercentage(this double percentage)
        {
            return percentage.ToString(PercentageFormat);
        }
    }
}
