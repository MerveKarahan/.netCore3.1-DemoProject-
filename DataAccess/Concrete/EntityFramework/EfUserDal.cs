using Core.DataAccess.EntityFramework;
using Core.Entities.Concrete;
using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Entities.DTOs;

namespace DataAccess.Concrete.EntityFramework
{

    public class EfUserDal : EfEntityRepositoryBase<User, ReCapContext>, IUserDal
    {
        public UserInformationDto GetById(int Id)
        {
            using (var context = new ReCapContext())
            {
                var result = context.Users.Select(q => new UserInformationDto
                {
                    UserId = q.Id,
                    FirstName = q.FirstName,
                    LastName = q.LastName,
                    Email = q.Email



                }).FirstOrDefault(q => q.UserId == Id);

                return result;
            }
        }

        public List<OperationClaim> GetClaims(User user)
        {
            using (var context = new ReCapContext())
            {
                var result = from operationClaim in context.OperationClaims
                             join userOperationClaim in context.UserOperationsClaims
                                 on operationClaim.Id equals userOperationClaim.OperationClaimId
                             where userOperationClaim.UserId == user.Id
                             select new OperationClaim { Id = operationClaim.Id, Name = operationClaim.Name };
                return result.ToList();

            }
        }
    }
}
