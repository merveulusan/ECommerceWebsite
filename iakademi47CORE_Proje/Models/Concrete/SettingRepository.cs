using iakademi47CORE_Proje.Models.MVVM;
using Microsoft.EntityFrameworkCore;

namespace iakademi47CORE_Proje.Models.Concrete
{
  public class SettingRepository
  {

    Iakademi47Context context = new Iakademi47Context();

    public Setting? Get()
    {
      Setting? settings = context.Settings?.FirstOrDefault(s => s.SettingID == 1);
      return settings;
    }

    public static bool Update(Setting setting)
    {
      try
      {
        //metod static olduğu için,new ile nesne oluşturma işini,metod içinde yapıyoruz
        using (Iakademi47Context context = new Iakademi47Context())
        {
          setting.SettingID = 1;
          context.Update(setting);
          context.SaveChanges();
          return true;
        }
      }
      catch (Exception)
      {
        return false;
      }
    }




  }
}
