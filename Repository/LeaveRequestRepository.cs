using LeaveMgmt.Contracts;
using LeaveMgmt.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LeaveMgmt.Repository
{
    public class LeaveRequestRepository : iLeaveRequestRepository
    {
        private readonly ApplicationDbContext _db;

        public LeaveRequestRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public bool Create(LeaveRequest entity)
        {
            var create = _db.LeaveRequests.Add(entity);
            return Save();
        }

        public bool Delete(LeaveRequest entity)
        {
            var delete = _db.LeaveRequests.Remove(entity);
            return Save();
        }
        public bool isExists(int id)
        {
            var exists = _db.LeaveRequests.Any(q => q.Id == id);
            return exists;
        }
        public ICollection<LeaveRequest> FindAll()
        {
            var leaveRequests = _db.LeaveRequests
                                .Include(x => x.RequestingPerson)
                                .Include(x =>x.ApprovedBy)
                                .Include(x => x.LeaveType)
                                .ToList();
            return leaveRequests;
        }

        public LeaveRequest FindById(int id)
        {
            LeaveRequest leaveRequest = _db.LeaveRequests
                .Include(x => x.RequestingPerson)
                                .Include(x => x.ApprovedBy)
                                .Include(x => x.LeaveType)
                                .FirstOrDefault( x => x.Id == id);
            return leaveRequest;
        }

        public ICollection<LeaveRequest> GetAllLeaveHistoryByPerson(string personId)
        {
            var lst = _db.LeaveRequests.ToList().Where(s => s.RequestingPersonId == personId);
            return lst.ToList();
        }

        public bool Save()
        {
            int save = _db.SaveChanges();
            return save > 0;
        }

        public bool Update(LeaveRequest entity)
        {
            var update = _db.LeaveRequests.Update(entity);
            return Save();
        }

        ICollection<LeaveRequest> iLeaveRequestRepository.GetAllLeaveRequestsByPerson(string personId)
        {
            throw new NotImplementedException();
        }
    }
}
