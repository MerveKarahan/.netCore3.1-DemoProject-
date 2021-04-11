using Core.Utilities.Results;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface ICreditCardService
    {
        IResult Add(CreditCard creditCard);
        IDataResult<CreditCard> GetCreditCardById(int CreditCardId);
        IDataResult<CreditCard> GetCreditCardByUserId(int UserId);
    }
}
