namespace Task3
{
    class CurrencyRUB : Currency
    {
        public static double ToUSD { get; set; }
        public static double ToEUR { get; set; }

        public CurrencyRUB(double value) : base(value) { }

        public static explicit operator CurrencyUSD(CurrencyRUB rub) =>
            new CurrencyUSD(rub.Value * ToUSD);

        public static explicit operator CurrencyEUR(CurrencyRUB rub) =>
            new CurrencyEUR(rub.Value * ToEUR);
    }
}