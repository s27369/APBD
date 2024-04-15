using Zadanie3;
using Zadanie3.Containers;
using Zadanie3.Exceptions;

namespace Zadanie3.Ships
{

    public class Ship
    {
        private Dictionary<string, Container> _containers = new Dictionary<string, Container>();
        private int _maxSpeed { get; }
        private int _maxContainerAmount { get; }
        private int _containerAmount { get; set; }
        private double _maxCargoWeight { get; }//tonnes
        private double _cargoWeight { get; set; }
        private static int counter = 0;
        private int _id { get; }

        public Ship(int maxSpeed, int maxContainerAmount, int maxCargoWeight)
        {
            _maxSpeed = maxSpeed;
            _maxContainerAmount = maxContainerAmount;
            _maxCargoWeight = maxCargoWeight;
            _id = GenerateId();
        }

        private int GenerateId()
        {
            return ++counter;
        }

        public bool LoadContainer(Container c)
        {
            if ((_containerAmount >= _maxContainerAmount) || (_cargoWeight + (double)c.getTotalWeight()/1000 > _maxCargoWeight))
            {
                Console.WriteLine("Can't load container "+c);
                Console.WriteLine(_cargoWeight + (double)c.getTotalWeight()/1000+" vs "+_maxCargoWeight);
                return false;
            }
            _containerAmount += 1;
            _cargoWeight += (double)c.getTotalWeight()/1000;
            _containers.Add(c.getSerialNumber(), c);
            return true;
        }

        public bool LoadContainer(List<Container> containerList)
        {
            foreach (Container c in containerList)
            {
                if (!LoadContainer(c)) return false;
            }
            return true;
        }

        public Container findContainer(string serialNumber)
        {
                Container c;
                try
                {
                    c = _containers[serialNumber];
                }
                catch (Exception e)
                {
                    throw new ContainerNotFound("Could not find container: "+serialNumber);
                }
                return c;
        }
        public bool RemoveContainer(string serialNumber)
        {
            Container c;
            try
            {
                c = findContainer(serialNumber);
            }
            catch (ContainerNotFound)
            {
                return false;
            }
            _cargoWeight -= (double)c.getTotalWeight()/1000;
            _containerAmount -= 1;
            _containers.Remove(serialNumber);
            return true;
        }

        public bool UnloadContainer(string serialNumber)
        {
            foreach (var pair in _containers)
            {
                if (pair.Key == serialNumber)
                {
                    _cargoWeight -= (double)pair.Value._cargoMass/1000;
                    pair.Value.Empty();
                    return true;
                }
            }
            return false;
        }

        public bool ReplaceContainer(string serialNumber, Container c)
        {
            if (RemoveContainer(serialNumber) && LoadContainer(c)) return true;
            return false;
        }

        public bool MoveContainer(string serialNumber, Ship ship)
        {
            Container c;
            try
            {
                c = findContainer(serialNumber);
            }
            catch (ContainerNotFound e)
            {
                Console.WriteLine("Can't find container "+serialNumber);
                return false;
            }

            RemoveContainer(c.getSerialNumber());
            return ship.LoadContainer(c);
        }

        public override string ToString()
        {
            string msg = "Ship_" + _id + "{\nstats:(";
            msg += "max speed=" + _maxSpeed + ", max container amount=" + _maxContainerAmount + ", max cargo weight (tonnes)=" +
                   _maxCargoWeight + "),";
            msg += "\nstatus:(current container amount=" + _containerAmount + ", current cargo weight (tonnes)=" + _cargoWeight +
                   "),";
            msg += "\ncontainers:(\n";
            foreach (var pair in _containers)
            {
                msg += pair.Value+"\n";
            }

            msg += ")}";
            return msg;
        }
    }
}