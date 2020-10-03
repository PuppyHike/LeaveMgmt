﻿using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LeaveMgmt.Models
{
    public class LeaveAllocationsVM
    {
        public int Id { get; set; }
        [Required]
        public int NumberOfDays { get; set; }

        public DateTime DateCreated { get; set; }

        public PersonVM Person { get; set; }

        public string PersonId { get; set; }
        public DetailLeaveTypeVM LeaveType { get; set; }
        public int LeaveTypeId { get; set; }

        public IEnumerable<SelectListItem> Persons { get; set; }
        public IEnumerable<SelectListItem> LeaveTypes { get; set; }
    }
}