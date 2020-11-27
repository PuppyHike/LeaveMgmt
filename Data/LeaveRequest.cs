﻿using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LeaveMgmt.Data
{
    public class LeaveRequest
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("RequestingPersonId")]
        public Person RequestingPerson { get; set; }
        public string RequestingPersonId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        [ForeignKey("LeaveTypeId")]
        public LeaveType LeaveType { get; set; }
        public int LeaveTypeId { get; set; }
        public DateTime DateRequested { get; set; }
        public DateTime DateActioned { get; set; }

        public bool? Approved { get; set; }
        [ForeignKey("ApprovedById")]
        public Person ApprovedBy { get; set; }
        public string ApprovedById { get; set; }

    }
}
