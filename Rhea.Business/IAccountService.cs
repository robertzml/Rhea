using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Rhea.Data.Entities;

namespace Rhea.Business
{
    public interface IAccountService
    {
        User ValidateUser(string userName, string password);
    }
}
