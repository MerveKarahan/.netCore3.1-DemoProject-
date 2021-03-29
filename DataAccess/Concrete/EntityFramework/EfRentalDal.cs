using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfRentalDal : EfEntityRepositoryBase<Rental, ReCapContext>, IRentalDal
    {
        private readonly ReCapContext _reCapContext;
        public EfRentalDal(ReCapContext reCapContext)
        {
            _reCapContext = reCapContext;
        }

        public RentalDetailDto GetRentalDetailsById(int rentalId)
        {

            var result = (from rent in _reCapContext.Rentals
                          join customer in _reCapContext.Customers
                          on rent.CustomerId equals customer.CustomerId
                          join car in _reCapContext.Cars
                          on rent.CarId equals car.CarId
                          join user in _reCapContext.Users
                          on customer.UserId equals user.Id
                          where rent.Id == rentalId
                          select new RentalDetailDto
                          {
                              CarId = rent.CarId,
                              CarName = car.CarName,
                              CompanyName = customer.CompanyName,
                              FirstName = user.FirstName,
                              LastName = user.LastName,
                              RentalId = rent.Id,
                              RentDate = rent.RentDate,
                              ReturnDate = rent.ReturnDate,
                              UserId = user.Id

                          }).FirstOrDefault();
            return result;
        }

    }
}
