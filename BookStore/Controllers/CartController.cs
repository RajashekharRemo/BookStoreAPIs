using BusinessLayer.Interfaces;
using CommonLayer.BookModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly IBookBusiness? _bookBusiness;
        public CartController (IBookBusiness bookBusiness)
        {
            _bookBusiness = bookBusiness;
        }


        [HttpPost("AddToCart")]
        public IActionResult AddCart(int UId, BookCart bookCart)
        {
            if (UId < 0 && bookCart == null)
            {
                return BadRequest();
            }

            bool flag = _bookBusiness.AddToCart(UId, bookCart);
            if (flag)
            {
                return Ok(new { message = "Added to Cart", result = true });
            }
            else
            {
                return BadRequest();
            }
        }


        [HttpGet("UserCartDetailsById")]
        public IActionResult cartDetails(int UId)
        {
            if (UId < 0)
            {
                return BadRequest();
            }

            var dataResult = _bookBusiness.UserCartDetails(UId);
            if (dataResult == null)
            {
                return NotFound(new { message = "Not Found", result = false });
            }
            else
            {
                int count = dataResult.Count;
                return Ok(new { message = "Details Found", result = true, data = dataResult, count = count });
            }
        }

        [HttpPut("UpdateCartBookQuantity")]
        public IActionResult UpdateCartBookQuantity(int Id, BookCart cart)
        {
            if (Id < 0 && cart == null)
            {
                return BadRequest(new { message = "Bad request", result = false });
            }

            var dataResult = _bookBusiness.UpdateBookQuantity(Id, cart);
            if (dataResult == null) { return NotFound(new { message = "Not Found", result = false }); }

            return Ok(new { message = "Details Found", result = true, data = dataResult });
        }

        [HttpGet("UserCartPrice")]
        public IActionResult UserCartPrice(int UId)
        {
            if (UId < 0)
            {
                return BadRequest(new { message = "Bad request", result = false });
            }

            double tprice = _bookBusiness.GetPriceInCart(UId);
            if (tprice > 0)
            {
                return Ok(new { message = "Details Found", result = true, data = tprice });
            }
            else
            {
                return NotFound(new { message = "Not Found", result = false });
            }
        }

        [HttpPost("RemoveCartItem")]
        public IActionResult DeleteCart(RemoveCartItem removeCartItem)
        {
            if(removeCartItem == null)
            {
                return BadRequest(new { message = "Bad request", result = false });
            }

            bool flag = _bookBusiness.RemoveCartItem(removeCartItem);

            if(flag)
            {
                return Ok(new {message="Removes Successfully", result=true});
            }
            else
            {
                return NotFound(new { message = "Not Found", result = false });
            }
        }

    }
}
