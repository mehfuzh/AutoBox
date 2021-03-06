﻿using System;
using System.Linq;
using System.Linq.Expressions;

namespace AutoBox.Abstraction
{
    /// <summary>
    /// Entry-point for defining settings for a resolved member.
    /// </summary>
    public interface IConfiguration
    {
        /// <summary>
        /// Defines the configuration of the resolved method.
        /// </summary>
        IConfigurationItem<T> Setup<T>(Expression<Func<T, object>> expression);
        
        /// <summary>
        /// Defines the configuration of the resolved method.
        /// </summary>
        IConfigurationItem<T> Setup<T>(Expression<Action<T>> expression); 
    }
}
