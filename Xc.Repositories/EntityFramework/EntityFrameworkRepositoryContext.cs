/*
 * Author:xiaocong_soft
 * Blog:http://xcong.cnblogs.com
 */
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xc.Domain.Repositories;
using Xc.Repositories.EntityFramework;

namespace Xc.Repositories
{
    public class EntityFrameworkRepositoryContext : RepositoryContext, IEntityFrameworkRepositoryContext
    {
        private readonly ThreadLocal<XcDbContext> localCtx = new ThreadLocal<XcDbContext>(() => new XcDbContext());

        public override void RegisterDeleted<TAggregateRoot>(TAggregateRoot obj)
        {
            try
            {
                //先判断状态是否分离，分离则附加
                if (localCtx.Value.Entry<TAggregateRoot>(obj).State == EntityState.Detached)
                    localCtx.Value.Set<TAggregateRoot>().Attach(obj);
            }
            catch (Exception)
            {
            }
            finally
            {
                localCtx.Value.Set<TAggregateRoot>().Remove(obj);
                Commited = false;
            }
        }

        public override void RegisterModified<TAggregateRoot>(TAggregateRoot obj)
        {
            //try
            //{
            //    //先判断状态是否分离，分离则附加
            //    if (localCtx.Value.Entry<TAggregateRoot>(obj).State == EntityState.Detached)
            //        localCtx.Value.Set<TAggregateRoot>().Attach(obj);
            //}
            //catch (Exception)
            //{
            //}
            //finally
            //{
                localCtx.Value.Entry<TAggregateRoot>(obj).State = EntityState.Modified;
                Commited = false;
            //}
        }

        public override void RegisterNew<TAggregateRoot>(TAggregateRoot obj)
        {
            localCtx.Value.Set<TAggregateRoot>().Add(obj);
            Commited = false;
        }

        public override int Commit()
        {
            if (!Commited)
            {
                var validationErrors = localCtx.Value.GetValidationErrors();
                var count = localCtx.Value.SaveChanges();
                Commited = true;
                return count;
            }
            return 0;
        }

        public override void Rollback()
        {
            Commited = false;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (!Commited)
                    Commit();
                localCtx.Value.Dispose();
                localCtx.Dispose();
                base.Dispose(disposing);
            }
        }

        #region IEntityFrameworkRepositoryContext Members

        public DbContext Context
        {
            get { return localCtx.Value; }
        }

        #endregion
    }
}
