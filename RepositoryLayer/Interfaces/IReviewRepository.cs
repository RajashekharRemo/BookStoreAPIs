﻿using CommonLayer.Review;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Interfaces
{
    public interface IReviewRepository
    {
        public bool AddReview(Review review);
        public List<GetReview> GetReviews();//int BId
    }
}
