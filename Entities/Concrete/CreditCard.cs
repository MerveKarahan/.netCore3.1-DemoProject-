using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete
{
   public class CreditCard :IEntity
    {
        public int CreditCardId { get; set; }
        public int UserId { get; set; }
        public string CardHolderName { get; set; }
        public string CardExprationDate { get; set; }
        public string CardNumber { get; set; }
        public string CVV { get; set; }
    }
}
