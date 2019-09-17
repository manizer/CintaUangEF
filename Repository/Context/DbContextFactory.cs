using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Context
{
	public class DbContextFactory
	{
		private readonly IHttpContextAccessor httpContextAccessor;
		private readonly DbContextOptions<CintaUangDbContext> options;

		public DbContextFactory(IHttpContextAccessor httpContextAccessor, DbContextOptions<CintaUangDbContext> options)
		{
			this.httpContextAccessor = httpContextAccessor;
			this.options = options;
		}

		public DbContext GetContext() => new CintaUangDbContext(options, httpContextAccessor);
	}
}
