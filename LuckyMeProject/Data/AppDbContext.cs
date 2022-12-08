using LuckyMeProject.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace LuckyMeProject.Data
{
	public class AppDbContext : DbContext
	{
		public AppDbContext() : base("aspnet-lucyme")
		{ }
		public DbSet<Player> Players { get; set; }
		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			//Database.SetInitializer<demoEntities>(null);
			modelBuilder.Entity<Player>().ToTable("Players");
			modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

			base.OnModelCreating(modelBuilder);
		}

	}
}