using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Application
{
    public enum EmailType
    {
        AccountVerification,PasswordChange,EmailChange
    }

    public enum RoleType
    {
        Admin, Pharmacist
    }
}