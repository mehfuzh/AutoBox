using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace AutoBox.Abstraction
{
    public interface IConfiguration
    {
        IConfigurationItem<T> Setup<T>(Expression<Func<T, object>> expression);
        IConfigurationItem<T> Setup<T>(Expression<Action<T>> expression); 
    }
}
