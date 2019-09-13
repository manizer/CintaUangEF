using Microsoft.EntityFrameworkCore;
using Model.DTO.DB.MenuDB;
using Repository.Base;
using Repository.Base.Helper;
using Repository.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Model.Domains.LayoutDomains.LayoutDomain;

namespace Repository.Repositories.MenuRepositories
{
	public interface IMenuRepository : IRepository<MenuDTO>
	{
		Task<IEnumerable<Menu>> GetAll();
	}

	public class MenuRepository : BaseRepository<MenuDTO>, IMenuRepository
	{
		private readonly DbContextOptions<CintaUangDbContext> options;

		public MenuRepository(CintaUangDbContext dbContext, DbUtil dbUtil,
			DbContextOptions<CintaUangDbContext> options) : base(dbContext, dbUtil)
		{
			this.options = options;
		}

		public async Task<IEnumerable<Menu>> GetAll()
		{
			var sp = DbUtil.StoredProcedureBuilder
				.WithSPName("msmenu_getall")
				.SP();
			IEnumerable<MenuDTO> menuDtos = await ExecSPToListAsync(sp);
			IEnumerable<Menu> menus = menuDtos.Select(x => new Menu
			{
				Icon = x.Icon,
				Id = x.Id,
				Name = x.Name,
				ParentId = x.ParentId,
				Position = x.Position,
				Url = x.Url
			});
			return menus;
		}
	}
}
