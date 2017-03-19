using System.Collections;
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

        public static MvcHtmlString YouTube(this HtmlHelper helper, string url, int? width, int? height)
        {
            TagBuilder builder = new TagBuilder("iframe");
            builder.MergeAttribute("src", $"https://www.youtube.com/embed/{url}");
            if (width != null)
            {
                builder.MergeAttribute("width", width.ToString());
            }
            else
            {
                builder.MergeAttribute("width", "560");
            }
            if (height != null)
            {
                builder.MergeAttribute("height", height.ToString());
            }
            else
            {
                builder.MergeAttribute("height", "315");
            }
            builder.MergeAttribute("frameborder", "0");
            builder.MergeAttribute("allowfullscreen", null);
            builder.AddCssClass("embed-responsive-item");
            return new MvcHtmlString(builder.ToString(TagRenderMode.Normal));

        }
    }
}