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
            public const string DeleteUser = UserEndPoint + "/{id}";
            public const string GetAllUser = UserEndPoint;
            public const string GetUser = UserEndPoint + "/{id}";
            public const string UpdateUser = UserEndPoint + "/{id}";
        }

        public static class Category
        {
            public const string CategoryEndPoint = ApiEndpoint + "/category";
            public const string CreateNewCategory = CategoryEndPoint;
            public const string GetAllCategory = CategoryEndPoint;
            public const string GetCategory = CategoryEndPoint + "/{id}";
            public const string UpdateCategory = CategoryEndPoint + "/{id}";
            public const string DeleteCategory = CategoryEndPoint + "/{id}";
        }
        public static class Product
        {
            public const string ProductEndpoint = ApiEndpoint + "/Product";
            public const string CreateNewProduct = ProductEndpoint;
            public const string GetListProducts = ProductEndpoint;
            public const string GetListProductsByCategoryId = ProductEndpoint +"/{id}" + "/Category";
            public const string UpdateProduct = ProductEndpoint + "/{id}";
            public const string DeleteProduct = ProductEndpoint + "/{id}";
        }

        public static class Order
        {
            public const string OrderEndpoint = ApiEndpoint + "/Order";
            public const string CreateNewOrder = OrderEndpoint;
        }

        public static class GoogleAuthentication
        {
            public const string GoogleAuthenticationEndpoint = ApiEndpoint + "/google-auth";
            public const string GoogleLogin = GoogleAuthenticationEndpoint + "/login";
            public const string GoogleSignIn = GoogleAuthenticationEndpoint + "/signin-google/";

        }

        public static class Cart
        {
            public const string CartEndPoint = ApiEndpoint + "/Cart";
            public const string CreateNewCart = CartEndPoint;
        }
    }
}
