﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LeaveMgmt.Data
{
    public class LeaveAllocation
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int NumberOfDays { get; set; }

        public DateTime DateCreated { get; set; }

        [ForeignKey("PersonId")]
        public Person Person { get; set; }

        public string PersonId { get; set; }
        [ForeignKey("LeaveTypeId")]
        public LeaveType LeaveType { get; set; }
        public int LeaveTypeId { get; set; }
        public int Period { get; set; }
    }
}
