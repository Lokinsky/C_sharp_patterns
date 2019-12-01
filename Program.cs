using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    class Program
    {
        static void Main(string[] args)
        {

            Program pr = new Program();
            FactoryCar bmwFactory = new BMW_Factory();
            FactoryCar mercedesFactory = new Mercedes_Factory();
            CarList carList = new CarList();


            carList.Add("BMW", bmwFactory
                .V("9.4")
                .Complection("Full")
                .CreateCars(new OptionsCarFactory(15)
                    .AddModel(Model:"M2",Count:2)
                    .AddModel(Model: "M6", Count: 2)
                    .AddModel(Model: "M5", Count: 1)
                ));
            carList.Add("Mercedes", mercedesFactory
                .Complection("Full")
                .V("9.2")
                .CreateCars(new OptionsCarFactory(15)
                    .AddModel(Model:"Amx",Count:1)
                    .AddModel(Model:"Mx3", Count: 2)
                ));
            carList.Add("Daimler", mercedesFactory
                .SetBrand("Daimler")
                .CreateCars(new OptionsCarFactory(15)
                    .AddModel(Model: "dd", Count: 1)
                    .AddModel(Model: "ss", Count: 2)
                ));

            foreach (var cars in carList.cars)
            {
                Console.WriteLine(cars.Key);
                foreach(Auto car in cars.Value)
                {
                    Console.WriteLine(car.Get_Description());
                    
                }
            }
            
            Console.Read();

        }

        
    }

    interface Auto
    {
        string Brand { get; set; }
        string Subsidiary { get; set; }
        string Model { get; set; }
        string V { get; set; }
        string Complection { get; set; }
        string Get_Description();
        Auto Clone();
    }

    public class Car : Auto
    {

        public string Brand { get; set; }
        public string Model { get; set; }
        public string V { get; set; }
        public string Complection { get; set; }
        public string Subsidiary { get; set; }

        public Car(CarBuilder car_prop)
        {
            Brand = car_prop.Brand;
            Model = car_prop.Model;
            V = car_prop.V;
            Complection = car_prop.Complection;
            Subsidiary = car_prop.Subsidiary;

        }
        public string Get_Description()
        {
            return $"Brand: {Brand}, Subsidiary: {Subsidiary}, Model: {Model}, V: {V}L, Complection: {Complection}";
        }

        Auto Auto.Clone()
        {
            return this;
        }
    }
    public class CarBuilder : Auto
    {
         public string Brand { get;  set; }
         public string Model { get;  set; }
         public string V { get;  set; }
         public string Complection { get;  set; }
        public string Subsidiary { get ; set ; }

        public CarBuilder brand(string _brand)
        {
            Brand = _brand;
            return this;
        }
        public CarBuilder model(string _model)
        {
            Model = _model;
            return this;
        }
        public CarBuilder v(string _v)
        {
            V = _v;
            return this;
        }
        public CarBuilder complection(string _complection)
        {
            Complection = _complection;
            return this;
        }
        public CarBuilder subsidiary(string _subsidiary)
        {
            Subsidiary = _subsidiary;
            return this;
        }
        public Car Build()
        {
            return new Car(this);
        }

        public string Get_Description()
        {
            throw new NotImplementedException();
        }

         Auto Clone()
        {
            throw new NotImplementedException();
        }

        Auto Auto.Clone()
        {
            throw new NotImplementedException();
        }
    }

    interface Options
    {
        Dictionary<string, int> CountModels { get; set; }
    }
    class OptionsCarFactory : Options
    {
        public Dictionary<string, int> CountModels { get ; set ; }
        public OptionsCarFactory(int capacite)
        {
            CountModels = new Dictionary<string, int>();
        }

        public OptionsCarFactory AddModel(string Model, int Count)
        {
            CountModels.Add(Model, Count);
            return this;
        }

        
    }

    interface s
    {
        void d(string s);
    }
    interface CarInstruments
    {
        List<Auto> cars { get; set; }
        List<Auto> Get_Cars(Options opt);    
    }

    class BMW_Product : CarInstruments, Auto
    {
        public string Brand { get; set; }
        public string Model { get; set; }
        public string V { get; set ; }
        public string Complection { get; set; }
        public List<Auto> cars { get; set; }
        public string Subsidiary { get; set; }

        public BMW_Product(string subsidiary)
        {
            Subsidiary = subsidiary;
            cars = new List<Auto>();
        }
        public List<Auto> Get_Cars(Options opt)
        {
            foreach (var key in opt.CountModels)
            {
                for (int i = key.Value; i > 0; i--)
                {
                    this.cars.Add
                        (
                        new CarBuilder().brand("BMW").subsidiary(_subsidiary: Subsidiary).model(key.Key).v(V).complection(Complection).Build()
                        );

                }
            }
            return cars;
        }
        public Auto Clone()
        {
            throw new Exception("Not Implented");
        }

        public string Get_Description()
        {
            throw new Exception("Not Implented");
        }
    }

    class Mercedes_Product : CarInstruments, Auto
    {
        public string Brand { get; set; }
        public string Model { get; set; }
        public string V { get; set; }
        public string Complection { get; set; }
        public List<Auto> cars { get; set; }
        public string Subsidiary { get; set; }

        public Mercedes_Product(string subsidiary)
        {
            Brand = "Mercedes";
            Subsidiary = subsidiary;
            cars = new List<Auto>();
        }
        public List<Auto> Get_Cars(Options opt)
        {
            foreach (var key in opt.CountModels)
            {
                for (int i = key.Value; i > 0; i--)
                {
                    this.cars.Add
                        (
                        new CarBuilder().brand(Brand).subsidiary(_subsidiary:Subsidiary).model(key.Key).Build()
                        );

                }
            }
            return cars;
        }
        public Auto Clone()
        {
            throw new Exception("Not Implented");
        }

        public string Get_Description()
        {
            throw new Exception("Not Implented");
        }
    }



    abstract class FactoryCar
    {
        public string brand;
        public string complection, v;
        public Dictionary<string, CarInstruments> CarTypes = new Dictionary<string, CarInstruments>();
        public FactoryCar(string brand)
        {
            this.brand = brand;
        }
        public List<Auto> CreateCars(Options opt)
        { 
            
            return CarTypes[brand].Get_Cars(opt);
        }
        public virtual FactoryCar SetBrand(string brand)  { return this; }
        public virtual FactoryCar Complection(string complection){return this;}
        public virtual FactoryCar V(string v) {  return this;}
    }

    class BMW_Factory : FactoryCar
    {
        public BMW_Factory():base(brand: "BMW")
        {
            CarTypes.Add("BMW", new BMW_Product("Head"));
            
        }
        public override FactoryCar Complection(string complection)
        {
            base.complection = complection;
            return this;
        }
        public override FactoryCar V(string v)
        {
            base.v = v;
            return this;
        }

    }
    class Mercedes_Factory : FactoryCar
    {

        public Mercedes_Factory():base(brand:"Mercedes")
        {
            CarTypes.Add("Mercedes", new Mercedes_Product(subsidiary:"Head"));
            CarTypes.Add("Daimler", new Mercedes_Product(subsidiary: "Daimler"));
        }
        public override FactoryCar SetBrand(string brand)
        {
            


            base.brand = brand;
            return this;
        }
        public override FactoryCar Complection(string complection)
        {

            return this;
        }
        public override FactoryCar V(string v)
        {

            return this;
        }


    }
    class CarList 
    {
        public Dictionary<string, List<Auto>> cars;

        public CarList()
        {
            cars = new Dictionary<string, List<Auto>>();
        }
        public void Add(string brand, List<Auto> _cars)
        {
            this.cars.Add(brand, _cars);
        }
        public Dictionary<string, List<Auto>> Get_List()
        {
            return cars;
        }
        public void Display()
        {
            
        }
        public void Display(string brand)
        {

        }
        
    }


}
