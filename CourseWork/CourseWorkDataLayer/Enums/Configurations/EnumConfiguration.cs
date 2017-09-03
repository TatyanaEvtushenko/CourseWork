﻿using System.Collections.Generic;

namespace CourseWork.DataLayer.Enums.Configurations
{
    public static class EnumConfiguration
    {
        public static Dictionary<UserRole, string> RoleNames = new Dictionary<UserRole, string>
        {
            { UserRole.User, "User" },
            { UserRole.ConfirmedUser,  "ConfirmedUser" },
            { UserRole.Admin, "Admin" }
        };
    }
}
