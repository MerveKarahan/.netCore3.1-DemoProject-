using Business.Abstract;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.Entities;
using Core.Utilities.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class PaymentManager : IPaymentService
    {
        [ValidationAspect(typeof(PaymentValidator))]
        //[PerformanceAspect(5)]
       // [TransactionScopeAspect]
        public IResult MakePayment(IPaymentModel paymentModel)
        {
            return new SuccessResult();
        }

    }
}
