using iakademi47CORE_Proje.Models.MVVM;
using Microsoft.EntityFrameworkCore;

namespace iakademi47CORE_Proje.Models.Concrete
{
  public class CategoryRepository
  {
    Iakademi47Context context = new Iakademi47Context();

    //Category Listesi
    public List<Category> Get(string Value)
    {
      List<Category> categories;
      if (Value == "main")
      {
         categories = context.Categories.Where(c => c.ParentID == 0).ToList();
      }
      else
      {
        categories = context.Categories.ToList();
      }
      return categories;
    }

    //Detay
    public Category Get(int? id)
    {
      Category? category = context.Categories?.FirstOrDefault(p => p.CategoryID == id);
      return category;
    }


    public bool Add(Category category)
    {
      try
      {
        category.Active = true;
        context.Categories?.Add(category);
        context.SaveChanges();
        return true;
      }
      catch (Exception)
      {
        //email(users.Email.userID==1)
        return false;
      }
    }

    public bool Update(Category category)
    {
      try
      {
        context.Categories?.Update(category);
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
        Category? category = context.Categories?.FirstOrDefault(c => c.CategoryID == id);
        category.Active = false;

        //eger silinen ana kategori ise , alt kategori varsa bakıyorum ve siliyorum
        List<Category> categoryList = context.Categories.Where(c => c.ParentID == id).ToList();
        foreach (var item in categoryList)
        {
          //categoryList boş değilse foreach içine girer ,alt kategorileride siler
          item.Active = false;
        }

        context.SaveChanges();
        return true;
      }
      catch (Exception)
      {
        return false;
      }
    }




  }
}
