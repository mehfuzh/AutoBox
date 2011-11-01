using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;

namespace AutoBox.Abstraction
{
    public interface IInvalidator<T> : IConfiguration
    {
        IConfiguration InvalidatesOn(Expression<Func<T, object>> expression);
    }
}
