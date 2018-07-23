using System.Collections.Generic;
using GigHub.Models;

namespace GigHub.Repositories
{
    public interface IGenresRepository
    {
        List<Genre> GetGenres();
    }
}