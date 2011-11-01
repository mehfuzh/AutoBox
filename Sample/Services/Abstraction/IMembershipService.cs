using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AutoBox.Sample.Services.Abstraction
{
    public interface IMembershipService
    {
        bool ValidateUser(string username, string password);
    }
}
