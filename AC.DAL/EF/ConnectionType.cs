//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AC.DAL.EF
{
    using System;
    using System.Collections.Generic;
    
    public partial class ConnectionType : IEntity
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ConnectionType()
        {
            this.A2BConnections = new HashSet<Connection>();
            this.B2AConnections = new HashSet<Connection>();
            this.ConnectionTypeAttributes = new HashSet<AttributeDescription>();
        }
    
        public int Id { get; set; }
        public string Name { get; set; }
        public string ConnectionRule { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Connection> A2BConnections { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Connection> B2AConnections { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AttributeDescription> ConnectionTypeAttributes { get; set; }
    }
}