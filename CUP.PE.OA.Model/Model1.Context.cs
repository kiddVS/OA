﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace CUP.PE.OA.Model
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class OAEntities : DbContext
    {
        public OAEntities()
            : base("name=OAEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<UserInfo> UserInfo { get; set; }
        public virtual DbSet<ActionInfo> ActionInfo { get; set; }
        public virtual DbSet<Department> Department { get; set; }
        public virtual DbSet<R_UserInfo_ActionInfo> R_UserInfo_ActionInfo { get; set; }
        public virtual DbSet<RoleInfo> RoleInfo { get; set; }
        public virtual DbSet<Books> Books { get; set; }
        public virtual DbSet<KeyWordsRank> KeyWordsRank { get; set; }
        public virtual DbSet<SearchDetails> SearchDetails { get; set; }
    }
}
