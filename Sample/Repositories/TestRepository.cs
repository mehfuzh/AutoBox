using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoBox.Sample.Repositories.Abstraction;

namespace AutoBox.Sample.Repositories
{
    public class TestRepository : ITestRepository
    {
        public TimeStamp GetTimeStamp()
        {
            return new TimeStamp { Now = DateTime.Now };
        }

        public void UpdateTimeStamp()
        {
            // not implementing anyhere here for now. Generally an update operation.
        }
    }
}