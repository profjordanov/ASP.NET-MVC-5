using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Web.Mvc;
using System.Web.Mvc.Html;

public static class HtmlHelperExtensions
{
    public static MvcHtmlString EditorForMany<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, IEnumerable<TValue>>> expression, string htmlFieldName = null) where TModel : class
    {
        var items = expression.Compile()(html.ViewData.Model);
        var output = new StringBuilder();

        if (string.IsNullOrEmpty(htmlFieldName))
        {
            var prefix = html.ViewContext.ViewData.TemplateInfo.HtmlFieldPrefix;

            htmlFieldName = (prefix.Length > 0 ? (prefix + ".") : string.Empty) + ExpressionHelper.GetExpressionText(expression);
        }

        foreach (var item in items)
        {
            var dummy = new { Item = item };
            var guid = Guid.NewGuid().ToString();

            var memberExp = Expression.MakeMemberAccess(Expression.Constant(dummy), dummy.GetType().GetProperty("Item"));
            var singleItemExp = Expression.Lambda<Func<TModel, TValue>>(memberExp, expression.Parameters);

            output.Append(string.Format(@"<input type=""hidden"" name=""{0}.Index"" value=""{1}"" class=""form-control"" />", htmlFieldName, guid));
            output.Append(html.EditorFor(singleItemExp, "ImageUrl", string.Format("{0}[{1}]", htmlFieldName, guid)));
        }

        return new MvcHtmlString(output.ToString());
    }
}