﻿using Business.Abstract;
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
        public IResult Add(CarImagesDto carImagesDto)
        {
            var result = BusinessRules.Run(CheckIfCarImageLimitExceded(carImagesDto.CarId));
            if (result!=null)
            {
                return result;
            }
            CarImage carImage = new CarImage
            {
                CarId = carImagesDto.CarId,
                ImagePath = FileUpload.Upload(carImagesDto.ImageFile, "\\image\\").Message,
                Date = DateTime.Now
            };
            carImage.Date = DateTime.Now;
            _carImageDal.Add(carImage);
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
            var imageResult = FileUpload.Update(image, "\\image\\").Message;
            carImage.ImagePath = imageResult;
            carImage.Date = DateTime.Now;
            _carImageDal.Add(carImage);
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
    }
}
