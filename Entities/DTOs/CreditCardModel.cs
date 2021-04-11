using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.DTOs
{

    public class CreditCardModel 
    {
        public string CardHolderName { get; set; }
        public string CardExprationDate{ get; set; }
        public string CardNumber { get; set; }
        public string Cvv { get; set; }
    }
}
