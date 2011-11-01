using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AutoBox.Abstraction
{
    public interface IMethodMetaData
    {
        string Key { get; }
        bool ValidateArguments(object[] arguments);
    }
}
