using BankWEB.Models;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text.Encodings.Web;

namespace BankWEB.Helpers
{
    public static class AccountListHelper
    {
        public static HtmlString CreateDropDownList(this IHtmlHelper htmlHelper, IEnumerable<Account> accounts, string name, string extClass = "")
        {
            TagBuilder select = new TagBuilder("select");
            select.AddCssClass("form-select");
            select.AddCssClass(extClass);
            select.Attributes.Add("aria-label", "Default select example");
            select.Attributes.Add("name", name);
            select.Attributes.Add("required", "required");
            TagBuilder option = new TagBuilder("option");
            option.Attributes.Add("selected", "selected");
            option.Attributes.Add("disabled", "disabled");
            option.Attributes.Add("value", "-1");
            option.InnerHtml.Append("--Select Account--");
            select.InnerHtml.AppendHtml(option);
            foreach (var account in accounts)
            {
                TagBuilder opt = new TagBuilder("option");
                opt.InnerHtml.Append(account.Title);
                opt.Attributes.Add("value", account.Id.ToString());
                select.InnerHtml.AppendHtml(opt);
            }
            using StringWriter writer = new StringWriter();
            select.WriteTo(writer, HtmlEncoder.Default);
            return new HtmlString(writer.ToString());
        }
    }
}
