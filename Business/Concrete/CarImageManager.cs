using Business.Abstract;
using Business.Constants;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
  public class CarImageManager : ICarImageService
    {
        private ICarImageDal _carImageDal;
        public CarImageManager(ICarImageDal carImageDal)
        {
            _carImageDal = carImageDal;
        }

       // [ValidationAspect(typeof(AddCarImageValidator))]
        public IResult Add(IFormFile image, CarImage img)
        {
            

            var result = BusinessRules.Run(CheckIfCarImageLimitExceded (img.CarId));
            if (result!=null)
            {
                return result;
            }
            img.ImagePath = FileUpload.Add(image);
            img.Date = DateTime.Now;
            _carImageDal.Add(img);
            return new SuccessResult(Messages.CarImageAdded);
        }

        public IResult Delete(CarImage entity)
        {
            var result = BusinessRules.Run();
            if (!result.Success)
            {
                return new ErrorResult();
            }
            _carImageDal.Delete(entity);
            return new SuccessResult();
        }

        public IDataResult<List<CarImage>> GetAll()
        {
            var result = _carImageDal.GetAll();
            if (result.Count<=0)
            {
                return new ErrorDataResult<List<CarImage>>();
            }
            else
            {
                return new SuccessDataResult<List<CarImage>>(result);
            }
        }

        public IDataResult<List<CarImage>> GetCarImageByCarId(int carId)
        {
            var result = _carImageDal.GetAll(c=>c.CarId==carId);
            if (result.Count <= 0)
            {
                return new ErrorDataResult<List<CarImage>>();
            }
            else
            {
                return new SuccessDataResult<List<CarImage>>(result);
            }
        }

        public IDataResult<CarImage> GetById(int id)
        {
            var result = _carImageDal.Get(c=>c.Id==id);
            if (result==null)
            {
                return new ErrorDataResult<CarImage>();
            }
            else
            {
                return new SuccessDataResult<CarImage>(result);
            }
        }
        

        public IResult Update(IFormFile image, CarImage carImage)
        {
            var result = BusinessRules.Run(CheckIfCarImageLimitExceded(carImage.CarId));
            if (result != null)
            {
                return result;
            }
            var carImg = _carImageDal.Get(x => x.Id == carImage.Id);
            carImg.Date = DateTime.Now;
            carImg.ImagePath = FileUpload.Add(image);
            FileUpload.Delete(carImage.ImagePath);
            _carImageDal.Update(carImg);
            return new SuccessResult();
        }

        private IResult CheckIfCarImageLimitExceded(int carId)
        {
            var result = _carImageDal.GetAll(c=>c.CarId==carId).Count;
            if (result>=5)
            {
                return new ErrorResult();
            }
            return new SuccessResult();
        }

        public IDataResult<CarImage> Get(int id)
        {
            throw new NotImplementedException();
        }
    }
}
