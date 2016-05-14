using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using EmitMapper;
using Xc.Core.Exceptions;
using Xc.Core.Tools;
using Xc.DataObjects;
using Xc.DataObjects.Enums;
using Xc.Domain;
using Xc.Domain.Entity;
using Xc.Domain.Repositories;

namespace Xc.Application
{
    public class ApplicationService
    {
        private readonly IRepositoryContext _context;

        public ApplicationService()
        {
            _context = AutofacInstace.Resolve<IRepositoryContext>();
        }

        #region Protected Properties
        /// <summary>
        /// 获取当前应用层服务所使用的仓储上下文实例。
        /// </summary>
        protected IRepositoryContext Context
        {
            get { return this._context; }
        }
        #endregion

        
    }
}
