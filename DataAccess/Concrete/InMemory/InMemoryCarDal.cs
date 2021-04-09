using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace DataAccess.Concrete.InMemory
{
    public class InMemoryCarDal : ICarDal
    {
        List<Car> _cars;
        public InMemoryCarDal()
        {
            _cars = new List<Car> {
                new Car{CarId=1, BrandId=1, ColorId=4, DailyPrice=500, Description="Büyük Araç", ModelYear=2019 } ,
                new Car{CarId=2, BrandId=2, ColorId=5, DailyPrice=600, Description="Orta Boy Araç", ModelYear=2018},
                new Car{CarId=3, BrandId=2, ColorId=7, DailyPrice=790, Description="Station Vagon Araç", ModelYear=2017 },
                new Car{CarId=4, BrandId=4, ColorId=8, DailyPrice=400, Description="Premium Araç", ModelYear=2020 },
                new Car{CarId=5, BrandId=4, ColorId=5, DailyPrice=750, Description="Minivan", ModelYear=2016 },


            };
        }
        public void Add(Car car)
        {
            _cars.Add(car);
        }

        public void Delete(Car car)
        {
            Car carToDelete =_cars.SingleOrDefault(c=>c.CarId==car.CarId);
            _cars.Remove(carToDelete);
        }

        public Car Get(Expression<Func<Car, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public List<Car> GetAll()
        {
            return _cars;
        }

        public List<Car> GetAll(Expression<Func<Car, bool>> filter = null)
        {
            throw new NotImplementedException();
        }

        public List<Car> GetAllByBrand(int brandId)
        {
           return _cars.Where(c => c.BrandId == brandId).ToList();
        }

        public List<CarDetailDto> GetAllCars()
        {
            throw new NotImplementedException();
        }

        public List<CarDetailDto> GetCarDetails(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(Car car)
        {
            Car carToUpdate = _cars.SingleOrDefault(c => c.CarId == car.CarId);
            carToUpdate.CarId = car.CarId;
            carToUpdate.BrandId = car.BrandId;
            carToUpdate.ModelYear = car.ModelYear;
            carToUpdate.ColorId = car.ColorId;
            carToUpdate.DailyPrice = car.DailyPrice;
            carToUpdate.Description = car.Description;
        }

        CarDetailDto ICarDal.GetCarDetails(int id)
        {
            throw new NotImplementedException();
        }
    }
}
