namespace MRC_API.Constant
{
    public static class MessageConstant
    {
        public static class UserMessage
        {
            public const string CreateUserAdminFail = "tạo tài khoản admin thất bại";
            public const string AccountExisted = "Tài khoản đã tồn tại";
            public const string EmailExisted = "Email đã tồn tại";
            public const string PhoneExisted = "Số điện thoại đã tồn tại";
            public const string UserNotExist = "Người dùng không tồn tại";
            public const string UserIsEmpty = "Không có người dùng";
            public const string AccountNotExist = "Tài khoản không tồn tại";

        }
        public static class LoginMessage
        {
            public const string InvalidUsernameOrPassword = "Tên đăng nhập hoặc mật khẩu không chính xác";
        }

        public static class PatternMessage
        {
            public const string EmailIncorrect = "Email không đúng định dạng";
            public const string PhoneIncorrect = "Số điện thoại không định dạng";
        }

        public static class CategoryMessage
        {
            public const string CreateCategoryFail = "Tạo mới category thất bại";

            public const string CategoryIsEmpty = "Category trống";
            public const string CategoryExisted = "Category đã tồn tại";

            public const string CategoryNotExist = "Category không tồn tại";
        }
        public static class ProductMessage
        {
            public const string CreateProductFail = "Tạo mới sản phẩm thất bại";

            public static string ProductNameExisted = "Sản Phẩm đã tồn tại";
            public static string ProductNotExist = "Sản phẩm không tồn tại";
            public static string NegativeQuantity = "Số lượng phải lớn hơn 0";
            public static string ProductIsEmpty = "không có sản phẩm nào";

            public static string ProductIdEmpty = "Id của sản phẩm bị trống";
        }
    }
}
