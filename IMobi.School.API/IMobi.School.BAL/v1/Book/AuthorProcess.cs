using AutoMapper;
using IMobi.School.BAL.Base;
using IMobi.School.Config;
using IMobi.School.DAL.Context;
using IMobi.School.DomainModal.v1.Book;
using IMobi.School.ServiceModal.v1.Book;
using Microsoft.EntityFrameworkCore;

namespace IMobi.School.BAL.v1.Book
{
    public class AuthorProcess : BaseProcess
    {
        #region Constructor
        public AuthorProcess(AppDbContext appDbContext, IMapper mapper)
            : base(appDbContext, mapper)
        {
        }
        #endregion Contructor

        #region Get All
        public async Task<List<AuthorSM>?> GetAuthors()
        {
            var authorDm = await _appDbContext.Authors.ToListAsync();
            if (authorDm != null)
                return _mapper.Map<List<AuthorSM>>(authorDm);
            return null;
        }
        #endregion Get All

        #region Get Single
        public async Task<AuthorSM?> GetAuthorById(int id)
        {
            var authorDm = await _appDbContext.Authors.FindAsync(id);
            if (authorDm != null)
                return _mapper.Map<AuthorSM>(authorDm);
            return null;
        }
        #endregion Get Single

        #region Add
        public async Task<AuthorSM?> AddAuthor(AuthorSM authorSM)
        {
            if (authorSM == null)
                throw new IMobiException("Incoming bookSM was null", "Incoming data was null");
            var authorDm = _mapper.Map<AuthorDM>(authorSM);

            authorDm.CreatedBy = "Admin";   //TODO: later can be replaced by logged in user
            authorDm.CreatedDateUTC = DateTime.Now;

            await _appDbContext.Authors.AddAsync(authorDm);
            try
            {
                if (await _appDbContext.SaveChangesAsync() > 0)
                    return _mapper.Map<AuthorSM>(authorDm);
            }
            catch (Exception ex)
            {
                throw new IMobiException(ex.Message, "Author cannot be saved, please try again.", ex?.InnerException);
            }
            return null;
        }
        #endregion Add
    }
}
