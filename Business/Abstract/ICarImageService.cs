using Core.Utilities.Results;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{

    public interface ICarImageService
    {
        IDataResult<List<CarImage>> GetAll();
        IDataResult<CarImage> Get(int id);
        IDataResult<List<CarImage>> GetCarImageByCarId(int carId);
        IResult Add(IFormFile image, CarImage img);
        IResult Update(IFormFile file, CarImage carImage);
        IResult Delete(CarImage carImage);

    }
}
