using Database_Layer;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository_Layer.Interface
{
    public interface IBookRL
    {
        public BookModel AddBook(BookModel book);
        public UpdateBook UpdateBook(UpdateBook update);
        public bool DeleteBook(int bookId);
        public BookModel GetBookByBookId(int BookId);
        public List<BookModel> GetAllBooks();
    }
}
