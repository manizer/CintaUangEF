using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Model.Lib.DropdownLibs;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CintaUang.Helpers.TagHelpers.DDLTagHelper
{
	[HtmlTargetElement("lovelyautolink")]
	public class LoveDropdownAutoLink : TagHelper
	{
		protected IHtmlGenerator generator;

		public string Provider { get; set; }
		public string ProvideFor { get; set; }
		public string SubDropdownKey { get; set; }

		public LoveDropdownAutoLink(IHtmlGenerator generator)
		{
			this.generator = generator;
		}

		public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
		{
			output.TagName = "script";

			string scripts = BuildScript();
			output.Content.SetHtmlContent(scripts);
		}

		private string BuildScript()
		{
			List<string> SubscriberDropdowns = ProvideFor.Split(';').ToList();
			StringBuilder scriptStringBuilder = new StringBuilder();
			/**
			 * Add
			 */
			SubscriberDropdowns.ForEach(subscriberDropdown =>
			{
				scriptStringBuilder.Append($@"
					$(document).off('change').on('change', '#{Provider}', function(event){{
						const selected = $(this).val();

						$.ajax({{
							url: `${{BASE_URL}}/DropdownProvider/ChildDropdown?ParentId={Provider}&SubDropdownKey={SubDropdownKey}&ParentValue=${{selected}}`,
							success: function(selectListItems){{
								$('#{subscriberDropdown}').empty();
								selectListItems.forEach(selectListItem => {{
									$('<option/>').val(selectListItem.value).html(selectListItem.text).appendTo('#{subscriberDropdown}');
								}});
							}},
							error: function(err){{
								console.error('Ddl autolink error: ', err);
							}}
						}});
					}})");
				scriptStringBuilder.Append($@"
					$('#{Provider}').trigger('change');
				");
			});
			return scriptStringBuilder.ToString();
		}
	}
}
