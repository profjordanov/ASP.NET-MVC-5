using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        public static MvcHtmlString Image(this HtmlHelper helper, string url, string alt = null, string width = "150px",
           string height = "150px" )
        {
            TagBuilder builder = new TagBuilder("img");
            builder.AddCssClass("img-thumbnail");
            builder.MergeAttribute("src", url);
            builder.MergeAttribute("alt", alt);
            builder.MergeAttribute("width", width);
            builder.MergeAttribute("height", height);

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

        public static MvcHtmlString YouTube2(this HtmlHelper helper, string videoId, int width = 600, int height = 400)
        {
            TagBuilder builder = new TagBuilder("iframe");
            builder.MergeAttribute("width", $"{width}");
            builder.MergeAttribute("height", $"{height}");
            builder.MergeAttribute("src", $"https://www.youtube.com/embed/{videoId}");
            builder.MergeAttribute("frameborder", "0");
            builder.MergeAttribute("allowfullscreen", "allowfullscreen");

            return new MvcHtmlString(builder.ToString(TagRenderMode.Normal));
        }

        public static MvcHtmlString Table<T>(this HtmlHelper helper, IEnumerable<T> models, params string[] cssClasses)
        {
            TagBuilder table = new TagBuilder("table");
            StringBuilder tableInnerHtml = new StringBuilder();
            string[] propertyNames = typeof(T).GetProperties().Select(info => info.Name).ToArray();
            foreach (var cssClass in cssClasses)
            {
                table.AddCssClass(cssClass);
            }

            TagBuilder tableHeaderRow = new TagBuilder("tr");
            StringBuilder tableHeaderInnerHtml = new StringBuilder();
            foreach (var propertyName in propertyNames)
            {
                TagBuilder tableData = new TagBuilder("th");
                tableData.InnerHtml = propertyName;
                tableHeaderInnerHtml.Append(tableData);
            }
            tableHeaderRow.InnerHtml = tableHeaderInnerHtml.ToString();
            tableInnerHtml.Append(tableHeaderRow);

            foreach (var model in models)
            {
                TagBuilder tableDataRow = new TagBuilder("tr");
                StringBuilder tableDataRowInnerHtml = new StringBuilder();
                foreach (var propertyName in propertyNames)
                {
                    TagBuilder tableData = new TagBuilder("td");
                    tableData.InnerHtml = typeof(T).GetProperty(propertyName).GetValue(model).ToString();
                    tableDataRowInnerHtml.Append(tableData);
                }
                tableDataRow.InnerHtml = tableDataRowInnerHtml.ToString();
                tableInnerHtml.Append(tableDataRow);
            }
            table.InnerHtml = tableInnerHtml.ToString();

            return new MvcHtmlString(table.ToString(TagRenderMode.Normal));
        }
    }
}