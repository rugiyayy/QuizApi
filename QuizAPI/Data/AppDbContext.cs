using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using QuizAPI.Entites;

namespace QuizAPI.Data
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }


        public DbSet<AppUser> Users { get; set; }
        public DbSet<Quiz> Quizzes { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Option> Options { get; set; }




        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Quiz>()
                           .HasMany(q => q.Questions)
                           .WithOne(q => q.Quizzes)
                           .OnDelete(DeleteBehavior.Cascade);



            modelBuilder.Entity<Option>()
        .HasOne(o => o.Question)
        .WithMany(q => q.Options)
        .OnDelete(DeleteBehavior.Cascade);
        }
    }

}
