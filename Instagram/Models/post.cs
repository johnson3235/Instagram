//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Instagram.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class post
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public post()
        {
            this.likes1 = new HashSet<like>();
            this.comment_posts = new HashSet<comment_posts>();
        }
    
        public long id { get; set; }
        public int likes { get; set; }
        public string comment { get; set; }
        public string images { get; set; }
        public long user_id { get; set; }
        public Nullable<System.DateTime> created_at { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<like> likes1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<comment_posts> comment_posts { get; set; }
    }
}
