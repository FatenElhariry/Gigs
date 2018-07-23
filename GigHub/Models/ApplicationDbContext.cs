using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace GigHub.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Attendance>()
                .HasRequired(c => c.Gig)
                .WithMany()
                .WillCascadeOnDelete(false);


            modelBuilder.Entity<ApplicationUser>()
                 .HasKey(c => c.Id)
                 .HasMany(c => c.Followees)
                 .WithRequired(c => c.Follower)
                 .HasForeignKey(c => c.FolloweeId)
                 .WillCascadeOnDelete(false);


            modelBuilder.Entity<ApplicationUser>()
                 .HasKey(c=>c.Id)
                 .HasMany(c => c.Followers)
                 .WithRequired(c=>c.Followee)
                 .HasForeignKey(c => c.FollowerId)
                 .WillCascadeOnDelete(false);

            modelBuilder.Entity<Gig>().HasKey(c => c.ID).
                                      HasMany(c => c.Attendances).
                                      WithRequired(c => c.Gig).
                                      HasForeignKey(c => c.GigId).
                                      WillCascadeOnDelete(false);

            modelBuilder.Entity<ApplicationUser>().HasKey(c => c.Id).
                                      HasMany(c => c.Attendances).
                                      WithRequired(c => c.Attendee).
                                      HasForeignKey(c => c.AttendeeId).
                                      WillCascadeOnDelete(false);

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Genre> Genres { get; set; }
        public DbSet<Gig> Gigs { get; set; }
        public DbSet<Attendance> Attendances { get; set; }

        public DbSet<Following> Followings { get; set; }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}