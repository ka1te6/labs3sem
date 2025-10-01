namespace Task3
{
    class CurrencyUSD : Currency
    {
        public static double ToEUR { get; set; }
        public static double ToRUB { get; set; }

        public CurrencyUSD(double value) : base(value) { }

        public static explicit operator CurrencyEUR(CurrencyUSD usd) =>
            new CurrencyEUR(usd.Value * ToEUR);

        public static explicit operator CurrencyRUB(CurrencyUSD usd) =>
            new CurrencyRUB(usd.Value * ToRUB);
    }
}