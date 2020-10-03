using LeaveMgmt.Contracts;
using LeaveMgmt.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LeaveMgmt.Repository
{
    public class LeaveHistoryRepository : iLeaveHistoryRepository
    {
        private readonly ApplicationDbContext _db;

        public LeaveHistoryRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public bool Create(LeaveHistory entity)
        {
            var create = _db.LeaveHistories.Add(entity);
            return Save();
        }

        public bool Delete(LeaveHistory entity)
        {
            var delete = _db.LeaveHistories.Remove(entity);
            return Save();
        }

        public ICollection<LeaveHistory> FindAll()
        {
            var list = _db.LeaveHistories.ToList();
            return list;
        }

        public LeaveHistory FindById(int id)
        {
            var leaveHistory = _db.LeaveHistories.Find(id);
            return leaveHistory;
        }

        public ICollection<LeaveHistory> GetAllLeaveHistoryByPerson(string personId)
        {
            var lst = _db.LeaveHistories.ToList().Where(s => s.RequestingPersonId == personId);
            return lst.ToList();
        }

        public bool Save()
        {
            int save = _db.SaveChanges();
            return save > 0;
        }

        public bool Update(LeaveHistory entity)
        {
            var update = _db.LeaveHistories.Update(entity);
            return Save();
        }
    }
}
