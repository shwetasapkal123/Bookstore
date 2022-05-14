using Database_Layer;
using System;
using System.Collections.Generic;
using System.Text;

namespace Buisness_Layer.Interface
{
    public interface IBookBL
    {
        public BookModel AddBook(BookModel book);
        public UpdateBook UpdateBook(UpdateBook update);
        public bool DeleteBook(int bookId);
        public BookModel GetBookByBookId(int BookId);
        public List<BookModel> GetAllBooks();

    }
}
