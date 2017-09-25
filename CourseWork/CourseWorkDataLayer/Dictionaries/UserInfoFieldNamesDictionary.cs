using System;
using System.Collections.Generic;
using CourseWork.DataLayer.Enums.Configurations;
using CourseWork.DataLayer.Models;

namespace CourseWork.DataLayer.Dictionaries
{
    public static class UserInfoFieldNamesDictionary
    {
        public static Dictionary<string, Func<UserInfo, Object>> UserInfoFieldNames =
            new Dictionary<string, Func<UserInfo, Object>>
            {
                {"Status", n => EnumConfiguration.StatusDisplayNames[n.Status]},
                {"LastLoginTime", n => n.LastLoginTime}
            };

    }
}
