using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using Core.Utilities.Security.Hashing;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{

    public class UserManager : IUserService
    {
        private readonly IUserDal _userDal;

        public UserManager(IUserDal userDal)
        {
            _userDal = userDal;
        }

        [ValidationAspect(typeof(UserValidator))]
        //[CacheRemoveAspect("IUserService.Get")]
        //[SecuredOperation("User.Add")]
        public IResult Add(User user)
        {
            _userDal.Add(user);
            return new SuccessResult(Messages.UserAdded);
        }

        [SecuredOperation("User.Delete")]
        public IResult Delete(User user)
        {
            _userDal.Delete(user);
            return new SuccessResult(Messages.UserDeleted);
        }

        //[CacheAspect]
        public IDataResult<List<User>> GetAll()
        {
            if (DateTime.Now.Hour == 00)
            {
                return new ErrorDataResult<List<User>>(Messages.MaintenanceTime);
            }
            return new SuccessDataResult<List<User>>(_userDal.GetAll(), Messages.UserListed);
        }

        //[CacheAspect]
        //[PerformanceAspect(5)]
        public IDataResult<UserInformationDto> GetById(int id)
        {
              
            return new SuccessDataResult<UserInformationDto>(_userDal.GetById(id));
        }

        public IResult Update(User user)
        {
            _userDal.Update(user);
            return new SuccessResult(Messages.UserUpdated);
        }

        //[CacheAspect]
        public IDataResult<List<OperationClaim>> GetClaims(User user)
        {
            return new SuccessDataResult<List<OperationClaim>>(_userDal.GetClaims(user));
        }

        //[CacheAspect]
        //[PerformanceAspect(5)]
        public IDataResult<User> GetByMail(string email)
        {
            return new SuccessDataResult<User>(_userDal.Get(u => u.Email == email));
        }

        public IResult ChangePassword(ChangePasswordDto changePasswordDto)
        {
            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(changePasswordDto.Password, out passwordHash, out passwordSalt);
            var user = _userDal.Get(q => q.Id == changePasswordDto.UserId);
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            _userDal.Update(user);
            return new SuccessResult(Messages.PasswordUpdated);

        }
        public IResult CheckPasswordMatch(string password, string passwordConfirm)
        {
            if (password !=passwordConfirm)
            {
                return new ErrorResult(Messages.PasswordNotMatch);
            }
            return new SuccessResult();
        }

        public IResult ChangeUserInformation(UserInformationDto userInformationDto)
        {
            var user = _userDal.Get(q => q.Id == userInformationDto.UserId);
            user.Email = userInformationDto.Email;
            user.FirstName = userInformationDto.FirstName;
            user.LastName = userInformationDto.LastName;
            _userDal.Update(user);
            return new SuccessResult(Messages.UserUpdated);
        }

      
    }
}
