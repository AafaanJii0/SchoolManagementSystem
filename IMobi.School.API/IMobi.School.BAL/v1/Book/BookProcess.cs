using AutoMapper;
using IMobi.School.BAL.Base;
using IMobi.School.Config;
using IMobi.School.DAL.Context;
using IMobi.School.DomainModal.v1.Book;
using IMobi.School.ServiceModal.v1.Book;
using Microsoft.EntityFrameworkCore;

namespace IMobi.School.BAL.v1.Book
{
    public class BookProcess : BaseProcess
    {
        #region Constructor
        public BookProcess(AppDbContext appDbContext, IMapper mapper)
            : base(appDbContext, mapper)
        {

        }
        #endregion Constructor

        #region Get All
        public async Task<List<BookSM>?> GetBooks()
        {
            var booksDm = await _appDbContext.Books.ToListAsync();
            if (booksDm != null)
                return _mapper.Map<List<BookSM>>(booksDm);
            return null;
        }
        #endregion Get All

        #region Get Single
        public async Task<BookSM?> GetBookById(int id)
        {
            var bookDm = await _appDbContext.Books.FindAsync(id);
            if (bookDm != null)
                return _mapper.Map<BookSM>(bookDm);
            return null;
        }
        #endregion Get Single

        #region Add
        public async Task<BookSM?> AddBook(BookSM bookSM)
        {
            if (bookSM == null)
                throw new IMobiException("Incoming bookSM was null", "Incoming bookSM was null");
            var bookDm = _mapper.Map<BookDM>(bookSM);

            bookDm.CreatedBy = "Admin";   //TODO: later can be replaced by logged in user
            bookDm.CreatedDateUTC = DateTime.Now;

            await _appDbContext.Books.AddAsync(bookDm);
            try
            {
                if (await _appDbContext.SaveChangesAsync() > 0)
                    return _mapper.Map<BookSM>(bookDm);
            }
            catch (Exception ex)
            {
                throw new IMobiException(ex.Message, "Book cannot be saved, please try again.", ex?.InnerException);
            }
            return null;
        }
        #endregion Add
    }
}
