namespace PredmetProjekat.Common.Constants
{
    public static class Constants
    { 
        //User roles
        public static readonly string AdminRole = "Admin";
        public static readonly string EmployeeRole = "Employee";

        //User claims
        public static readonly string Username = "username";
        public static readonly string Role = "role";


        //appsettings.json config
        public static readonly string Jwt = "Jwt";
        public static readonly string Key = "Key";
        public static readonly string Issuer = "Issuer";
        public static readonly string Lifetime = "Lifetime";
        public static readonly string DbConnectionString = "DbConnectionString";
    }
}
