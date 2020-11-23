using System;
using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LeaveMgmt.Models;
using LeaveMgmt.Data;

namespace LeaveMgmt.Mappings
{
    public class Maps : Profile
    {
        public Maps()
        {
            //ReverseMap allows for the CreateMap to be bidirectional.
            //Otherwise it's source to target
            CreateMap<LeaveType, LeaveTypeVM>().ReverseMap();
            CreateMap<LeaveHistory, LeaveHistoryVM>().ReverseMap();
            CreateMap<LeaveAllocation, LeaveAllocationVM>().ReverseMap();
            CreateMap<LeaveAllocation, EditLeaveAllocationVM>().ReverseMap();
            CreateMap<Person, PersonVM>().ReverseMap();
        }
    }
}
