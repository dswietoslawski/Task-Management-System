using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TMS.WebAPI.Infrastructure {

    public class ApplicationUser : IdentityUser {

        [Required]
        [MaxLength]
        public string FirstName { get; set; }

        [Required]
        [MaxLength]
        public string LastName { get; set; }

        [Required]
        public byte Level { get; set; }

        [Required]
        public DateTime JoinDate { get; set; }
    }
}