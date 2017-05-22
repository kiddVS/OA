//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace CUP.PE.OA.Model
{
	using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    
    public partial class Department
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Department()
        {
    
            this.ActionInfo = new HashSet<ActionInfo>();
    
            this.UserInfo = new HashSet<UserInfo>();
        }
    
        public int ID { get; set; }
        public string DepName { get; set; }
        public int ParentId { get; set; }
        public string TreePath { get; set; }
        public int Level { get; set; }
        public bool IsLeaf { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
    	[JsonIgnore]
        public virtual ICollection<ActionInfo> ActionInfo { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
    	[JsonIgnore]
        public virtual ICollection<UserInfo> UserInfo { get; set; }
    }
}
