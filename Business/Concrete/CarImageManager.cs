using Business.Abstract;
using Business.Constants;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
  public class CarImageManager : ICarImageService
    {
        private ICarImageDal _carImageDal;
        private IHostingEnvironment _hostingEnvironment;
        public CarImageManager(ICarImageDal carImageDal, IHostingEnvironment hostingEnvironment)
        {
            _carImageDal = carImageDal;
            _hostingEnvironment = hostingEnvironment;
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
            string webRootPath = _hostingEnvironment.WebRootPath;
            
            
            var fullPath = webRootPath + "/Images/" + entity.ImagePath;
            var result = FileUpload.Delete(fullPath);
            if (result)
            {
                _carImageDal.Delete(entity);
                return new SuccessResult();
            }
            return new ErrorResult("Resim silinemedi");
            
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
            var carImg = _carImageDal.Get(x => x.Id == carImage.Id);
            string webRootPath = _hostingEnvironment.WebRootPath;


            var fullPath = webRootPath + "/Images/" + carImg.ImagePath;
            
            var result = BusinessRules.Run(CheckIfCarImageLimitExceded(carImage.CarId));
            if (result != null)
            {
                return result;
            }
            
            carImg.Date = DateTime.Now;
            carImg.ImagePath = FileUpload.Add(image);
           FileUpload.Delete(fullPath);
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
            var result = _carImageDal.Get(c => c.Id == id);
            
                return new SuccessDataResult<CarImage>(result);
           
        }
    }
}
