using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LeaveMgmt.Models
{
    public class LeaveTypeVM
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [Range(1,25, ErrorMessage ="Please Enter a Valid Number")]
        [Display(Name="Default # of Days")]
        public int DefaultDays { get; set; }

        [Display(Name="Created On")]
        public DateTime? DateCreated { get; set; }
    }

}
