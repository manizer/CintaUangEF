using System;
using Microsoft.EntityFrameworkCore;
using Model.DTO.DB;
using Model.DTO.DB.CategoryDB;
using Model.DTO.DB.DataTable;
using Model.DTO.DB.SubCategoryDB;
using Model.DTO.DB.UserDB;

namespace Repository.Context
{
    public class CintaUangDbContext : DbContext
    {
        public CintaUangDbContext(DbContextOptions<CintaUangDbContext> options) : base(options)
        {
        }

        public DbSet<ExecuteResultDTO> ExecuteResultDbSet { get; set; }
        public DbSet<CategoryDTO> CategoryDbSet { get; set; }
        public DbSet<SubCategoryDTO> SubCategoryDbSet { get; set; }
		public DbSet<UserDTO> UserDbSet { get; set; }

		// DataTable
        public DbSet<CategoryDataTableRowDTO> CategoryDataTableRowDbSet { get; set; }
    }
}
