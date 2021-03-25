using Core.Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.ValidationRules.FluentValidation
{
    public class PaymentValidator : AbstractValidator<CreditCardModel>
    {
        public PaymentValidator()
        {
            RuleFor(f => f.CardNumber).NotEmpty()
                .MinimumLength(16).MaximumLength(16);

            RuleFor(f => f.CardHolderName).NotEmpty()
                .MaximumLength(50);

            RuleFor(f => f.ExpirationMonth).NotEmpty()
                .LessThan(13).GreaterThan(0);

            RuleFor(f => f.Cvv).NotEmpty()
                .MinimumLength(3).MaximumLength(3);

            RuleFor(p => p.ExpirationYear).NotEmpty()
                .LessThan(DateTime.Now.AddYears(30).Year).GreaterThan(DateTime.Now.Year);
        }
    }
}
