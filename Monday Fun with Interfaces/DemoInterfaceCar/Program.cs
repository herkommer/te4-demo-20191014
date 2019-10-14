using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoInterfaceCar
{
    class Program
    {
        static void Main(string[] args)
        {
            ICarRepository repo = new CarRepository(); //abstract/interface sammankopplas med concrete/class/kod (new)

            

            repo.Add(new Car() { Make = "Volvo", Model = "V70" });
            repo.Add(new Car() { Make = "Volvo", Model = "V60" });

            foreach (ICar anka in repo.GetCars())
            {
                Console.WriteLine("{0} {1}", anka.Make, anka.Model);
            }

        }
    }



    //Steg 1: Domänmodellen
    //Car, CarStorage

    //Steg 2: Interface/Abstract
    //Beskriv vad en Car ska kunna erbjuda
    public interface ICar
    {
        string Make { get; set; }
        string Model { get; set; }

    }

    public interface ICarRepository
    {
        void Add(ICar car);

        List<ICar> GetCars();

        ICar GetCarById(ICar car);
    }

    //Steg 3: Class/Concrete

    public class Car : ICar
    {
        public string Make { get; set; }
        public string Model { get; set; }
    }

    public class CarRepository : ICarRepository
    {
        public List<ICar> _cars = new List<ICar>();

        public void Add(ICar car)
        {
            _cars.Add(car);
        }

        public ICar GetCarById(ICar car)
        {
            throw new NotImplementedException();
        }

        public List<ICar> GetCars()
        {
            return _cars;
        }

    }
}
