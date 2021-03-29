using Business.Concrete;
using DataAccess.Concrete.EntityFramework;
using DataAccess.Concrete.InMemory;
using Entities.Concrete;
using System;

namespace ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            //ProductTest();
            //BrandTest();

        }

        private static void BrandTest()
        {
            BrandManager brandManager = new BrandManager(new EfBrandDal());
            foreach (var brand in brandManager.GetAll())
            {
                Console.WriteLine(brand.BrandName);
            }
        }

        private static void CarTest()
        {
            BrandManager brandManager = new BrandManager(new EfBrandDal());
            brandManager.Add(new Brand { BrandName = "Hyundai" });
            ColorManager colorManager = new ColorManager(new EfColorDal());
            colorManager.Add(new Color { ColorName = "Mavi" });


            //CarManager carManager = new CarManager(new EfCarDal());
            //var result = carManager.GetCarDetails();
            //if (result.Success == true)
            //{
            //    foreach (var car in result.Data)
            //    {
            //        Console.WriteLine(car.CarName + "/" + car.CarName);
            //    }
            //}
            //else
            //{
            //    Console.WriteLine(result.Message);
            //}

            //carManager.Add(new Car { BrandId = 1, ColorId = 1, Description = "büyük araç", CarName = "bilmiyom", DailyPrice = 500, ModelYear = 2019 });
            //foreach (var car in carManager.GetCarDetails().Data)
            //{
            //    Console.WriteLine(car.CarId);
            //}
        }
    }
}
