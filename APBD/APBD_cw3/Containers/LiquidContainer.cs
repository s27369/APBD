using Zadanie3.Exceptions;

namespace Zadanie3.Containers
{
    public class LiquidContainer : Container, IHazardNotifier

    {
        private bool Dangerous;
        public LiquidContainer(int CargoMass, int Height, int ContainerMass, int Depth, int MaxCapacity, bool dangerous) : this( Height, ContainerMass, Depth, MaxCapacity)
        {
            Dangerous = dangerous;
            LoadCargo(CargoMass);
            // Console.WriteLine(this);
        }
        public LiquidContainer( int Height, int ContainerMass, int Depth, int MaxCapacity) : base( Height, ContainerMass, Depth, MaxCapacity)
        {
            _cargoMass = 0;
            Dangerous = false;
            // Console.WriteLine(this);
        }

        public override string getSerialNumber()
        {
            return base.getSerialNumber()+"L-"+_id;
        }

        public void SendHazardNotification(string msg)
        {
            Console.WriteLine(getSerialNumber()+" is in danger!");
            Console.WriteLine(msg);
        }

        public override void LoadCargo(int weight)
        {
            // Console.WriteLine("_cargoMass="+_cargoMass+", weight=" + weight+", _maxCapacity" + _maxCapacity+", dangerous="+Dangerous);
            if (Dangerous && (_cargoMass + weight > _maxCapacity / 2))
            {
                SendHazardNotification("Dangerous liquid over 50% capacity");
            }
            // Console.WriteLine("_cargoMass="+_cargoMass+", weight=" + weight+", _maxCapacity" + _maxCapacity);
            if(_cargoMass + weight > _maxCapacity*0.9)
                
            {
                SendHazardNotification("Liquid over 90% capacity");
            }
            base.LoadCargo(weight);
        }

        public override string ToString()
        {
            return base.ToString() + ", dangerous cargo=" + Dangerous + ";";
        }
    }
    
}

