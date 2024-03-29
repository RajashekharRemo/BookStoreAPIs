using BusinessLayer.Interfaces;
using BusinessLayer.Services;
using CommonLayer.BookModel;
using CommonLayer.WishList;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WishListController : ControllerBase
    {
        private readonly IWishListBusiness _wishlistBusiness;
        public WishListController(IWishListBusiness wishlistBusiness)
        {
            _wishlistBusiness = wishlistBusiness;
        }

        [HttpGet("GetWishListByID")]
        public IActionResult GetAllWishListBiId(int UId)
        {
            if(UId<0)
            {
                return BadRequest();
            }

            var dataResult=_wishlistBusiness.GetWishList(UId);
            if(dataResult == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(new {data=dataResult, result=true, message="Data Fount", count=dataResult.Count});
            }
        }

        [HttpPost("AddToWishList")]
        public IActionResult AddToWistList(WishListClass wishListClass)
        {
            if(wishListClass==null)
            {
                return BadRequest();
            }

            bool flag = _wishlistBusiness.AddToWishList(wishListClass);
            if(!flag)
            {
                return BadRequest(new { message = "Not Added", result = false });
            }
            else
            {
                return Ok(new {message="Added Successfullly", result=true});
            }
        }


        [HttpPost("RemoveWishListItem")]
        public IActionResult DeleteCart(RemoveCartItem removeCartItem)
        {
            if (removeCartItem == null)
            {
                return BadRequest(new { message = "Bad request", result = false });
            }

            bool flag = _wishlistBusiness.RemoveWishListItem(removeCartItem);

            if (flag)
            {
                return Ok(new { message = "Removed Successfully", result = true });
            }
            else
            {
                return NotFound(new { message = "Not Found", result = false });
            }
        }
    }
}
