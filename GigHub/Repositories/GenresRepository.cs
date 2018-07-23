using GigHub.Models;
using System.Collections.Generic;
using System.Linq;

namespace GigHub.Repositories
{
    public class GenresRepository : IGenresRepository
    {
        private readonly ApplicationDbContext _context ;
        public GenresRepository(ApplicationDbContext context)
        {
            _context = new ApplicationDbContext();
        }
        public List<Genre> GetGenres()
        {
            return _context.Genres.ToList();
        }
    }
}