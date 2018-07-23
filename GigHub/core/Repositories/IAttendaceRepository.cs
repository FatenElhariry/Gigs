using GigHub.core.Models;
using System.Collections.Generic;

namespace GigHub.core.Repositories
{
    public interface IAttendaceRepository
    {
        Attendance GetAttendance(int gigId, string attendeeId);
        List<Attendance> GetFutureAttandance(string userId);
    }
}