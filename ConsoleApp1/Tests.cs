using NUnit.Framework;

namespace CIExercise
{
    class Tests
    {

        [Test]
        public void VendingMachineTestMethod()
        {
            VendingMachine vendingMachine = new VendingMachine();

            Assert.That(vendingMachine, Is.Not.Null);
        }

        [Test]
        public void SetupCoinAmountsTestMethod()
        {
            VendingMachine vendingMachine = new VendingMachine();
            int[] coinTest = vendingMachine.SetupCoinMagazine();

            Assert.That(vendingMachine.CoinsMagazine, Is.EqualTo(coinTest));
        }

        [Test]
        public void SetupCoinAmountLengthTestMethod()
        {
            VendingMachine vendingMachine = new VendingMachine();

            Assert.That(vendingMachine.CoinsMagazine.Length, Is.EqualTo(vendingMachine.COIN_TYPES.Length));
        }
        

        [Test]
        public void UseOneCoinTestMethod()
        {
            VendingMachine vendingMachine = new VendingMachine();
            int[] coinTest = vendingMachine.SetupCoinMagazine();
            vendingMachine.CoinsMagazine[0] -= 1;

            Assert.That(vendingMachine.CoinsMagazine, Is.Not.EqualTo(coinTest));
        }


        [Test]
        public void CreateCoinTypesTestMethod()
        {
            VendingMachine vendingMachine = new VendingMachine();
            decimal[] vs = vendingMachine.CreateCoinTypesFromEnum();

            Assert.That(vendingMachine.COIN_TYPES, Is.EqualTo(vs));
        }

        [Test]
        public void ObtainCoins()
        {
            VendingMachine vendingMachine = new VendingMachine();
            decimal change = 5971;
            int[] coins = vendingMachine.FindChange(change);
            int[] expected = { 1, 0, 2, 1, 4, 1, 0, 0, 1, 0 };

            Assert.That(coins, Is.EqualTo(expected));
        }
    }
}