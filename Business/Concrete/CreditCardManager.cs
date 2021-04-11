using Business.Abstract;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class CreditCardManager : ICreditCardService
    {
        ICreditCardDal _creditCardDal;
        public CreditCardManager(ICreditCardDal creditCardDal)
        {
            _creditCardDal = creditCardDal;
        }
        public IResult Add(CreditCard creditCard)
        {
            _creditCardDal.Add(creditCard);
            return new SuccessResult();
        }

        public IDataResult<CreditCard> GetCreditCardById(int CreditCardId)
        {
            return new SuccessDataResult<CreditCard>(_creditCardDal.Get(c => c.CreditCardId == CreditCardId));
        }

        public IDataResult<CreditCard> GetCreditCardByUserId(int UserId)
        {
            return new SuccessDataResult<CreditCard>(_creditCardDal.Get(c => c.UserId == UserId));
        }
    }
}
