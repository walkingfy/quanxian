/*
 * Author:xiaocong_soft
 * Blog:http://xcong.cnblogs.com
 */
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xc.Domain.ValueObject;

namespace Xc.Domain
{
    public abstract class EntityBase : DomainBase, IEntity
    {
        #region 构造函数

        //protected EntityBase(Guid id)
        //{
        //    Id = id;
        //}
        #endregion
        #region 字段
        /// <summary>
        /// 创建时间
        /// </summary>
        public virtual DateTime? CreateTime { get; set; }
        #endregion
        #region 标识
        /// <summary>
        /// 标识
        /// </summary>
        [Required]
        public Guid Id { get; set; }
        #endregion
        #region 重写

        /// <summary>
        /// 重写操作符
        /// </summary>
        /// <param name="entity1">实体1</param>
        /// <param name="entity2">实体2</param>
        /// <returns>返回是否相等</returns>
        public static bool operator ==(EntityBase entity1, EntityBase entity2)
        {
            if ((object)entity1 == null && (object)entity2 == null)
                return true;
            if ((object)entity1 == null || (object)entity2 == null)
                return false;
            if (ReferenceEquals(entity1, entity2))
                return true;
            if (entity1.Id == null)
                return false;
            if (entity1.Id.Equals(default(Guid)))
                return false;

            return entity1.Id.Equals(entity2.Id);
        }
        /// <summary>
        /// 重写操作符
        /// </summary>
        /// <param name="entity1">实体1</param>
        /// <param name="entity2">实体2</param>
        /// <returns>返回是否不相等</returns>

        public static bool operator !=(EntityBase entity1, EntityBase entity2)
        {
            return !(entity1 == entity2);
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            if (!(obj is EntityBase))
                return false;
            return this == (EntityBase)obj;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        #endregion
    }
}
