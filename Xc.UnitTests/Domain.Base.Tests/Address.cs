﻿/*
 * Author:xiaocong_soft
 * Blog:http://xcong.cnblogs.com
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xc.Domain;

namespace Xc.UnitTests.Domain.Base.Tests
{
    /// <summary>
    /// 地址
    /// </summary>
    public class Address : ValueObjectBase<Address>
    {
        /// <summary>
        /// 初始化地址
        /// </summary>
        /// <param name="city">城市</param>
        /// <param name="street">街道</param>
        public Address(string city, string street)
        {
            City = city;
            Street = street;
        }

        /// <summary>
        /// 城市
        /// </summary>
        public string City { get; private set; }
        /// <summary>
        /// 街道
        /// </summary>
        public string Street { get; private set; }
    }
}
