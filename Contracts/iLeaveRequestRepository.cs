using LeaveMgmt.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LeaveMgmt.Contracts
{
    public interface iLeaveRequestRepository: iRepositoryBase<LeaveRequest>
    {
        ICollection<LeaveRequest> GetAllLeaveRequestsByPerson(string personId);
    }
}
