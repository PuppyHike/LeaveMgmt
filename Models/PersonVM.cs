using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LeaveMgmt.Models
{
    public class PersonVM
    {
        public string Id { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Email { get; set; }
        
        public string PhoneNumber { get; set; }
        
        public string Firstname { get; set; }
        public string LastName { get; set; }

        public string TaxId { get; set; }

        public DateTime DateOfBirth { get; set; }

        public DateTime DateJoined { get; set; }
    }
}
