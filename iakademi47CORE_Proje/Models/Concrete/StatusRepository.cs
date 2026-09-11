using iakademi47CORE_Proje.Models.MVVM;

namespace iakademi47CORE_Proje.Models.Concrete
{
  public class StatusRepository
  {

    Iakademi47Context context = new Iakademi47Context();
    public List<Status> Get()
    {
      List<Status>? statuses = context.Statuses?.ToList();
      return statuses;
    }

    public bool Add(Status status)
    {
      try
      {
        context.Statuses?.Add(status);
        context.SaveChanges();
        return true;
      }
      catch (Exception)
      {
        return false;
      }
    }

    public Status Get(int? id)
    {
      return context.Statuses?.FirstOrDefault(c => c.StatusID == id);
    }

    public bool Update(Status status)
    {
      try
      {
        context.Statuses?.Update(status);
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
        Status? status = context.Statuses?.FirstOrDefault(c => c.StatusID == id);
        status.Active = false;
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
