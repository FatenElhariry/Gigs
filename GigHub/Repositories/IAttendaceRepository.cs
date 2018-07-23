using GigHub.Models;
using System.Collections.Generic;

namespace GigHub.Repositories
{
    public interface IAttendaceRepository
    {
        Attendance GetAttendance(int gigId, string attendeeId);
        List<Attendance> GetFutureAttandance(string userId);
    }
}