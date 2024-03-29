using CommonLayer.BookModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interfaces
{
    public interface IBookBusiness
    {
        public Book GetBookById(int Id);
        public Book GetBookByTitle(string Title);
        public List<Book> GetAllBooks();
        public List<Book> GetBooksByAutherName(string Auther);
        public Book GetBookByTitleandAuther(string Title, string Auther);
        public bool CreateBook(InsertBook book);

        public bool UpdateBook(Book book);
        //======================================================================================
        public bool AddToCart(int UId, BookCart cart);
        public List<UserCartDetails> UserCartDetails(int UId);
        public BookCart UpdateBookQuantity(int UId, BookCart cart);
        public double GetPriceInCart(int userId);
        public bool RemoveCartItem(RemoveCartItem removeCartItem);
    }
}
