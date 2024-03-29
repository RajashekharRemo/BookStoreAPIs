using BusinessLayer.Interfaces;
using CommonLayer.Review;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class ReviewBusiness : IReviewBusiness
    {
        private readonly IReviewRepository _reviewRepository;
        public ReviewBusiness(IReviewRepository reviewRepository)
        {
            _reviewRepository = reviewRepository;
        }

        public bool AddReview(Review review)
        {
            return _reviewRepository.AddReview(review); 
        }

        public List<GetReview> GetReviews()
        {
            return _reviewRepository.GetReviews();
        }

    }
}
