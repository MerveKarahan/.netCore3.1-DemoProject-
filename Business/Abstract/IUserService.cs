using Core.Entities.Concrete;
using Core.Utilities.Results;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface IUserService
    {
        IResult Add(User user);
        IResult Update(User user);
        IResult Delete(User user);
        IResult ChangeUserInformation(UserInformationDto userInformationDto);
        IDataResult<List<User>> GetAll();
        IDataResult<UserInformationDto> GetById(int userId);

        IDataResult<List<OperationClaim>> GetClaims(User user);
        IDataResult<User> GetByMail(string email);
        IResult ChangePassword(ChangePasswordDto changePasswordDto);
        IResult CheckPasswordMatch(string password, string passwordConfirm);
    }
}
