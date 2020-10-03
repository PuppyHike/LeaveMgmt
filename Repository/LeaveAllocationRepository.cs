using LeaveMgmt.Contracts;
using LeaveMgmt.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LeaveMgmt.Repository
{
    public class LeaveAllocationRepository : iLeaveAllocationRepository
    {
        private readonly ApplicationDbContext _db;

        public LeaveAllocationRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public bool Create(LeaveAllocation entity)
        {
            var create = _db.LeaveAllocations.Add(entity);
            return Save();
        }

        public bool Delete(LeaveAllocation entity)
        {
            var delete = _db.LeaveAllocations.Remove(entity);
            return Save();
        }

        public ICollection<LeaveAllocation> FindAll()
        {
            var lst = _db.LeaveAllocations.ToList();
            return lst;
        }

        public LeaveAllocation FindById(int id)
        {
            var leaveAllocation = _db.LeaveAllocations.Find(id);
            return leaveAllocation;
        }

        public ICollection<LeaveAllocation> GetAllPersonsByLeaveAllocations(int leaveAllocationId)
        {
            var personsByLeaveAllocation = _db.LeaveAllocations.ToList().OrderBy(s => s.Person.LastName);
            return personsByLeaveAllocation.ToList();
        }

        public bool Save()
        {
            var saved = _db.SaveChanges();
            return saved > 0;
        }

        public bool Update(LeaveAllocation entity)
        {
            var upd = _db.LeaveAllocations.Update(entity);
            return Save();

        }
    }
}
