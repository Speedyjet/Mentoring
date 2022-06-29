using Mentoring.BL;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Mentoring.TagHelpers
{
    public class NorthwindTagHelper : TagHelper
    {
        private ICategoryService _businessLogic;

        public NorthwindTagHelper(ICategoryService businessLogic)
        {
            _businessLogic = businessLogic;
        }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            var imageId = context.AllAttributes.FirstOrDefault(x=> x.Name == "nordwind-id").Value;
            string resultString;
            if (!string.IsNullOrEmpty(imageId.ToString()) && int.TryParse(imageId.ToString(), out int imageIdInt))
            {
                var imageArray = _businessLogic.GetImageById(imageIdInt).Result;

                string base64 = string.Empty;
                if (imageArray != null)
                {
                    base64 = Convert.ToBase64String(imageArray.ToArray());
                    if (!string.IsNullOrEmpty(base64))
                    {
                        resultString = $"data:image/bmp;base64,{base64}";
                        output.TagName = "img";
                        output.Attributes.Add("src", resultString);
                    }
                }
                else
                {
                    output.TagName = "div";
                    output.Content.SetContent("No Image available");
                }
            }
            //base.Process(context, output);
        }
    }
}
