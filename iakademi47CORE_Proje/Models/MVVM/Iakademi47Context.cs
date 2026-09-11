using Microsoft.EntityFrameworkCore;

namespace iakademi47CORE_Proje.Models.MVVM
{
  public class Iakademi47Context : DbContext
  {
    //OnConfiguring
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");

      var configuration = builder.Build();
      optionsBuilder.UseSqlServer(configuration["ConnectionStrings:iakademi47Connection"]);
    }
    public DbSet<Category>? Categories { get; set; }
    public DbSet<Order>? Orders { get; set; }
    public DbSet<Product>? Products { get; set; }
    public DbSet<Setting>? Settings { get; set; }
    public DbSet<Status>? Statuses { get; set; }
    public DbSet<Supplier>? Suppliers { get; set; }
    public DbSet<User>? Users { get; set; }
    public DbSet<Vw_MyOrder> Vw_MyOrders { get; set; }
	public DbSet<Sp_Search> Sp_Searches { get; set; }


	}
}
