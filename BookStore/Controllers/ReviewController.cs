using BusinessLayer.Interfaces;
using CommonLayer.Review;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewBusiness _reviewBusiness;
        public ReviewController(IReviewBusiness reviewBusiness)
        {
            _reviewBusiness = reviewBusiness;
        }

        [HttpPost("AddReview")]
        public IActionResult AddReview(Review review)
        {
            if (review == null)
            {
                return BadRequest();
            }

            bool flag=_reviewBusiness.AddReview(review);
            if(!flag) { 
                return BadRequest(new { message = "Not Added", result = false });
            }
            else
            {
                return Ok(new { message = "Added Successfullly", result = true });
            }

        }

        [HttpGet("GetAllReviews")]
        public IActionResult GetReviews()
        {

            var dataResult=_reviewBusiness.GetReviews();
            if(dataResult == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(new {data=dataResult, result=true});
            }

        }

    }
}
