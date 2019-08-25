using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EvolentAssignment.Models
{
    [Serializable]
    public class Contact
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public Boolean? Status { get; set; }
    }

    public partial class tblContact
    {
        
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public Nullable<bool> Status { get; set; }
        public string PhoneNumber { get; set; }
    }
}