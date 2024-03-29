using BusinessLayer.Interfaces;
using CommonLayer.BookModel;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class BookBusiness : IBookBusiness
    {
        private readonly IBookRepository _bookRepository;
        public BookBusiness(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public Book GetBookById(int Id)
        {
            return _bookRepository.GetBookById(Id);
        }
        public Book GetBookByTitle(string Title)
        {
            return _bookRepository.GetBookByTitle(Title);
        }
        public List<Book> GetAllBooks()
        {
            return _bookRepository.GetAllBooks();
        }

        public List<Book> GetBooksByAutherName(string Auther)
        {
            return _bookRepository.GetBooksByAutherName(Auther);
        }

        public Book GetBookByTitleandAuther(string Title, string Auther)
        {
            return _bookRepository.GetBookByTitleandAuther(Title, Auther);
        }

        public bool CreateBook(InsertBook book)
        {
            return _bookRepository.CreateBook(book);
        }

        public bool UpdateBook(Book book)
        {
            return _bookRepository.UpdateBook(book);
        }

        //=================================================================================================================
        public bool AddToCart(int UId, BookCart cart)
        {
            return _bookRepository.AddToCart(UId, cart);
        }

        public List<UserCartDetails> UserCartDetails(int UId)
        {
            return _bookRepository.UserCartDetails(UId);
        }

        public BookCart UpdateBookQuantity(int UId, BookCart cart)
        {
            return _bookRepository.UpdateBookQuantity(UId, cart);
        }

        public double GetPriceInCart(int userId)
        {
            return _bookRepository.GetPriceInCart(userId);
        }
        public bool RemoveCartItem(RemoveCartItem removeCartItem)
        {
            return _bookRepository.RemoveCartItem(removeCartItem);
        }
    }
}
