using System;
using System.Collections.Generic;

namespace CIExercise
{
    class VendingMachine
    {
        public int[] CoinsMagazine { get; set; }

        // Can be changed to anything else
        const int INITIAL_COIN_AMOUNT = 100;

        // Denomination of coins
        decimal[] COIN_TYPES;


        public VendingMachine()
        {
            COIN_TYPES = CreateCoinTypesFromEnum();
            CoinsMagazine = SetupCoinMagazine();
        }

        int[] SetupCoinMagazine()
        {
            try
            {
                int[] coins = new int[COIN_TYPES.Length];

                for (var i = 0; i < COIN_TYPES.Length; i++)
                {
                    coins[i] = INITIAL_COIN_AMOUNT;
                }

                return coins;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return null;
            }
        }

        /* Method exists to avoid duplication with the Coins enum.
         */

        decimal[] CreateCoinTypesFromEnum()
        {
            string[] coinNames = Enum.GetNames(typeof(Coins));
            decimal[] coinValues = new decimal[coinNames.Length];

            for(var i = 0; i < coinNames.Length; i++)
            {
                try
                {
                    coinValues[i] = ConvertEnumNameToCoinValue(coinNames[i]);
                }
                catch(FormatException ex)
                {
                    Console.WriteLine(string.Format("{0}", ex.ToString()));
                }
            }

            return coinValues;
        }

        /* Limitation in the Coins.cs enum: Must contain 5 characters before
         * the denomination (COIN_, CENT_, NOTE_ are good words)
         * Also, everything is using integer values (in this case cents,
         * but it can also be whole values like JPY)
         */

        decimal ConvertEnumNameToCoinValue(string stringIn)
        {
            const int WHOLE_TO_SMALLEST_RATIO = 100;

            var subValue = stringIn.Substring(5);
            return Decimal.Parse(subValue) / WHOLE_TO_SMALLEST_RATIO;
        }

        /* This algorithm uses the greedy method since most real-life
         * monetary systems use the 1-2-5 system. However, its biggest limitation
         * is that for arbitrary coin systems, it may not be the global optimum.
         */

        int[] FindChange(decimal balance)
        {
            var coinsChange = new int[COIN_TYPES.Length];

            while (balance > 0.00m)
            {
                var index = FindGreatestIndex(balance);
                coinsChange[index] += 1;
                balance -= COIN_TYPES[index];
            }

            return coinsChange;
        }

        /* Used for finding the highest possible coin using 
         * the greedy method.
         */
        int FindGreatestIndex(decimal balance)
        {
            for(var index = 0; index < COIN_TYPES.Length; index++)
            {
                var value = COIN_TYPES[index];
                if (value > balance)
                    return index - 1;
            }
            return COIN_TYPES.Length - 1;
        }

        public string DisplayChange(int[] coins)
        {
            var hrCoinList = "";
            for (int index = 0; index < coins.Length; index++)
            {
                hrCoinList += string.Format("{0} JPY -- {1} coins\n", COIN_TYPES[index], coins[index]);
            }

            return hrCoinList;
        }

        bool CanGiveChange(int[] coins)
        {
            for(var index = 0; index < coins.Length; index++)
            {
                if (coins[index] > CoinsMagazine[index])
                    return false;
            }
            return true;
        }

        public string GiveChange(decimal balance, decimal price)
        {
            try
            {
                var coins = FindChange(balance - price);

                for (var index = 0; index < coins.Length; index++)
                {
                    CoinsMagazine[index] -= coins[index];
                }

                return DisplayChange(coins);
            }
            catch (Exception _) when (balance - price < 0)
            {
                return "Not enough money in the balance";
            }
            catch (Exception _) when (!CanGiveChange(FindChange(balance - price)))
            {
                return "Not enough coins in the bank";
            }
            
        }
    }
}
