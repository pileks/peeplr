//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Peeplr.Data.Internal
{
    using System;
    using System.Collections.Generic;
    
    public partial class Contact
    {
        public Contact()
        {
            this.Numbers = new HashSet<Number>();
            this.Tags = new HashSet<Tag>();
            this.Emails = new HashSet<Email>();
        }
    
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string StreetAddress { get; set; }
        public string City { get; set; }
        public string Company { get; set; }
    
        public virtual ICollection<Number> Numbers { get; set; }
        public virtual ICollection<Tag> Tags { get; set; }
        public virtual ICollection<Email> Emails { get; set; }
    }
}
