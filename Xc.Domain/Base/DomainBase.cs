/*
 * Author:xiaocong_soft
 * Blog:http://xcong.cnblogs.com
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xc.Core.Validations;

namespace Xc.Domain
{
    public class DomainBase
    {

        #region 构造方法

        /// <summary>
        /// 初始化领域层顶级基类
        /// </summary>
        protected DomainBase()
        {
            _rules = new List<IValidationRule>();
            _handler = new ValidationHandler();
        }

        #endregion

        #region 字段

        /// <summary>
        /// 描述
        /// </summary>
        private StringBuilder _description;
        /// <summary>
        /// 验证规则集合
        /// </summary>
        private readonly List<IValidationRule> _rules;
        /// <summary>
        /// 验证处理器
        /// </summary>
        private IValidationHandler _handler;

        #endregion

        #region ToString(输出领域对象的状态)

        /// <summary>
        /// 输出领域对象的状态
        /// </summary>
        public override string ToString()
        {
            _description = new StringBuilder();
            AddDescriptions();
            return _description.ToString().TrimEnd().TrimEnd(',');
        }

        /// <summary>
        /// 添加描述
        /// </summary>
        protected virtual void AddDescriptions()
        {
        }

        /// <summary>
        /// 添加描述
        /// </summary>
        protected void AddDescription(string description)
        {
            if (string.IsNullOrWhiteSpace(description))
                return;
            _description.Append(description);
        }

        /// <summary>
        /// 添加描述
        /// </summary>
        protected void AddDescription<T>(string name, T value)
        {
            if (string.IsNullOrWhiteSpace(value.ToString()))
                return;
            _description.AppendFormat("{0}:{1},", name, value);
        }

        #endregion

        #region SetValidationHandler(设置验证处理器)

        /// <summary>
        /// 设置验证处理器
        /// </summary>
        /// <param name="handler">验证处理器</param>
        public void SetValidationHandler(IValidationHandler handler)
        {
            if (handler == null)
                return;
            _handler = handler;
        }

        #endregion

        #region AddValidationRule(添加验证规则)

        /// <summary>
        /// 添加验证规则
        /// </summary>
        /// <param name="rule">验证规则</param>
        public void AddValidationRule(IValidationRule rule)
        {
            if (rule == null)
                return;
            _rules.Add(rule);
        }

        #endregion

        #region Validate(验证)

        /// <summary>
        /// 验证
        /// </summary>
        public virtual void Validate()
        {
            var result = GetValidationResult();
            HandleValidationResult(result);
        }

        /// <summary>
        /// 获取验证结果
        /// </summary>
        private ValidationResultCollection GetValidationResult()
        {
            var result = ValidationFactory.Create().Validate(this);
            Validate(result);
            foreach (var rule in _rules)
                result.Add(rule.Validate());
            return result;
        }

        /// <summary>
        /// 验证并添加到验证结果集合
        /// </summary>
        /// <param name="results">验证结果集合</param>
        protected virtual void Validate(ValidationResultCollection results)
        {
        }

        /// <summary>
        /// 处理验证结果
        /// </summary>
        private void HandleValidationResult(ValidationResultCollection results)
        {
            if (results.IsValid)
                return;
            _handler.Handle(results);
        }

        #endregion
    }
}
