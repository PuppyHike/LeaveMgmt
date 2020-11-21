using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LeaveMgmt.Models
{
    public class LeaveAllocationVM
    {
        public int Id { get; set; }
        [Required]
        [Range(1, 25, ErrorMessage = "Please Enter a Valid Number")]
        [Display(Name = "Default # of Days")]
        public int NumberOfDays { get; set; }
        [Display(Name ="Created On")]
        public DateTime DateCreated { get; set; }
        public int Period { get; set; }
        [Required]
        public PersonVM Person { get; set; }
        [Required]
        public string PersonId { get; set; }
        public LeaveTypeVM LeaveType { get; set; }
        public int LeaveTypeId { get; set; }

        public IEnumerable<SelectListItem> Persons { get; set; }
        public IEnumerable<SelectListItem> LeaveTypes { get; set; }
    }

    public class CreateLeaveAllocationVM
    {
        public int NumberUpdated { get; set; }
        public List<LeaveTypeVM> LeaveTypes { get; set;}
    }
}
