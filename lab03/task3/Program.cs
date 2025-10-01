using System;

namespace Task3
{
    class Program
    {
        static void Main()
        {
            CurrencyUSD.ToEUR = 0.95;
            CurrencyUSD.ToRUB = 90.0;
            CurrencyEUR.ToUSD = 1.05;
            CurrencyEUR.ToRUB = 95.0;
            CurrencyRUB.ToUSD = 0.011;
            CurrencyRUB.ToEUR = 0.0105;

            var usd = new CurrencyUSD(100);
            var eur = (CurrencyEUR)usd;
            var rub = (CurrencyRUB)usd;
            Console.WriteLine($"USD: {usd.Value}, EUR: {eur.Value:F2}, RUB: {rub.Value:F2}");
        }
    }
}