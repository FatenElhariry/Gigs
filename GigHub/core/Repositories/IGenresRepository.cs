using GigHub.core.Models;
using System.Collections.Generic;

namespace GigHub.core.Repositories
{
    public interface IGenresRepository
    {
        List<Genre> GetGenres();
    }
}