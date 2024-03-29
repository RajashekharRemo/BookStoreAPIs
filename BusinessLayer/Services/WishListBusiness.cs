using BusinessLayer.Interfaces;
using CommonLayer.BookModel;
using CommonLayer.WishList;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class WishListBusiness : IWishListBusiness
    {

        private readonly IWishList _wishList;
        public WishListBusiness(IWishList wishList)
        {
            _wishList = wishList;
        }

        public bool AddToWishList(WishListClass wishList)
        {
            return _wishList.AddToWishList(wishList);
        }

        public List<Book> GetWishList(int UId)
        {
            return _wishList.GetWishList(UId);
        }

        public bool RemoveWishListItem(RemoveCartItem removeCartItem)
        {
            return _wishList.RemoveWishListItem(removeCartItem);
        }

    }
}
