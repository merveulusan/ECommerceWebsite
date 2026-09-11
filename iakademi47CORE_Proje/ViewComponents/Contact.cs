using iakademi47CORE_Proje.Models.MVVM;
using Microsoft.AspNetCore.Mvc;

namespace iakademi47CORE_Proje.ViewComponents
{
  public class Contact : ViewComponent
  {

    Iakademi47Context context = new Iakademi47Context();

    public IViewComponentResult Invoke()
    {
      Setting setting = context.Settings.FirstOrDefault(s => s.SettingID == 1);
      return View(setting);
    }

  }
}
