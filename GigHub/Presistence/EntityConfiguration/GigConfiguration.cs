using GigHub.core.Models;
using System.Data.Entity.ModelConfiguration;

namespace GigHub.Presistence.EntityConfiguration
{
    public class GigConfiguration:EntityTypeConfiguration<Gig>
    {

        public GigConfiguration()
        {
            Property(g => g.ArtistId).
                IsRequired().
                HasMaxLength(128);

            Property(g => g.Venue).
                IsRequired().
                HasMaxLength(255);

            Property(g => g.GenreId).
                IsRequired();

            HasMany(g=>g.Attendances)
                    .WithRequired(c => c.Gig)
                    .WillCascadeOnDelete(false);

        }
    }
}