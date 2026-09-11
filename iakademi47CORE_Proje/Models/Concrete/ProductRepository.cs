using iakademi47CORE_Proje.Models.MVVM;
using Microsoft.EntityFrameworkCore;
using X.PagedList;

namespace iakademi47CORE_Proje.Models.Concrete
{
  public class ProductRepository
  {
    public int mainpagecount { get; set; }
    public int subpagecount { get; set; }
    public int Page { get; set; }

    //ctrl M+O = kapalı görünüm
    //ctrl M+L = açık görünüm
    Iakademi47Context context = new Iakademi47Context();

    public async Task<List<Product>?> Get(string mainPageName, int pagenumber)
    {
      List<Product>? products;

      if (mainPageName == "Slider")
      {
        //select top 5 * from Products  where StatusID=1 and Active=1
        products = context.Products?.Where(p => p.StatusID == 1 && p.Active == true).Take(mainpagecount).ToList();
      }




      else if (mainPageName == "New")
      {
        if (pagenumber == -1)
        {
          //-1  ana sayfa slider yeni ürün= 15 ürün
          products = context.Products?.Where(p => p.Active == true).OrderByDescending(p => p.AddDate).Take(mainpagecount).ToList();
        }
       else if (pagenumber == 0)
        {
          //0 menü ilk tıklama = ilk 8 ürün
          products = context.Products?.Where(p => p.Active == true).OrderByDescending(p => p.AddDate).Take(subpagecount).ToList();
        }
        else
        {
          //1,2,3,4,5   
          products = context.Products?.Where(p => p.Active == true).OrderByDescending(p => p.AddDate).Skip(pagenumber * subpagecount).Take(subpagecount).ToList();
        }
      }






      else if (mainPageName == "Special")
      {
        if (pagenumber == -1)  //home/Index 
        {
          products = context.Products?.Where(p => p.StatusID == 2 && p.Active == true).Take(mainpagecount).ToList();
        }
        else if (pagenumber == 0) //menü ilk tıklama = ilk 8 ürün
        {
          products = context.Products?.Where(p => p.StatusID == 2 && p.Active == true).Take(subpagecount).ToList();
        }
        else //ajax
        {
          products = context.Products?.Where(p => p.StatusID == 2 && p.Active == true).Skip(pagenumber * subpagecount).Take(subpagecount).ToList();
        }
      }




      else if (mainPageName == "Discounted")
      {
        if (pagenumber == -1) //Home/Index
        {
          products = context.Products?.Where(p => p.Active == true).OrderByDescending(p => p.Discount).Take(mainpagecount).ToList();
        }
        else if (pagenumber == 0) //menu ye ilk tıklandıgında
        {
          products = context.Products?.Where(p => p.Active == true).OrderByDescending(p => p.Discount).Take(subpagecount).ToList();
        }
        else//ajax -- scroll
        {
          products = context.Products?.Where(p => p.Active == true).OrderByDescending(p => p.Discount).Skip(pagenumber * subpagecount).Take(subpagecount).ToList();
        }
      }






      else if (mainPageName == "Highlighted")
      {
        if (pagenumber == -1)//Home/Index
        {
          products = context.Products?.Where(p => p.Active == true).OrderByDescending(p => p.HighLighted).Take(mainpagecount).ToList();
        }
        else if (pagenumber == 0) //menu ye ilk tıklandıgında
        {
          products = context.Products?.Where(p => p.Active == true).OrderByDescending(p => p.HighLighted).Take(subpagecount).ToList();
        }
        else//ajax -- scroll
        {
          products = context.Products?.Where(p => p.Active == true).OrderByDescending(p => p.HighLighted).Skip(pagenumber * subpagecount).Take(subpagecount).ToList();
        }
      }





      else if (mainPageName == "Topseller")
      {
        products = context.Products?.Where(p => p.Active == true).OrderByDescending(p => p.TopSeller).Take(mainpagecount).ToList();
      }

      else if (mainPageName == "Star")
      {
        products = context.Products?.Where(p => p.StatusID == 3 && p.Active == true).Take(mainpagecount).ToList();
      }

      else if (mainPageName == "Opportunity")
      {
        products = context.Products?.Where(p => p.StatusID == 4 && p.Active == true).Take(mainpagecount).ToList();
      }

      else if (mainPageName == "Notable")
      {
        products = context.Products?.Where(p => p.StatusID == 5 && p.Active == true).Take(mainpagecount).ToList();
      }

      else
      {
        products = context.Products?.ToList();
      }
      return products;
    }

    #region MyRegion
    //ana sayfa kategori, anasayfa marka,admin ürün listesi
    #endregion
    public List<Product>? Get(int id, string TableName)
    {
      List<Product>? products;
      if (TableName == "Category")
      {
        //ana sayfa kategori tıklanınca  ürün listesi
        products = context.Products?.Where(p => p.CategoryID == id).ToList();
      }

      else if (TableName == "Supplier")
      {
        //ana sayfa marka tıklanınca  ürün listesi
        products = context.Products?.Where(p => p.SupplierID == id).ToList();
      }

      else
      {
        //admin tarafında ürün listesi
        products = context.Products?.ToList();
      }
      return products;
    }

    //metod overload = tekrar Get metodu yazıyoruz,parametresi(parantez içindeki kısım - int,string) sırası farklı
    public async Task<Product> Get(int? id)
    {
      Product? product;
      if (id == 0)
      {
        //ana sayfada günün ürünü için burası
        product = await context.Products?.FirstOrDefaultAsync(c => c.StatusID == 6);
      }
      else
      {
        //adminde güncelle,ana sayfada detay, admin tarafındada detay
        product =await context.Products?.FirstOrDefaultAsync(c => c.ProductID == id);
      }
      return product;
    }


    public bool Add(Product product)
    {
      try
      {
        product.AddDate = DateTime.Now;
        product.Active = true;
        context.Products?.Add(product);
        context.SaveChanges();
        return true;
      }
      catch (Exception)
      {
        return false;
      }
    }


    public bool Update(Product product)
    {
      try
      {
        context.Products?.Update(product);
        context.SaveChanges();
        return true;
      }
      catch (Exception)
      {
        return false;
      }
    }


    public bool Delete(int id)
    {
      try
      {
        Product? product = context.Products?.FirstOrDefault(c => c.ProductID == id);
        product.Active = false;
        context.SaveChanges();
        return true;
      }
      catch (Exception)
      {
        return false;
      }
    }

    public  int? Count(int? id)
    {
      //ORM
      //ado.net
      //select  count(*) from Products where CategoryID=2

      //entityframeworkcore
      //linq
      //dapper
      int? count = context.Products?.Where(c => c.CategoryID == id).Count();
      return count;
    }

    public int? Count(string Value)
    {
      int? count = 0;
      if (Value == "New")
      {
        count = context.Products?.Count();
      }
      else if (Value == "Special")
      {
        count = context.Products?.Where(p => p.StatusID == 2).Count();
      }
      else
      {
        count = context.Products?.Count();
      }
      return count;
    }


    public PagedList<Product> Get()
    {
      PagedList<Product> model = new PagedList<Product>(context.Products.OrderByDescending(p => p.TopSeller), Page, subpagecount);

      return model;
    }

    public static void Highlighted_Increase(int id)
    {
      using (Iakademi47Context context = new Iakademi47Context())
      {
        Product? product = context.Products?.FirstOrDefault(p => p.ProductID == id);

        if (product != null)
        {
          product.HighLighted += 1; //product.HighLighted = product.HighLighted + 1
          context.Update(product);
          context.SaveChanges();
        }
      }
    }

   

  }
}
