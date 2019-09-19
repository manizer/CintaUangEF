using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Helper.Session;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Model.DTO.DB;
using Model.DTO.DB.CategoryDB;
using Model.DTO.DB.DataTable;
using Model.DTO.DB.ExpenseDB;
using Model.DTO.DB.SubCategoryDB;
using Model.DTO.DB.UserDB;

namespace Repository.Context
{
    public class CintaUangDbContext : DbContext
    {
		private readonly IHttpContextAccessor httpContextAccessor;

        public CintaUangDbContext(DbContextOptions<CintaUangDbContext> options, IHttpContextAccessor httpContextAccessor) : base(options)
        {
			this.httpContextAccessor = httpContextAccessor;
        }

		// TODO: Delete later
        public DbSet<ExecuteResultDTO> ExecuteResultDbSet { get; set; }
        public DbSet<CategoryDTO> CategoryDbSet { get; set; }
        public DbSet<SubCategoryDTO> SubCategoryDbSet { get; set; }
		public DbSet<UserDTO> UserDbSet { get; set; }

		public DbSet<ExecuteResultDTO> ExecuteResults { get; set; }
		public DbSet<CategoryDTO> Categories { get; set; }
		public DbSet<SubCategoryDTO> SubCategories { get; set; }
		public DbSet<UserDTO> Users { get; set; }
		public DbSet<ExpenseDTO> Expenses { get; set; }

		// DataTable
		public DbSet<ExpenseDataTableRowDTO> ExpenseDataTableRows { get; set; }

		public override int SaveChanges()
		{
			OnBeforeSave();
			return base.SaveChanges();
		}

		public int SaveDeletion()
		{
			OnBeforeDelete();
			return base.SaveChanges();
		}

		public override EntityEntry<TEntity> Remove<TEntity>(TEntity entity)
		{
			Entry(entity).State = EntityState.Deleted;
			return base.Update(entity);
		}

		private void OnBeforeSave()
		{
			var httpContext = httpContextAccessor.HttpContext;
			IEnumerable<EntityEntry> entries = ChangeTracker.Entries();
			foreach (var entry in entries)
			{
				if (entry.Entity is BaseModel model)
				{
					model.SetAuditedTime(DateTime.Now);
					if (httpContext.Session.GetLoginUserId() == null) throw new NullReferenceException("Session Login UserId is null");
					model.SetAuditedUserId(httpContext.Session.GetLoginUserId() ?? 0);

					switch (entry.State)
					{
						case EntityState.Added:
							model.SetAuditedActivity(DBEnum.AUDITEDACTIVITY_INSERT);
							break;
						case EntityState.Modified:
							model.SetAuditedActivity(DBEnum.AUDITEDACTIVITY_UPDATE);
							break;
						case EntityState.Deleted:
							model.SetAuditedActivity(DBEnum.AUDITEDACTIVITY_DELETE);
							break;
					}
				}
			}
		}

		private void OnBeforeDelete()
		{
			var httpContext = httpContextAccessor.HttpContext;
			IEnumerable<EntityEntry> entries = ChangeTracker.Entries();
			foreach (var entry in entries)
			{
				if (entry.Entity is BaseModel model)
				{
					model.SetAuditedTime(DateTime.Now);
					if (httpContext.Session.GetLoginUserId() == null) throw new NullReferenceException("Session Login UserId is null");
					model.SetAuditedUserId(httpContext.Session.GetLoginUserId() ?? 0);
					model.SetAuditedActivity(DBEnum.AUDITEDACTIVITY_DELETE);
				}
			}
		}
	}
}
