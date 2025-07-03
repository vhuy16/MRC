using Ganss.Xss;

namespace MRC_API.Utils
{
    public class HtmlSanitizerUtils
    {
        public string Sanitize(string htmlContent)
        {
            if (string.IsNullOrWhiteSpace(htmlContent))
                return string.Empty;

            var sanitizer = new HtmlSanitizer();

            // Allow specific tags
            sanitizer.AllowedTags.Add("p");
            sanitizer.AllowedTags.Add("a");
            sanitizer.AllowedTags.Add("img");

            // Allow specific attributes
            sanitizer.AllowedAttributes.Add("href");
            sanitizer.AllowedAttributes.Add("rel");
            sanitizer.AllowedAttributes.Add("target");
            sanitizer.AllowedAttributes.Add("style");
            sanitizer.AllowedAttributes.Add("src");
            sanitizer.AllowedAttributes.Add("alt");
            sanitizer.AllowedAttributes.Add("class");

            return sanitizer.Sanitize(htmlContent);
        }
    }
}
