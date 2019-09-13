using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Model.Domain.DB.CategoryDB;
using Model.Lib.DropdownLibs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CintaUang.Helpers.TagHelpers.DDLTagHelper
{
	[HtmlTargetElement("lovelydropdown")]
	public class LovelyDropdown : SelectTagHelper
	{
		protected IHtmlGenerator generator;
		public List<DropdownItem> Data { get; set; }
		
		public LovelyDropdown(IHtmlGenerator generator): base(generator)
		{
			this.generator = generator;
		}
		
		public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
		{
			Items = Dropdown.From(Data ?? new List<DropdownItem>());
			await base.ProcessAsync(context, output);
			output.TagName = "select";

			string id = output.Attributes.FirstOrDefault(attribute => attribute.Name == "id").Value.ToString();
			SelectListItemsRegistry.Get().AddToDict(id, Data);

			string options = BuildOptionsTag();
			output.Content.SetHtmlContent(options);
		}

		private string BuildOptionsTag()
		{
			StringBuilder selectStringBuilder = new StringBuilder();
			Data?.ForEach(datum =>
			{
				selectStringBuilder.Append($"<option value=\"{datum.DropdownValue()}\">{datum.DropdownText()}</option>");
			});
			return selectStringBuilder.ToString();
		}
	}
}
