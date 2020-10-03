using LeaveMgmt.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LeaveMgmt.Contracts
{
    public interface iLeaveHistoryRepository: iRepositoryBase<LeaveHistory>
    {
        ICollection<LeaveHistory> GetAllLeaveHistoryByPerson(string personId);
    }
}
