using IMobi.School.DAL.Context;
using IMobi.School.DomainModal.v1.Book;

namespace IMobi.School.DAL.Seeder
{
    public class Seeder<T> where T : AppDbContext
    {
        /*  public void SetupDatabaseWithSeedData(ModelBuilder modelBuilder)
          {
              var defaultCreatedBy = "SeedAdmin";

              SeedDummyData(modelBuilder, defaultCreatedBy);
          }*/

        public async Task<bool> SeedDummyData(T context)
        {
            string defaultCreatedBy = "Seed";
            DateTime defaultCreatedDateUTC = DateTime.UtcNow;
            var apiDb = context as AppDbContext;

            if (apiDb != null && apiDb.Authors.Count() <= 0)
            {
                SeedAuthors(apiDb, defaultCreatedBy, defaultCreatedDateUTC);
                SeedBooks(apiDb, defaultCreatedBy, defaultCreatedDateUTC);
                return true;
            }
            return false;
        }

        #region Dummy Data
        /* private void SeedDummyData(ModelBuilder modelBuilder, string defaultCreatedBy)
         {
             modelBuilder.Entity<DummyTeacherDM>().HasData(
                 new DummyTeacherDM() { Id = 1, FirstName = "Teacher A", LastName = "Khan", CreatedBy = defaultCreatedBy },
                 new DummyTeacherDM() { Id = 2, FirstName = "Teacher B", LastName = "Kumar", CreatedBy = defaultCreatedBy },
                 new DummyTeacherDM() { Id = 3, FirstName = "Teacher C", LastName = "Johar", CreatedBy = defaultCreatedBy }
                 );

             modelBuilder.Entity<DummySubjectDM>().HasData(
                 new DummySubjectDM() { Id = 1, SubjectName = "Physics", SubjectCode = "phy", DummyTeacherID = 1, CreatedBy = defaultCreatedBy },
                 new DummySubjectDM() { Id = 2, SubjectName = "Chemistry", SubjectCode = "chem", DummyTeacherID = 2, CreatedBy = defaultCreatedBy },
                 new DummySubjectDM() { Id = 3, SubjectName = "Biology", SubjectCode = "bio", DummyTeacherID = 1, CreatedBy = defaultCreatedBy }
             );
         }*/
        #endregion Dummy Data

        #region Authors
        private void SeedAuthors(AppDbContext appDbContext, string defaultCreatedBy, DateTime defaultCreatedDateUTC)
        {
            List<AuthorDM> authors = new List<AuthorDM>()
            {
                new AuthorDM()
                {
                    Name = "Aafaan Jii",
                    Address = "Pampore",
                    CreatedBy = defaultCreatedBy,
                    CreatedDateUTC = defaultCreatedDateUTC,
                },
                 new AuthorDM()
                {
                    Name = "Salmaan Mushtaq",
                    Address = "Meej Pampore",
                    CreatedBy = defaultCreatedBy,
                    CreatedDateUTC = defaultCreatedDateUTC,
                },
                  new AuthorDM()
                {
                    Name = "Ifra Nazir",
                    Address = "Khanyar Srinagar",
                    CreatedBy = defaultCreatedBy,
                    CreatedDateUTC = defaultCreatedDateUTC,
                },
            };
            appDbContext.Authors.AddRange(authors);
            appDbContext.SaveChanges();
        }
        #endregion Authors

        #region Books
        private void SeedBooks(AppDbContext appDbContext, string defaultCreatedBy, DateTime defaultCreatedDateUTC)
        {
            List<BookDM> books = new List<BookDM>()
            {
                new BookDM()
                {
                    Title = "C#",
                    Publisher = "Arihant",
                    PublishedDate = DateTime.UtcNow,
                    AuthorId = 1,
                    Price = 500,
                    CreatedBy = defaultCreatedBy,
                    CreatedDateUTC = defaultCreatedDateUTC,
                },
                new BookDM()
                {
                    Title = "World Geography",
                    Publisher = "Oxford Press",
                    PublishedDate = DateTime.UtcNow,
                    AuthorId = 1,
                    Price = 1500,
                    CreatedBy = defaultCreatedBy,
                    CreatedDateUTC = defaultCreatedDateUTC,
                },
                new BookDM()
                {
                    Title = "Advanced Calculus",
                    Publisher = "KBD",
                    PublishedDate = DateTime.UtcNow,
                    AuthorId = 2,
                    Price = 800,
                    CreatedBy = defaultCreatedBy,
                    CreatedDateUTC = defaultCreatedDateUTC,
                },
                new BookDM()
                {
                    Title = "Data Structures in C",
                    Publisher = "Unknown",
                    PublishedDate = DateTime.UtcNow,
                    AuthorId = 3,
                    Price = 1000,
                    CreatedBy = defaultCreatedBy,
                    CreatedDateUTC = defaultCreatedDateUTC,
                },
            };
            appDbContext.Books.AddRange(books);
            appDbContext.SaveChanges();
        }
        #endregion Books
    }
}
