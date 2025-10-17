using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Xml.Linq;

namespace Lern.ge.Controllers
{
    public class SitemapController : Controller
    {
        [Route("sitemap.xml")]
        public IActionResult Index()
        {
            var baseUrl = $"{Request.Scheme}://{Request.Host}";

            // მხოლოდ მთავარი გვერდი
            var pages = new List<string>
            {
                ""
            };

            XNamespace ns = "http://www.sitemaps.org/schemas/sitemap/0.9";
            var sitemap = new XDocument(new XElement(ns + "urlset",
                from page in pages
                select new XElement(ns + "url",
                    new XElement(ns + "loc", $"{baseUrl}/{page}"),
                    new XElement(ns + "changefreq", "weekly"),
                    new XElement(ns + "priority", "1.0")
                )
            ));

            return Content(sitemap.ToString(), "application/xml", Encoding.UTF8);
        }
    }
}
