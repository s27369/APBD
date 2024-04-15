using Zadanie3.Ships;
using Zadanie3.Exceptions;
namespace Zadanie3.Containers
{
	public abstract class Container
	{
		public int _cargoMass { get; set; }
		protected int _height { get; set; }
		protected int _containerMass { get; set; }
		protected int _depth { get; set; }
		protected int _maxCapacity { get; set; }
		protected string _serialNumber { get; set; }
		protected int _id;
		protected static int counter=0;
		

		public Container(int CargoMass, int Height, int ContainerMass, int Depth, int MaxCapacity):this( Height,ContainerMass, Depth, MaxCapacity)
		{
			LoadCargo(CargoMass);
			// Console.WriteLine(this);
		}
		public Container( int Height, int ContainerMass, int Depth, int MaxCapacity)
		{
			_id = GenerateId();
			_serialNumber = getSerialNumber();
			_maxCapacity = MaxCapacity;
			_height = Height;
			_containerMass = ContainerMass;
			_depth = Depth;
			_cargoMass = 0;
			// Console.WriteLine(this);
		}

		protected int GenerateId()
		{
			return counter++;
		}
		public virtual void Empty()
		{
			_cargoMass = 0;
		}

		public virtual void LoadCargo(int weight)
		{
			if (weight < 0)
			{
				throw new IncorrectCargoMass("Cargo mass can't be " + weight);
			}
			// Console.WriteLine("cmass="+_cargoMass+", loaded weight="+weight+", max capacity="+_maxCapacity);
			if ((_cargoMass + weight) > _maxCapacity)
			{
				throw new OverfillException(
					"Capacity for cargo exceeded by " + ((_cargoMass + weight)- _maxCapacity));
			}
				
			_cargoMass += weight;
		}

		public virtual string getSerialNumber()
		{
			return "KON-";
		}

		public int getTotalWeight()
		{
			return _cargoMass + _containerMass;
		}

		public override string ToString()
		{
			return _serialNumber+": total mass(kg)="+getTotalWeight()+", cargo mass(kg)="+_cargoMass+", container mass(kg)="+_containerMass+", max capacity(kg)="+_maxCapacity+", height(cm)="+_height+", depth(cm)="+_depth;
		}
	}
}