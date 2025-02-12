using AssetsPro.Models;

namespace AssetsPro.Constants
{
    public class Permessions
    {
        public static List<string> GeneratePermissionList(string module)
        {
            if (module == "Employee" || module == "Users")
            {
                return new List<string>()
                {
                    $"Permission.{module}.Show",
                    $"Permission.{module}.Add",
                    $"Permission.{module}.Edit",
                    $"Permission.{module}.Delete"
                };
            }
            else if(module == "Groups")
            {
                return new List<string>()
                {
                    $"Permission.{module}.Show",
                    $"Permission.{module}.Add",
                    $"Permission.{module}.Edit"
                };
            }
            else
            {
                return new List<string>()
                { $"Permission.{module}.Show" };
            }
        }
        public static List<string> GenerateAllPermissions()
        {
            var permissions = new List<string>();
            var modules = Enum.GetValues(typeof(Modules));
            foreach (var module in modules)
            {
                permissions.AddRange(GeneratePermissionList(module.ToString()));
            }
            return permissions;
        }

        public static class Employee
        {
            public const string Show = "Permission.Employee.Show";
            public const string Add = "Permission.Employee.Add";
            public const string Edit = "Permission.Employee.Edit";
            public const string Delete = "Permission.Employee.Delete";
        }
        public static class Attendance
        {
            public const string Show = "Permission.Attendance.Show";
        }
        public static class SalaryReport
        {
            public const string Show = "Permission.SalaryReport.Show";
        }
        public static class GeneralSetting
        {
            public const string Show = "Permission.GeneralSetting.Show";
        }
        public static class Users
        {
            public const string Show = "Permission.Users.Show";
            public const string Add = "Permission.Users.Add";
            public const string Edit = "Permission.Users.Edit";
            public const string Delete = "Permission.Users.Delete";
        }
        public static class Groups
        {
            public const string Show = "Permission.Groups.Show";
            public const string Add = "Permission.Groups.Add";
            public const string Edit = "Permission.Groups.Edit";
        }   
    }
}
