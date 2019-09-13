using Model.Domain.DB;
using Model.Domain.DB.CategoryDB;
using Model.Domain.DB.SubCategoryDB;
using Model.DTO.DB;
using Model.DTO.DB.SubCategoryDB;
using Repository.Base;
using Repository.Base.Helper;
using Repository.Base.Helper.StoredProcedure;
using Repository.Context;
using Repository.Repositories.CategoryRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories.SubCategoryRepositories
{
	public interface ISubCategoryRepository : IRepository<SubCategoryDTO>
	{
		Task<IEnumerable<SubCategory>> GetSubCategoriesByCategoryID(int CategoryID);
		Task<ExecuteResult> InsertSubCategory(InsertSubCategoryDTO insertSubCategoryDTO);
		Task<ExecuteResult> UpdateSubCategory(UpdateSubCategoryDTO updateSubCategoryDTO);
	}

	public class SubCategoryRepository : BaseRepository<SubCategoryDTO>, ISubCategoryRepository
	{
		public SubCategoryRepository(CintaUangDbContext dbContext, DbUtil dbUtil) : base(dbContext, dbUtil)
		{
		}

		public async Task<IEnumerable<SubCategory>> GetSubCategoriesByCategoryID(int CategoryID)
		{
			var sp = DbUtil.StoredProcedureBuilder
				.WithSPName("mssubcategory_getbycategoryid")
				.AddParam(CategoryID)
				.SP();
			var subcategorieDtos = await ExecSPToListAsync(sp);
			return subcategorieDtos.Select(x => new SubCategory
			{
				Id = x.Id,
				CategoryId = x.CategoryId,
				Name = x.Name
			});
		}

		public async Task<ExecuteResult> InsertSubCategory(InsertSubCategoryDTO insertSubCategoryDTO)
		{
			List<StoredProcedure> storedProcedures = new List<StoredProcedure>();
			storedProcedures.Add(
				DbUtil.StoredProcedureBuilder.WithSPName("mssubcategory_insert")
				.AddParam(insertSubCategoryDTO.CategoryId)
				.AddParam(insertSubCategoryDTO.SubCategoryName)
				.AddParam(insertSubCategoryDTO.AuditedUserId)
				.SP()
			);

			IEnumerable<ExecuteResultDTO> executeResults = await ExecSPWithTransaction<ExecuteResultDTO>(storedProcedures.ToArray());
			return executeResults.Select(x => new ExecuteResult
			{
				InstanceId = x.InstanceId
			}).FirstOrDefault();
		}

		public async Task<ExecuteResult> UpdateSubCategory(UpdateSubCategoryDTO updateSubCategoryDTO)
		{
			List<StoredProcedure> storedProcedures = new List<StoredProcedure>();
			storedProcedures.Add(
				DbUtil.StoredProcedureBuilder.WithSPName("mssubcategory_update")
				.AddParam(updateSubCategoryDTO.SubCategoryId)
				.AddParam(updateSubCategoryDTO.CategoryId)
				.AddParam(updateSubCategoryDTO.SubCategoryName)
				.AddParam(updateSubCategoryDTO.AuditedUserId)
				.SP()
			);

			IEnumerable<ExecuteResultDTO> executeResults = await ExecSPWithTransaction<ExecuteResultDTO>(storedProcedures.ToArray());
			return executeResults.Select(x => new ExecuteResult
			{
				InstanceId = x.InstanceId
			}).FirstOrDefault();
		}
	}
}
