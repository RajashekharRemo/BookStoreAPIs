using BusinessLayer.Interfaces;
using CommonLayer.OrderAddress;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderAddressController : ControllerBase
    {
        private readonly IOrderAddressBusiness _orderAddressBusiness;

        public OrderAddressController(IOrderAddressBusiness orderAddressBusiness)
        {
            _orderAddressBusiness = orderAddressBusiness;
        }

        [HttpPost("AddOrder")]
        public IActionResult AddOrder(int UId, OrderToDB orderToDB)
        {
            if(UId<0 && orderToDB == null)
            {
                return BadRequest(new {message="Enter data correctly", result=false});
            }

            bool flag=_orderAddressBusiness.AddOrder(UId, orderToDB);
            if(flag)
            {
                Random rnd = new Random();
                int num = rnd.Next(1000000000, 2000000000);
                return Ok(new {message="Order success", result=true, id = num});
            }
            else { return BadRequest(new { message = "Enter data correctly", result = false }); }
        }

        [HttpGet("GetAllOrders")]
        public IActionResult GetAllOrders(int UId) {
            if (UId < 0 )
            {
                return BadRequest();
            }

            var dataResult=_orderAddressBusiness.GetAllOrderes(UId);
            if(dataResult != null)
            {
                return Ok(dataResult);
            }else
            {
                return NotFound();
            }
        }


        [HttpPost("AddUserAddress")]
        public IActionResult AddAddress(int UId, UpdateAddress address)
        {
            if(UId<0 && address == null) { return BadRequest(new {message="Please pass required data", result=false}); }

            bool flag=_orderAddressBusiness.AddAddress(UId, address);

            if(flag)
            {
                return Ok(new { message = "Added successfully", result = true, data=address });
            }else { return BadRequest(new { message = "Please pass correct data", result = false }); }
        }

        [HttpPut("UpdateUserAddress")]
        public IActionResult UpdateAddress(int UId, Address address)
        {
            if (UId < 0 && address == null) { return BadRequest(new { message = "Please pass required data", result = false }); }

            bool flag = _orderAddressBusiness.UpdateAddress(UId, address);

            if (flag)
            {
                return Ok(new { message = "Updated Successfully", result = true });
            }
            else { return BadRequest(new { message = "Please pass correct data", result = false }); }
        }

        [HttpGet("GetAddressById")]
        public IActionResult GetAddresses(int UId)
        {
            if (UId < 0)
            {
                return BadRequest(new { message = "Please pass required data", result = false });
            }
            var dataResult = _orderAddressBusiness.GetAddresses(UId);
            if (dataResult != null)
            {
                return Ok(dataResult);
            }else { return NotFound(new { message = "User not found", result = false }); }
        }
    }
}
