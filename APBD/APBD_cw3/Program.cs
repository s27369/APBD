using Zadanie3.Containers;
using Zadanie3.Exceptions;
using Zadanie3.Ships;
namespace Zadanie3
{

    class Program
    {
        static void Main(string[] args)
        {
            Ship ship1 = new Ship(10, 15, 5);
            Ship ship2 = new Ship(30, 12, 4);
            Container liquidContainer1, liquidContainer3;
            Console.WriteLine("Loading too much cargo to a container...");
            try
            {
                liquidContainer1 = new LiquidContainer(1500, 1000, 100, 1200, 1000, false);
            }
            catch (OverfillException e)
            {
                Console.WriteLine(e.Message);
                liquidContainer1 = new LiquidContainer(1000, 100, 1200, 1000);
                liquidContainer1.LoadCargo(300);
            }

            Console.WriteLine("\nNegative cargo weight...");
            try
            {
                liquidContainer3 = new LiquidContainer(-100, 1000, 100, 1200, 1000, false);
            }
            catch (IncorrectCargoMass e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine("\nLoading 100% of max capacity to a liquid container...");
                liquidContainer3 = new LiquidContainer(1000, 100, 1200, 5000);
                liquidContainer3.LoadCargo(5000);
            }

            Container gasContainer1 = new GasContainer(400, 1500, 100, 1300, 1000, 5);
            Console.WriteLine("\nGas container cargo mass >90% of capacity...");
            Container gasContainer2 = new GasContainer(950, 1300, 100, 1100, 1000, 9);
            Container coolingContainer1 = new CoolingContainer(100, 500, 100, 200, 500, "Chocolate");

            
            List<Container> ContainerList = new List<Container>();
            ContainerList.Add(new LiquidContainer(500, 10, 100, 10, 1000, true));
            ContainerList.Add(new LiquidContainer(600, 10, 100, 10, 1000, true));
            ContainerList.Add(new LiquidContainer(950, 10, 100, 10, 1000, false));
            ContainerList.Add(new GasContainer(100, 10, 100, 10, 1000, 1));
            ContainerList.Add(new CoolingContainer(200,10,100,10,500,"Ice Cream"));
            ContainerList.Add(new CoolingContainer(400,10,100,10,500,"Fish"));
            ContainerList.Add(new CoolingContainer(500,10,100,10,500,"Bananas"));
            Console.WriteLine("\nTrying to load over max weight...");
            ContainerList.Add(new CoolingContainer(500,10,100,10,500,"Cheese"));
            
            Console.WriteLine("\n----------------loading ships:----------------");
            
            ship1.LoadContainer(liquidContainer1);
            ship1.LoadContainer(gasContainer1);
            ship1.LoadContainer(ContainerList);

            ship2.LoadContainer(gasContainer2);
            ship2.LoadContainer(coolingContainer1);

            Console.WriteLine(ship1);
            Console.WriteLine(ship2);
            
            Console.WriteLine("\n----------------Moving containers from ship1 to ship2:----------------");
            Console.WriteLine("Moving KON-L-9 from ship1 to ship2...");
            Console.WriteLine(ship1.MoveContainer("KON-L-9", ship2));
            Console.WriteLine(ship1);
            Console.WriteLine(ship2);


            Console.WriteLine("\n----------------Container operations:----------------");
            Console.WriteLine("finding container KON-C-12: "+ship1.findContainer("KON-C-12"));
            Console.WriteLine("unloading KON-C-12...");
            ship1.UnloadContainer("KON-C-12");
            Console.WriteLine("finding container KON-C-12: "+ship1.findContainer("KON-C-12"));

            Console.WriteLine("removing KON-C-13...");
            Console.WriteLine(ship1.RemoveContainer("KON-C-13"));
            Console.WriteLine("replacing KON-G-10 with a different container");
            Container replacement = new LiquidContainer(1000, 100, 100, 1000);
            Console.WriteLine(ship1.ReplaceContainer("KON-G-10", replacement));
            Console.WriteLine(ship1);
        }
    }
}