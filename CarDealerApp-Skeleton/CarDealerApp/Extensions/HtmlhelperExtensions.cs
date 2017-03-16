using System.Web.Mvc;

namespace CarDealerApp.Extensions
{
    public static class HtmlhelperExtensions
    {
        public static MvcHtmlString Button(this HtmlHelper helper, string text, string cssClasses)
        {
            TagBuilder builder = new TagBuilder("button");
            builder.AddCssClass(cssClasses);
            builder.MergeAttribute("type", "submit");
            builder.InnerHtml = text;
            return new MvcHtmlString(builder.ToString(TagRenderMode.Normal));
        }

        public static MvcHtmlString Image(this HtmlHelper helper, string url, string alt)
        {
            TagBuilder builder = new TagBuilder("img");
            builder.AddCssClass("img-thumbnail");
            builder.MergeAttribute("src", url);
            builder.MergeAttribute("alt", alt);
            return new MvcHtmlString(builder.ToString(TagRenderMode.SelfClosing));
        }
    }
}