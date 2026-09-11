using iakademi47CORE_Proje.Models.MVVM;
using Microsoft.AspNetCore.Mvc;

namespace iakademi47CORE_Proje.ViewComponents
{
  public class Address : ViewComponent
  {

    Iakademi47Context context = new Iakademi47Context();
    public string Invoke()
    {
      string address = context.Settings.FirstOrDefault(s => s.SettingID == 1).Address;
      return $"{address}";
    }

  }
}
