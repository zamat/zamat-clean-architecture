using System;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace AUMS.AspNetCore.Mvc.TagHelpers;

[HtmlTargetElement(Attributes = "active-page")]
public class ActivePageTagHelper : AnchorTagHelper
{
    public ActivePageTagHelper(IHtmlGenerator generator) : base(generator)
    {
    }

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        var routeData = ViewContext.RouteData.Values;
        var currentPage = routeData["page"] as string;

        if (string.Equals(Page, currentPage, StringComparison.OrdinalIgnoreCase))
        {
            var currentClasses = output.Attributes["class"].Value.ToString();

            if (output.Attributes["class"] != null)
            {
                output.Attributes.Remove(output.Attributes["class"]);
            }

            output.Attributes.Add("class", $"{currentClasses} active");
        }
    }
}
