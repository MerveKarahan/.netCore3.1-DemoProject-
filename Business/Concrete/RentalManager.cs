using Business.Abstract;
using Business.Constants;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business.Concrete
{
    public class RentalManager : IRentalService
    {
        IRentalDal _rentalDal;

        public RentalManager(IRentalDal rentalDal )
        {
            _rentalDal = rentalDal;
            
            
        }

        public IResult Add(Rental rental)
        {
            _rentalDal.Add(rental);
            return new SuccessResult(Messages.RentalAdded);
        }
        public IResult CheckCarRentalAble(Rental rental)
        {
            var rentalCar = _rentalDal.GetAll(r => r.CarId == rental.CarId).OrderByDescending(q => q.Id).FirstOrDefault();
            if (rentalCar==null)
            {
                return new SuccessResult();
            }

            if (rentalCar.RentDate<rental.RentDate && rentalCar.ReturnDate<rental.RentDate)
                return new SuccessResult();

            
            return new ErrorResult(Messages.CarNotRent);
        }
        public IResult Delete(Rental rental)
        {
            _rentalDal.Delete(rental);
            return new SuccessResult(Messages.RentalDeleted);
        }

        public IDataResult<List<Rental>> GetAll()
        {
            if (DateTime.Now.Hour == 00)
            {
                return new ErrorDataResult<List<Rental>>(Messages.MaintenanceTime);
            }
            return new SuccessDataResult<List<Rental>>(_rentalDal.GetAll(), Messages.RentalListed);
        }

        public IDataResult<Rental> GetById(int rentalId)
        {
            return new SuccessDataResult<Rental>(_rentalDal.Get(b => b.Id== rentalId));
        }

      

        public IDataResult<List<RentalDetailDto>> GetRentalDetails()
        {
            throw new NotImplementedException();
        }

        public IDataResult<List<Rental>> GetRentalDetailsByCustomerId(int id)
        {
            return new SuccessDataResult<List<Rental>>(_rentalDal.GetAll(c=>c.CustomerId == id));
        }

        public IDataResult<RentalDetailDto> GetRentalDetailsById(int RentalId)
        {
            return new SuccessDataResult<RentalDetailDto>(_rentalDal.GetRentalDetailsById(RentalId));
        }

        public IDataResult<List<RentalListDto>> GetRentalList()
        {
            return new SuccessDataResult<List<RentalListDto>>
                (_rentalDal.GetRentalList(),Messages.RentalListed);
        }

        public IResult TransactionalOperation(Rental rental)
        {
            _rentalDal.Add(rental);
            return new SuccessResult(Messages.CarRentalSuccess);
        }

        public IResult Update(Rental rental)
        {
            _rentalDal.Update(rental);
            return new SuccessResult(Messages.RentalUpdated);
        }

    }
}
