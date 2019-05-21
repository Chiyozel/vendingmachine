using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIExercise
{
    class VendingMachineApp
    {
        static void Main(string[] args)
        {
            VendingMachine vm = new VendingMachine();
            vm.GiveChange(2.00m, 1.85m);
            vm.GiveChange(5.00m, 4.16m);
            vm.GiveChange(2.17m, 1.31m);
            vm.GiveChange(2.00m, 1.00m);
            vm.GiveChange(5.88m, 3.21m);

            Console.WriteLine(vm.DisplayChange(vm.CoinsMagazine));

            Console.ReadKey();
        }
    }
}
