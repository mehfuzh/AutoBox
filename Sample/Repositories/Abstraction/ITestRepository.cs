using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AutoBox.Sample.Repositories.Abstraction
{
    public interface ITestRepository
    {
        TimeStamp GetTimeStamp();
        void UpdateTimeStamp();
    }
}
