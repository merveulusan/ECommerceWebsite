using iakademi47CORE_Proje.Models.MVVM;
using Microsoft.AspNetCore.Mvc;

namespace iakademi47CORE_Proje.ViewComponents
{
  public class Headers : ViewComponent
  {
    Iakademi47Context context = new Iakademi47Context();

    public IViewComponentResult Invoke()
    {
      List<Category> categories = context.Categories.Where(c => c.Active == true).ToList();
      return View(categories);
    }
  }
}
