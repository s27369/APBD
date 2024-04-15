using Zadanie3.Exceptions;

namespace Zadanie3.Containers
{
    public class GasContainer:Container, IHazardNotifier
    {
        protected int _pressure { get; set; }

        public GasContainer(int CargoMass, int Height, int ContainerMass, int Depth, int MaxCapacity, int pressure) : base(CargoMass, Height, ContainerMass, Depth, MaxCapacity)
        {
            _pressure = pressure;
            // Console.WriteLine(this);
        }
        public GasContainer( int Height, int ContainerMass, int Depth, int MaxCapacity, int pressure) : base( Height, ContainerMass, Depth, MaxCapacity)
        {
            _cargoMass = 0;
            _pressure = pressure;
            // Console.WriteLine(this);
        }

        public override void Empty()
        {
            this._cargoMass = this._cargoMass / 20;
        }

        // public override void LoadCargo(int weight)
        // {
        //     try
        //     {
        //         base.LoadCargo(weight);
        //     }
        //     catch (OverfillException e)
        //     {
        //         SendHazardNotification();
        //         
        //         // throw e.GetBaseException();
        //     }
        // }

        public void SendHazardNotification(string msg)
        {
            Console.WriteLine(getSerialNumber()+" is in danger!");
            Console.WriteLine(msg);
        }

        public override string getSerialNumber()
        {
            return base.getSerialNumber()+"G-"+_id;
        }
        public override string ToString()
        {
            return base.ToString() + ", pressure(atmospheres)=" + _pressure + ";";
        }
    }
}