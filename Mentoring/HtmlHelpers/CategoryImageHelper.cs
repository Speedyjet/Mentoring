using Mentoring.BL;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using static System.Net.Mime.MediaTypeNames;

namespace Mentoring.HtmlHelpers
{
    public static class CategoryImageHelper
    {
        public static HtmlString CategoryImage(this IHtmlHelper html, byte[] categoryPic)
        {
            if (categoryPic != null)
            {
                string base64 = string.Empty;
                var imageArray = new List<byte>(categoryPic);
                if (imageArray != null)
                {
                    base64 = Convert.ToBase64String(imageArray.ToArray());
                    if (!string.IsNullOrEmpty(base64))
                    {
                        var resultString = $"<img src='data:image/bmp;base64,{base64}'/>";
                        return new HtmlString(resultString);
                    }
                }
            }
        return new HtmlString($"< div > No image available</ div >");
        }
    }
}
