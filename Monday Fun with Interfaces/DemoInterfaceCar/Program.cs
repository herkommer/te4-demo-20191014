using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace DemoInterfaceCar
{
    class Program
    {
        static void Main(string[] args)
        {
            ICarRepository repo = new CarDB(); //abstract/interface sammankopplas med concrete/class/kod (new)

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
        private List<ICar> _cars = new List<ICar>();

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

        public void SecretSetting()
        {
            throw new NotImplementedException();
        }
    }

    public class CarDB : ICarRepository
    {
        //Skriv kod för at prata med DB
        private SqlConnection _cn;

        public CarDB()
        {
            //Skapa en connection till DB
            _cn = new SqlConnection(@"Data Source=(localdb)\mssqllocaldb;Initial Catalog=ACSSDB;Integrated Security=True");

        }

        public void Add(ICar car)
        {
            //INSERT INTO
            SqlCommand cm = new SqlCommand();
            cm.CommandText = string.Format("INSERT INTO Cars (Make, Model) VALUES ('{0}', '{1}')", car.Make, car.Model);
            cm.Connection = _cn;

            _cn.Open();
            cm.ExecuteNonQuery();
            _cn.Close();
        }

        public ICar GetCarById(ICar car)
        {
            //SELECT ... WHERE ...
            throw new NotImplementedException();
        }

        public List<ICar> GetCars()
        {
            //SELECT * FROM Cars
            SqlDataReader dr;
            SqlCommand cm = new SqlCommand();
            cm.CommandText = "SELECT * FROM Cars";
            cm.Connection = _cn;

            _cn.Open();
            dr = cm.ExecuteReader();

            List<ICar> _cars = new List<ICar>();
            while (dr.Read())
            {
                //gör något med det som vi får från DB
                _cars.Add(new Car() { Make = dr[1].ToString(), Model = dr[2].ToString() });
            }

            return _cars;

            dr.Close();
            _cn.Close();
        }
    }
}
