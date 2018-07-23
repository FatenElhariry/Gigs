﻿using GigHub.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GigHub.Repositories
{
    public class AttendaceRepository
    {
        private readonly ApplicationDbContext _context;
        public AttendaceRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Attendance> GetFutureAttandance(string userId)
        {
            return _context.Attendances.Where(a => a.AttendeeId == userId &&
                                       a.Gig.DateTime > DateTime.Now).
                                       ToList();
        }

        public Attendance GetAttendance(int gigId , string attendeeId)
        {
            return _context.Attendances.
                            FirstOrDefault(a => a.AttendeeId == attendeeId
                                                && a.GigId == gigId);

        }
    }
}