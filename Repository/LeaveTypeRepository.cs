using LeaveMgmt.Contracts;
using LeaveMgmt.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LeaveMgmt.Repository
{
    public class LeaveTypeRepository : iLeaveTypeRepository
    {
        private readonly ApplicationDbContext _db;

        public LeaveTypeRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public bool Create(LeaveType entity)
        {
            _db.LeaveTypes.Add(entity);
            return Save();
        }

        public bool Delete(LeaveType entity)
        {
            _db.Remove(entity);
            return Save();
        }

        public ICollection<LeaveType> FindAll()
        {
          var leaveTypes =  _db.LeaveTypes.ToList();
            return leaveTypes;
        }

        public LeaveType FindById(int id)
        {
            var leaveType = _db.LeaveTypes.Find(id);
            return leaveType;
        }

        public ICollection<LeaveType> GetAllPersonsByLeaveType(int leaveTypeId)
        {

            throw new NotImplementedException();
        }

        public bool Save()
        {
           var changes = _db.SaveChanges();
            return changes > 0;
        }

        public bool Update(LeaveType entity)
        {
            _db.Update(entity);
            return Save();
        }
    }
}
