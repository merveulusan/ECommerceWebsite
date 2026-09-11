using iakademi47CORE_Proje.Models.MVVM;
using Microsoft.AspNetCore.Mvc;

namespace iakademi47CORE_Proje.ViewComponents
{
  public class Footers : ViewComponent
  {
    Iakademi47Context context = new Iakademi47Context();

    public IViewComponentResult Invoke()
    {
      List<Supplier>? suppliers = context.Suppliers?.Where(s => s.Active == true).ToList();
      return View(suppliers);
    }
  }
}
