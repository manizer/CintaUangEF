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
        ExecuteResultDTO InsertCategory(CategoryDTO categoryDTO);
		ExecuteResultDTO UpdateCategory(CategoryDTO categoryDTO);
		ExecuteResultDTO DeleteCategory(int Id);
	}

    public class CategoryRepository : BaseRepository<CategoryDTO>, ICategoryRepository
	{ 
        public CategoryRepository(CintaUangDbContext dbContext, DbContextOptions<CintaUangDbContext> options) : base(dbContext)
		{
		}

		public IEnumerable<CategoryDTO> GetCategories() => Context.Categories.Where(x => x.AuditedActivity != DBEnum.AUDITEDACTIVITY_DELETE);
		public CategoryDTO GetCategory(int id) => Context.Categories.Find(id);
        public ExecuteResultDTO InsertCategory(CategoryDTO categoryDTO)
        {
			Context.Categories.Add(categoryDTO);
			Context.SaveChanges();

			try
			{
				Context.Remove(categoryDTO);
				Context.SaveDeletion();
			}
			catch(Exception ex)
			{

			}
			return new ExecuteResultDTO
			{
				InstanceId = categoryDTO.Id
			};
        }

		public ExecuteResultDTO UpdateCategory(CategoryDTO categoryDTO)
		{
			Context.Categories.Update(categoryDTO);
			Context.SaveChanges();
			return new ExecuteResultDTO
			{
				InstanceId = categoryDTO.Id
			};
		}

		public ExecuteResultDTO DeleteCategory(int Id)
		{
			CategoryDTO categoryDTO = Context.Categories.FirstOrDefault(x => x.Id == Id);
			Context.Remove(categoryDTO);
			Context.SaveDeletion();
			return new ExecuteResultDTO
			{
				InstanceId = categoryDTO.Id
			};
		}
	}
}