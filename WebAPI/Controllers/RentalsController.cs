using Business.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RentalsController : ControllerBase
    {
        private readonly IRentalService _rentalService;
       

        public RentalsController(IRentalService rentalService)
        {
            _rentalService = rentalService;
     
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _rentalService.GetAll();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("getrentallist")]
        public IActionResult GetRentalList()
        {
            var result = _rentalService.GetRentalList();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("id")]
        public IActionResult GetById(int id)
        {
            var result = _rentalService.GetById(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("add")]
        public IActionResult Add(RentalPaymentDto rentalPaymentDto)
        {
            
            var result = _rentalService.Add(rentalPaymentDto.Rental);

            if (result.Success)
                return Ok(result);

            return BadRequest(result.Message);
        }

        [HttpPost("carCheck")]
        public IActionResult CarCheck(Rental rental)
        {
            
            var result = _rentalService.CheckCarRentalAble(rental);

            if (result.Success)
                return Ok(result);

            return BadRequest(result);
        }
        [HttpPost("delete")]
        public IActionResult Delete(Rental rental)
        {
            var result = _rentalService.Delete(rental);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("update")]
        public IActionResult Update(Rental rental)
        {
            var result = _rentalService.Update(rental);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }


        [HttpGet("detailsbycar")]
        public IActionResult GetRentalByCar(int id)
        {
            var result = _rentalService.GetRentalDetailsById(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("detailsbycustomer")]
        public IActionResult GetRentalByCustomer(int id)
        {

            var result = _rentalService.GetRentalDetailsByCustomerId(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
