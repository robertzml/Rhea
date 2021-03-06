﻿using System;

namespace Rhea.Model
{
    /// <summary>
    /// CollectionName Attribute,
    /// 默认使用类名为集合名称
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, Inherited = true)]
    public class CollectionName : Attribute
    {
        /// <summary>
        /// 初始化CollectionName
        /// </summary>
        /// <param name="value">Collection名称</param>
        public CollectionName(string value)
        {
#if NET35
            if (string.IsNullOrEmpty(value) || value.Trim().Length == 0)
#else
            if (string.IsNullOrWhiteSpace(value))
#endif
                throw new ArgumentException("Empty collectionname not allowed", "value");

            this.Name = value;
        }

        /// <summary>
        /// Collection名称
        /// </summary>
        /// <value>Collection名称</value>
        public virtual string Name { get; private set; }
    }
}
