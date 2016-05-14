/*
 * Author:xiaocong_soft
 * Blog:http://xcong.cnblogs.com
 */
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xc.Domain.Entity;

namespace Xc.Repositories.EntityFramework
{
    public class RoleTypeConfiguration : EntityTypeConfiguration<Role>
    {
        public RoleTypeConfiguration()
        {
            HasKey(t => t.Id);
            Property(t => t.Id).IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.Name).IsRequired().HasMaxLength(50);
            Property(t => t.Remark).HasMaxLength(200);
        }
    }
}
