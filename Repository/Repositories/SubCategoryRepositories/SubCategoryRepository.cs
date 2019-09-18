using Model.Domain.DB;
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
		IEnumerable<SubCategoryDTO> GetSubCategoriesByCategoryID(int CategoryID);
		ExecuteResultDTO InsertSubCategory(InsertSubCategoryDTO insertSubCategoryDTO);
		ExecuteResultDTO UpdateSubCategory(UpdateSubCategoryDTO updateSubCategoryDTO);
	}

	public class SubCategoryRepository : BaseRepository<SubCategoryDTO>, ISubCategoryRepository
	{
		public SubCategoryRepository(CintaUangDbContext dbContext) : base(dbContext)
		{
		}

		public IEnumerable<SubCategoryDTO> GetSubCategoriesByCategoryID(int CategoryID) => Context.SubCategories.Where(x =>
			x.CategoryId == CategoryID &&
			!x.AuditedActivity.Equals(DBEnum.AUDITEDACTIVITY_DELETE));

		public ExecuteResultDTO InsertSubCategory(InsertSubCategoryDTO insertSubCategoryDTO)
		{
			SubCategoryDTO subCategoryDTO = new SubCategoryDTO
			{
				CategoryId = insertSubCategoryDTO.CategoryId,
				Name = insertSubCategoryDTO.SubCategoryName
			};

			Context.SubCategories.Add(subCategoryDTO);
			Context.SaveChanges();
			return new ExecuteResultDTO
			{
				InstanceId = subCategoryDTO.Id
			};
		}

		public ExecuteResultDTO UpdateSubCategory(UpdateSubCategoryDTO updateSubCategoryDTO)
		{
			SubCategoryDTO subCategoryDTO = new SubCategoryDTO
			{
				Id = updateSubCategoryDTO.SubCategoryId,
				CategoryId = updateSubCategoryDTO.CategoryId,
				Name = updateSubCategoryDTO.SubCategoryName
			};

			Context.SubCategories.Add(subCategoryDTO);
			Context.SaveChanges();
			return new ExecuteResultDTO
			{
				InstanceId = subCategoryDTO.Id
			};
		}
	}
}
