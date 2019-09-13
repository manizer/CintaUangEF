using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CintaUang.Helpers.TagHelpers
{
    [HtmlTargetElement("datatable")]
    public class DatatableTagHelper : TagHelper
    {
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "table";
            output.Attributes.SetAttribute("class", "table table-striped table-bordered");
        }
    }
}
