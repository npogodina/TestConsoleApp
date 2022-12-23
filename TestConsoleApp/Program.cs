using System.Runtime.InteropServices;
using System.Text;

namespace DesignPatterns;

// Stepwise Builder pattern
public enum CarType
{
    Sedan,
    SUV,
}

public class Car
{
    public CarType Type;
    public int WheelSize;
}

public interface ISpecifyCarType
{
    public ISpecifyWheelSize OfType(CarType type);
}

public interface ISpecifyWheelSize
{
    public IBuildCar WithWheels(int size);
}

public interface IBuildCar
{
    public Car Build();
}

public class CarBuilder
{
    private class Impl : ISpecifyCarType, ISpecifyWheelSize, IBuildCar
    {
        private Car car = new Car();

        public ISpecifyWheelSize OfType(CarType type)
        {
            car.Type = type;
            return this;
        }

        public IBuildCar WithWheels(int size)
        {
            switch (car.Type)
            {
                case CarType.SUV when size < 17 || size > 20:
                case CarType.Sedan when size < 15 || size > 17:
                    throw new ArgumentException($"Wrong wheel size for {car.Type}.");
            }

            car.WheelSize = size;
            return this;
        }

        public Car Build()
        {
            return car;
        }
    }

    public static ISpecifyCarType Create()
    {
        return new Impl();
    }
}

public class Program
{
    static void Main(string[] args)
    {
        var car = CarBuilder
            .Create() // returns ISpecifyCarType
            .OfType(CarType.SUV) // returns ISpecifyWheelSize
            .WithWheels(19) // returns IBuildCar
            .Build(); // returns Car
    }
}