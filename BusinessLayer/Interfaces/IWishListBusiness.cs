using CommonLayer.BookModel;
using CommonLayer.WishList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interfaces
{
    public interface IWishListBusiness
    {
        public bool AddToWishList(WishListClass wishList);
        public List<Book> GetWishList(int UId);
        public bool RemoveWishListItem(RemoveCartItem removeCartItem);
    }
}
