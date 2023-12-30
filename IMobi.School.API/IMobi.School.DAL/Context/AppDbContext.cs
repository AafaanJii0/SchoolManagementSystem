using IMobi.School.DomainModal.v1;
using IMobi.School.DomainModal.v1.Book;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace IMobi.School.DAL.Context
{
    public class AppDbContext : IdentityDbContext<IdentityUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        #region Users
        public DbSet<ClientUserDM> ClientUsers { get; set; }
        #endregion Users

        #region Application Specified Tables
        public DbSet<BookDM> Books { get; set; }
        public DbSet<AuthorDM> Authors { get; set; }
        #endregion Application Specified Tables
    }
}