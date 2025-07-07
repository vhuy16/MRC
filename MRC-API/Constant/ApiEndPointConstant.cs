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
        public static class News
        {
            public const string NewsEndPoint = ApiEndpoint + "/news";
            public const string CreateFromExternal = "api/news/external/create";
            public const string CreateNews = NewsEndPoint;
            public const string GetAllNews = NewsEndPoint;
            public const string GetNewsById = NewsEndPoint + "/{id}";
            public const string DeleteNewsById = NewsEndPoint + "/{id}";
            public const string UpdateNews = NewsEndPoint + "/{id}";
        }
        public static class User
        {
            public const string UserEndPoint = ApiEndpoint + "/user";
            public const string RegisterAdmin = UserEndPoint + "/admin";
            public const string RegisterManager = UserEndPoint + "/manager";
            public const string RegisterCustomer = UserEndPoint + "/customer";
            public const string Login = UserEndPoint + "/login";
            public const string LoginCustomer = UserEndPoint + "/login-customer";
            public const string DeleteUser = UserEndPoint + "/{id}";
            public const string GetAllUser = UserEndPoint;
            public const string GetUserById = UserEndPoint + "/{id}";
            public const string GetUser = UserEndPoint + "token";
            public const string UpdateUser = UserEndPoint + "/{id}";
            public const string VerifyOtp = UserEndPoint + "/verify-otp";
            public const string ForgotPassword = UserEndPoint + "/forgot-password";
            public const string ResetPassword = UserEndPoint + "/reset-password";
            public const string VerifyForgotPassword = UserEndPoint + "/verify-forgot-password";
            public const string ChangePassword = UserEndPoint + "/change-password";
        }
        public static class Payment
        {
            public const string CreatePaymentUrl = "api/payment/create-url";
            public const string GetPaymentInfo = "api/payment/{paymentLinkId}";
        }
        public static class VNPay
        {
            public const string CreatePaymentUrl = "api/vnpay/create-payment-url";
            public const string ValidatePaymentResponse = "api/vnpay/validate-payment-response";
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
            public const string ProductEndpoint = ApiEndpoint + "/product";
            public const string CreateNewProduct = ProductEndpoint;
            public const string GetListProducts = ProductEndpoint;
            public const string GetAllProducts = ProductEndpoint + "/get-all-product";
            public const string GetProductById = ProductEndpoint + "/{id}";
            public const string GetListProductsByCategoryId = ProductEndpoint +"/{id}" + "/category";
            public const string UpdateProduct = ProductEndpoint + "/{id}";
            public const string EnableProduct = ProductEndpoint + "/enable-product" + "/{id}"  ;
            public const string DeleteProduct = ProductEndpoint + "/{id}";
            public const string DisableProduct = ProductEndpoint + "/disable-product" + "/{id}";
            public const string PatchProductImages = ProductEndpoint + "/patch-product-images" + "/{id}";
            public const string UploadImg = "upload-img";
        }

        public static class Order
        {
            public const string OrderEndpoint = ApiEndpoint + "/order";
            public const string CreateNewOrder = OrderEndpoint;
            public const string GetListOrder = OrderEndpoint;
            public const string GetALLOrder = OrderEndpoint + "/get-all-order";
            public const string GetOrderById = OrderEndpoint + "/{id}";
            public const string UpdateOrder = OrderEndpoint + "/{id}";
            public const string CancelOrder = OrderEndpoint + "/cancel-order" + "/{id}";
        }

        public static class GoogleAuthentication
        {
            public const string GoogleAuthenticationEndpoint = ApiEndpoint + "/google-auth";
            public const string GoogleLogin = GoogleAuthenticationEndpoint + "/login";
            public const string GoogleSignIn = GoogleAuthenticationEndpoint + "/signin-google/";

        }

        public static class Cart
        {
            public const string CartEndPoint = ApiEndpoint + "/cart";
            public const string AddCartItem = CartEndPoint + "/item";
            public const string DeleteCartItem = CartEndPoint + "item" + "/{itemId}";
            public const string GetAllCart = CartEndPoint;
            public const string ClearCart = CartEndPoint + "/clear-all";
            public const string GetCartSummary = CartEndPoint + "/get-summary";
            public const string UpdateCartItem = CartEndPoint + "item" + "/{itemId}";
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
            public const string GetAllServiceBySatus = ServiceEndPoint + "/get-all-service-by-satus";
            public const string GetService = ServiceEndPoint + "/{id}";
            public const string UpdateService = ServiceEndPoint + "/{id}";
            public const string DeleteService = ServiceEndPoint + "/{id}";
        }
        public static class Booking
        {
            public const string BookingEndPoint = ApiEndpoint + "/booking";
            public const string CreateNewBooking = BookingEndPoint;
            public const string GetAllBookings = BookingEndPoint;
            public const string GetBookings = BookingEndPoint + "/get-bookings-admin";

            public const string GetBooking = BookingEndPoint + "/{id}";
            public const string UpdateBooking = BookingEndPoint + "/{id}";
            public const string DeleteBooking = BookingEndPoint + "/{id}";
            public const string GetbookingByStatus = BookingEndPoint + "/status";

        }
        public static class Form
        {
            public const string FormEndPoint = ApiEndpoint + "/form";
            public const string SentForm = FormEndPoint;
            public const string GetForms = FormEndPoint;
            public const string GetForm = FormEndPoint + "/{id}";
            public const string DeleteForm = FormEndPoint + "/{id}";
        }

        public static class DashBoard
        {
            public const string DashBoardEndPoint = ApiEndpoint + "/dashboard";
            public const string GetDashBoard = DashBoardEndPoint;
        }

        public static class SubCategory
        {
            public const string SubCategoryEndPoint = ApiEndpoint + "/sub-category";
            public const string CreateSubCategory = SubCategoryEndPoint;
            public const string GetSubCategories = SubCategoryEndPoint;
            public const string GetSubCategory = SubCategoryEndPoint + "/{id}";
            public const string DeleteSubCategory = SubCategoryEndPoint + "/{id}";
            public const string UpdateSubCategory = SubCategoryEndPoint + "/{id}";
            public const string GetListSubCategoryByCategoryId = SubCategoryEndPoint + "/{id}" + "/category";
        }

    }
}
