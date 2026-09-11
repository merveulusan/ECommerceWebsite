using iakademi47CORE_Proje.Models.MVVM;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;

namespace iakademi47CORE_Proje.Models.Concrete
{
  public class UserRepository
  {
    //ORM
    //entityframeworkcore 
    //bool answer = userRepository.LoginControl(user.Email);
    Iakademi47Context context = new Iakademi47Context();

    public bool LoginControl(string Email)
    {
      //select * from users where Email = 'sedat@hotmail.com'
      //FirstOrDefault = tek kayıt
      //=>   lampda expression
      User result = context.Users?.FirstOrDefault(u => u.Email == Email);
      if (result == null) return false;
      return true;
    }

    public bool Add(User user)
    {
      try
      {
        user.Active = true;
        context.Users?.Add(user);
        context.SaveChanges();
        return true;
      }
      catch (Exception)
      {
        return false;
      }
    }


    public string LoginControl(User user)
    {
      User? usr = context.Users?.FirstOrDefault(u => u.Email == user.Email && u.Password == user.Password);

      if (usr == null)
      {
        //login/şifre yanlıs
        return "error";
      }
      else
      {
        //login ve şifre doğru
        if (usr.IsAdmin)
        {
          return usr.NameSurname; //çalışan isim
        }
        else
        {
          return usr.Email; //alısveriş yapacak kullanıcı email
        }
      }
    }


    public static User Get(string Email)
    {
      using (Iakademi47Context context = new Iakademi47Context())
      {
        User? user = context.Users?.FirstOrDefault(c => c.Email == Email);
        return user;
      }
    }



  }
}
