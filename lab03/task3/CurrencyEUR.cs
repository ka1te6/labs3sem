namespace Task3
{
    class CurrencyEUR : Currency
    {
        public static double ToUSD { get; set; }
        public static double ToRUB { get; set; }

        public CurrencyEUR(double value) : base(value) { }

        public static explicit operator CurrencyUSD(CurrencyEUR eur) =>
            new CurrencyUSD(eur.Value * ToUSD);

        public static explicit operator CurrencyRUB(CurrencyEUR eur) =>
            new CurrencyRUB(eur.Value * ToRUB);
    }
}