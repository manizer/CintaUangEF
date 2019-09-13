using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Model.Domain.DB;
using Model.DTO.DB;
using Model.DTO.DB.CategoryDB;
using Repository.Base;
using Repository.Base.Helper;
using Repository.Base.Helper.StoredProcedure;
using Repository.Context;
using Repository.Repositories.SubCategoryRepositories;

namespace Repository.Repositories.CategoryRepositories
{
    public interface ICategoryRepository : IRepository<CategoryDTO>
    {
        IEnumerable<CategoryDTO> GetCategories();
        CategoryDTO GetCategory(int id);
        ExecuteResultDTO InsertCategory(InsertCategoryDTO insertCategoryDTO);
		ExecuteResultDTO UpdateCategory(UpdateCategoryDTO insertCategoryDTO);
	}

    public class CategoryRepository : BaseRepository<CategoryDTO>, ICategoryRepository
	{ 
		private readonly ISubCategoryRepository subCategoryRepository;
		private readonly DbContextOptions<CintaUangDbContext> options;

        public CategoryRepository(CintaUangDbContext dbContext, DbUtil dbUtil,
			ISubCategoryRepository subCategoryRepository,
			DbContextOptions<CintaUangDbContext> options,
			UnitOfWork unitOfWork) : base(dbContext, dbUtil)
		{
			this.subCategoryRepository = subCategoryRepository;
			this.options = options;
		}

		public IEnumerable<CategoryDTO> GetCategories() => Context.Categories;
		public CategoryDTO GetCategory(int id) => Context.Categories.Find(id);
        public ExecuteResultDTO InsertCategory(InsertCategoryDTO insertCategoryDTO)
        {
			CategoryDTO categoryDTO = new CategoryDTO
			{
				Name = insertCategoryDTO.Name,
				AuditedActivity = DBEnum.AUDITEDACTIVITY_INSERT,
				AuditedTime = DateTime.Now,
				AuditedUserId = insertCategoryDTO.AuditedUserId
			};
			Context.Categories.Add(categoryDTO);
			Context.SaveChanges();
			return new ExecuteResultDTO
			{
				InstanceId = categoryDTO.Id
			};
        }

		public ExecuteResultDTO UpdateCategory(UpdateCategoryDTO updateCategoryDTO)
		{
			CategoryDTO categoryDTO = new CategoryDTO
			{
				Id = updateCategoryDTO.Id,
				Name = updateCategoryDTO.Name,
				AuditedUserId = updateCategoryDTO.AuditedUserId,
				AuditedActivity = DBEnum.AUDITEDACTIVITY_DELETE,
				AuditedTime = DateTime.Now
			};
			Context.Categories.Update(categoryDTO);
			Context.SaveChanges();
			return new ExecuteResultDTO
			{
				InstanceId = categoryDTO.Id
			};
		}
	}
}