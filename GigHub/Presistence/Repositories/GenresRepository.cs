using GigHub.core.Models;
using GigHub.core.Repositories;
using GigHub.Presistence.Models;
using System.Collections.Generic;
using System.Linq;

namespace GigHub.Presistence.Repositories
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