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
        public static class PaymentMessage
        {
            public const string CreatePaymentFail = "Failed to create payment URL.";
            public const string PaymentNotFound = "Payment not found.";
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
            public static string ProductIsEmpty = "Không có sản phẩm nào";
            public static string ProductNotEnough = "Số lượng trong kho không đủ";
            public static string ProductIdEmpty = "Id của sản phẩm bị trống";
        }

        public static class OrderMessage
        {
            public const string CreateOrderFail = "Tạo hoá đơn thất bại";
            public const string OrderIsEmpty = "Không có hoá đơn nào";
        }
        public static class EmailMessage
        {
            public const string InvalidEmailRequest = "thông tin mail không hợp lệ";
            public const string EmailSentSuccessfully = "mail đã được gửi thành công";
            public const string EmailSendFail = "gửi mail thất bại";
           
        }
        public static class EmailTemplates
        {
            public const string WelcomeSubject = "Welcome to MRC!";

            public const string WelcomeMessage = @"
    <html>
    <body>
        <h2>Welcome to MRC, {0}!</h2>
        <p>Dear {0},</p>
        <p>Thank you for joining MRC. Your account has been successfully created.</p>
        <p>Here are your account details:</p>
        <ul>
            <li><strong>Username:</strong> {1}</li>
        </ul>
        <p>We are excited to have you on board and look forward to serving you.</p>
        <p>If you have any questions or need assistance, feel free to reach out to our support team.</p>
        <p>Best regards,</p>
        <p><strong>The MRC Team</strong></p>
    </body>
    </html>";
        }

        public static class CartMessage
        {
            public const string AddCartItemFail = "Thêm vào giỏ hàng thất bại";
            public const string CartItemNotExist = "Không có sản phẩm này trong giỏ hàng";
            public const string CartItemIsEmpty = "Không có sản phẩm nào trong giỏ hàng";
            public const string NegativeQuantity = "Số lượng phải lớn hơn 0";
        }
        public static class ServiceMessage
        {
            public const string ServiceExisted = "The service already exists.";
            public const string ServiceNotExist = "The service does not exist.";
            public const string ServiceCreatedSuccessfully = "The service has been created successfully.";
            public const string ServiceDeletedSuccessfully = "The service has been deleted successfully.";
            public const string ServiceUpdatedSuccessfully = "The service has been updated successfully.";
            public const string CreateServiceFail = "Failed to create the service. Please try again.";
            public const string UpdateServiceFail = "Failed to update the service. Please try again.";
            public const string DeleteServiceFail = "Failed to delete the service. Please try again.";
            public const string ServiceExists = "A service with this name already exists.";
            public const string ServiceIsEmpty = "No services found.";
        }
        public static class BookingMessage
        {
            public const string CreateBookingFail = "Failed to create the booking. Please try again.";
            public const string BookingNotExist = "The specified booking does not exist.";
            public const string UpdateBookingFail = "Failed to update the booking. Please try again.";
            public const string DeleteBookingFail = "Failed to delete the booking. Please try again.";
            public const string BookingExists = "A booking with this ID already exists.";
            public const string BookingIsEmpty = "No bookings found.";
            public const string BookingDateInvalid = "The booking date is invalid. Please select a valid date.";
            public const string BookingAlreadyConfirmed = "This booking has already been confirmed.";
        }
    }
}
