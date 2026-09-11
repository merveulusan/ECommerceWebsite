using iakademi47CORE_Proje.Models.MVVM;
using Microsoft.EntityFrameworkCore;

namespace iakademi47CORE_Proje.Models.Concrete
{
  public class QueryRepository
  {
    Iakademi47Context context = new Iakademi47Context();


    public async Task Query()
    {
      //tek kayıt ,bütün kolonları
      Product? product = context.Products?.FirstOrDefault(p => p.ProductID == 4);
      Product? product1 = await context.Products.FirstOrDefaultAsync(p => p.ProductID == 4);


      string? productName = context.Products?.FirstOrDefault(p => p.ProductID == 4).ProductName;

      decimal UnitPrice = context.Products.FirstOrDefault(p => p.ProductID == 4).UnitPrice;


      List<Product> products = context.Products.ToList();
      List<Product> products2=context.Products.Where(p =>p.CategoryID==7).ToList();
    }




  }
}
