using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using System.Text;

namespace Mentoring.ViewComponents
{
    public class BreadCrumbs : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            var routeDataValues = this.RouteData.Values;
            var result = new List<string>();
            if (routeDataValues != null)
            {
                result.Add("Home");
                foreach (var controller in routeDataValues.Values.ToList())
                {
                    result.Add(">");
                    result.Add(controller.ToString());
                }
            }
            return View(result);
        }
    }
}
