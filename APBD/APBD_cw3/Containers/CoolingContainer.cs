namespace Zadanie3.Containers
{

    public class CoolingContainer:Container
    {
        protected private double _temperature;
        private string _product { get; set; } = "";
        public string Procuct { get { return _product; } set { _product = value;} }
        private Dictionary<string, double> _productsDictionary = new Dictionary<string, double>
        {
            { "Bananas", 13.3 }, { "Chocolate", 18 }, { "Fish", 2 }, { "Meat", -15 }, { "Ice Cream", -18 },
            { "Frozen pizza", -30 }, { "Cheese", 7.2 }, { "Sausages", 5 }, { "Butter", 20.5 }, { "Eggs", 19 }, {"None", 20}
        };

        public CoolingContainer(int CargoMass, int Height, int ContainerMass, int Depth, int MaxCapacity,
            string product) : base(CargoMass, Height, ContainerMass, Depth, MaxCapacity)
        {
            _product = product;
            _temperature = _productsDictionary[_product];
        }
        // public CoolingContainer( int Height, int ContainerMass, int Depth, int MaxCapacity) : base( Height, ContainerMass, Depth, MaxCapacity)
        // {
        //     _cargoMass = 0;
        //     _product = "None";
        //     _temperature = _productsDictionary["None"];
        // }

        public override string getSerialNumber()
        {
            return base.getSerialNumber()+"C-"+_id;
        }

        public override string ToString()
        {
            return base.ToString()+", product="+_product+", temperature(C)="+_temperature+";";
        }
    }
}