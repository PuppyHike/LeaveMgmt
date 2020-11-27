using LeaveMgmt.Contracts;
using LeaveMgmt.Data;
using Microsoft.EntityFrameworkCore;
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

        public bool CheckAllocation(int leavetypeid, string personid)
        {
            int period = DateTime.Now.Year;
            return FindAll().Where(q => q.PersonId == personid && q.LeaveTypeId == leavetypeid && q.Period == period).Any();

        }

        public ICollection<LeaveAllocation> GetLeaveAllocationsByPerson(string id)
        {
            int period = DateTime.Now.Year;
            return FindAll().Where(q => q.PersonId == id
            && q.Period == period).ToList();
        }
       
        public bool isExists(int id)
        {
            var exists = _db.LeaveAllocations.Any(q => q.Id == id);
            return exists;
        }
        public ICollection<LeaveAllocation> FindAll()
        {
            var LeaveAllocations = _db.LeaveAllocations
                .Include(c => c.LeaveType)
                .Include(c => c.Person)
                .ToList();
            return LeaveAllocations;
        }

        public LeaveAllocation FindById(int id)
        {
            var leaveAllocation = _db.LeaveAllocations
                .Include(c => c.LeaveType)
                .Include(c => c.Person)
                .FirstOrDefault( c => c.Id == id);
            return leaveAllocation;
        }

        public ICollection<LeaveAllocation> GetAllPersonsByLeaveAllocations(int leaveAllocationId)
        {
            var personsByLeaveAllocation = _db.LeaveAllocations.ToList().OrderBy(s => s.Person.LastName);
            return personsByLeaveAllocation.ToList();
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
