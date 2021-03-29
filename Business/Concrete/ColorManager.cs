using Business.Abstract;
using Business.Constants;
using Core.Aspects.Autofac.Caching;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class ColorManager : IColorService

    {
        IColorDal _colorDal;

        public ColorManager(IColorDal colorDal)
        {
            _colorDal = colorDal;
        }

        [CacheRemoveAspect("IColorService.Get")]
        public IResult Add(Color color)
        {

            _colorDal.Add(color);
            return new SuccessResult();
        }

        [CacheRemoveAspect("IColorService.Get")]
        public IResult Delete(Color color)
        {

            _colorDal.Delete(color);
            return new SuccessResult();
        }
        [CacheAspect]
        public IDataResult<List<Color>> GetAll()
        {

            
            return new SuccessDataResult<List<Color>>(_colorDal.GetAll());

        }
        [CacheAspect]
        public IDataResult<Color> GetById(int colorId)
        {
            
            return new SuccessDataResult<Color>(_colorDal.Get(c=>c.ColorId == colorId),Messages.ColorGet);
        }

        [CacheRemoveAspect("IColorService.Get")]
        public IResult Update(Color color)
        {

            _colorDal.Update(color);
            return new SuccessResult(Messages.ColorUpdated);
        }
    }
}
