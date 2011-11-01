using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoBox.Sample.Services.Abstraction;
using System.Web.Security;

namespace AutoBox.Sample.Services
{
    public class MembershipService : IMembershipService
    {
        public bool ValidateUser(string username, string password)
        {
            return Membership.ValidateUser(username, password);
        }
    }
}