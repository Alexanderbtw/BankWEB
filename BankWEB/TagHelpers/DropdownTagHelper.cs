using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures.Buffers;

namespace BankWEB.TagHelpers
{
    [HtmlTargetElement("drop-down")]
    public class DropdownTagHelper : TagHelper
    {
        public int TargetId { get; set; }
        public bool IsSafe { get; set; } = false;

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "div";
            output.TagMode = TagMode.StartTagAndEndTag;
            //output.AddClass("dropdown", );

            TagBuilder button = new("button");
            button.InnerHtml.AppendHtml("&#10247;");
            button.Attributes.Add("type", "button");
            button.Attributes.Add("data-bs-toggle", "dropdown");
            button.Attributes.Add("aria-expanded", "false");
            button.AddCssClass("btn bg-white p-0 m-0 border-0");

            TagBuilder ul = new("ul");
            ul.AddCssClass("dropdown-menu");
            TagBuilder liFirst = new("li");
            TagBuilder liLast = new("li");

            TagBuilder somethingLink = new("a");
            somethingLink.AddCssClass("dropdown-item");
            somethingLink.Attributes.Add("href", "#");
            somethingLink.InnerHtml.Append("Do Something");
            liFirst.InnerHtml.AppendHtml(somethingLink);
            ul.InnerHtml.AppendHtml(liFirst);

            if (!IsSafe)
            {
                TagBuilder deleteLink = new("a");
                deleteLink.AddCssClass("dropdown-item danger-dropdown-item");
                deleteLink.Attributes.Add("href", $"Home/DeleteAccount/{TargetId}");
                deleteLink.InnerHtml.Append("Delete");
                liLast.InnerHtml.AppendHtml(deleteLink);
                ul.InnerHtml.AppendHtml(liLast);
            }

            output.Content.AppendHtml(button);
            output.Content.AppendHtml(ul);
        }
    }
}
