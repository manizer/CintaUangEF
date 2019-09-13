using Model.DTO.DB.DataTable;
using Repository.Base;
using Repository.Base.Helper;
using Repository.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories.CategoryRepositories
{
	public interface ICategoryDataTableRepository : IRepository<CategoryDataTableRowDTO>
	{
		Task<AjaxDataTableDTO<CategoryDataTableRowDTO>> GetCategoryDataTable(int Page, int Take, string Search, int OrderColIdx, string OrderDirection);
	}

	public class CategoryDataTableRepository : BaseRepository<CategoryDataTableRowDTO>, ICategoryDataTableRepository
	{
		public CategoryDataTableRepository(CintaUangDbContext dbContext, DbUtil dbUtil) : base(dbContext, dbUtil) { }

		public async Task<AjaxDataTableDTO<CategoryDataTableRowDTO>> GetCategoryDataTable(int Page, int Take, string Search, int OrderColIdx, string OrderDirection)
		{
			var sp = DbUtil.StoredProcedureBuilder.WithSPName("mscategory_getallpaginated")
				.AddParam(Page)
				.AddParam(Take)
				.AddParam(OrderDirection)
				.AddParam(OrderColIdx)
				.AddParam(Search)
				.SP();
			IEnumerable<CategoryDataTableRowDTO> categoryDataTableRows = await ExecSPToListAsync(sp);

			AjaxDataTableDTO<CategoryDataTableRowDTO> categoryAjaxDataTable = new AjaxDataTableDTO<CategoryDataTableRowDTO>
			{
				Data = categoryDataTableRows.ToList(),
				Draw = Page,
				RecordsFiltered = categoryDataTableRows != null && categoryDataTableRows.Count() > 0 ? Convert.ToInt32(categoryDataTableRows.First().TotalRecord) : categoryDataTableRows.Count(),
				RecordsTotal = categoryDataTableRows.Count()

			};
			return categoryAjaxDataTable;
		}
	}
}
