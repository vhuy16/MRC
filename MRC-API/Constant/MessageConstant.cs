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
        }
        public static class ProductMessage
        {
            public const string CreateProductFail = "Tạo mới sản phẩm thất bại";
        }
    }
}
