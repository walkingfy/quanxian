/*
 * Author:xiaocong_soft
 * Blog:http://xcong.cnblogs.com
 */
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xc.Domain.Entity;

namespace Xc.Repositories.EntityFramework
{
    public class ModuleTypeConfiguration : EntityTypeConfiguration<Module>
    {
        public ModuleTypeConfiguration()
        {
            HasKey(t => t.Id);
            Property(t => t.Id).IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.Name).IsRequired().HasMaxLength(100);
            Property(t => t.Icon).HasMaxLength(200);
            Property(t => t.Controller).HasMaxLength(200);
            Property(t => t.Action).HasMaxLength(200);
            Property(t => t.Remark).HasMaxLength(200);
        }
    }
}
