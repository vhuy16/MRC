﻿namespace MRC_API.Constant
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
            public const string VerifyOtp = UserEndPoint + "/verify-otp";
            public const string ForgotPassword = UserEndPoint + "/forgot-password";
            public const string VerifyAndResetPassword = UserEndPoint + "/{id}" + "/forgot-password/verify";
        }
        public static class Payment
        {
            public const string CreatePaymentUrl = "api/payment/create-url";
            public const string GetPaymentInfo = "api/payment/{paymentLinkId}";
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
            public const string GetProductById = ProductEndpoint + "/{id}";
            public const string GetListProductsByCategoryId = ProductEndpoint +"/{id}" + "/Category";
            public const string UpdateProduct = ProductEndpoint + "/{id}";
            public const string DeleteProduct = ProductEndpoint + "/{id}";
        }

        public static class Order
        {
            public const string OrderEndpoint = ApiEndpoint + "/Order";
            public const string CreateNewOrder = OrderEndpoint;
            public const string GetListOrder = OrderEndpoint;
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
            public const string AddCartItem = CartEndPoint + "/Item";
            public const string DeleteCartItem = CartEndPoint + "Item" + "/{ItemId}";
            public const string GetAllCart = CartEndPoint;
            public const string ClearCart = CartEndPoint + "/Clear-All";
            public const string GetCartSummary = CartEndPoint + "/Get-Summary";
            public const string UpdateCartItem = CartEndPoint + "Item" + "/{ItemId}";
        }
        public static class Email
        {
            public const string EmailPoint = ApiEndpoint + "/email";
            public const string SendEmail = EmailPoint;
        }
        public static class Service
        {
            public const string ServiceEndPoint = ApiEndpoint + "/service";
            public const string CreateNewService = ServiceEndPoint;
            public const string GetAllService = ServiceEndPoint;
            public const string GetService = ServiceEndPoint + "/{id}";
            public const string UpdateService = ServiceEndPoint + "/{id}";
            public const string DeleteService = ServiceEndPoint + "/{id}";
        }
        public static class Booking
        {
            public const string BookingEndPoint = ApiEndpoint + "/booking";
            public const string CreateNewBooking = BookingEndPoint;
            public const string GetAllBookings = BookingEndPoint;
            public const string GetBooking = BookingEndPoint + "/{id}";
            public const string UpdateBooking = BookingEndPoint + "/{id}";
            public const string DeleteBooking = BookingEndPoint + "/{id}";
            public const string GetbookingByStatus = BookingEndPoint + "/status";

        }
        
    }
}
