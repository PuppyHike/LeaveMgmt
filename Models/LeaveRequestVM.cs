﻿using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LeaveMgmt.Models
{
    public class LeaveRequestVM
    {

        public int Id { get; set; }

        public PersonVM RequestingPerson { get; set; }
        public string RequestingPersonId { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }

        public LeaveTypeVM LeaveType { get; set; }
        public int LeaveTypeId { get; set; }
        public IEnumerable<SelectListItem> LeaveTypes { get; set; }
        public DateTime DateRequested { get; set; }
        public DateTime DateActioned { get; set; }

        public bool? Approved { get; set; }

        public PersonVM ApprovedBy { get; set; }
        public string ApprovedById { get; set; }

    }

    public class AdminLeaveRequestViewVM 
        {
        public LeaveRequestVM LeaveRequest { get; set; }
        public int TotalRequests { get; set; }
        public int ApprovedRequests { get; set; }
        public int PendingRequests { get; set; }
        public int RejectedRequests { get; set; }

        public List<LeaveRequestVM> LeaveRequests { get; set; }
    }
}
