using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using Microsoft.Ajax.Utilities;
using PlaySchool.Models;

namespace PlaySchool.Helpers
{
    public static class HtmlExtensions
    {
        //public static MvcHtmlString Pagination(this HtmlHelper helper, int totalPages, int currentPage)
        //{
        //    TagBuilder builder = new TagBuilder("asd");
        //    if (totalPages < 10)
        //    {
        //        
        //    }
        //}
        public static MvcHtmlString ProfileImage(this HtmlHelper helper, string imageUrl)
        {
            TagBuilder builder = new TagBuilder("img");
            builder.AddCssClass("img-rounded");
            builder.AddCssClass("img-responsive");
            builder.MergeAttribute("src", imageUrl);
            builder.MergeAttribute("alt", "Profile Picture");

            builder.MergeAttribute("width", "200px");
            builder.MergeAttribute("height", "200px");

            return new MvcHtmlString(builder.ToString(TagRenderMode.SelfClosing));
        }
    }
}