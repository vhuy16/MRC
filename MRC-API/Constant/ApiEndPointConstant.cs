namespace MRC_API.Constant
{
    public static class ApiEndPointConstant
    {
        static ApiEndPointConstant()
        {
        }

        public const string RootEndPoint = "/api";
        public const string ApiVersion = "/v1";
        public const string ApiEndpoint = RootEndPoint + ApiVersion;

        public static class User
        {
            public const string UserEndPoint = ApiEndpoint + "/user";
            public const string RegisterAdmin = UserEndPoint + "/admin";
            public const string RegisterManager = UserEndPoint + "/Manager";
            public const string RegisterCustomer = UserEndPoint + "/Customer";
            public const string Login = UserEndPoint + "/login";
        }
    }
}
