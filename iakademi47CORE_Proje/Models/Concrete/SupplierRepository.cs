using iakademi47CORE_Proje.Models.MVVM;

namespace iakademi47CORE_Proje.Models.Concrete
{
  public class SupplierRepository
  {

    Iakademi47Context context = new Iakademi47Context();
    public List<Supplier> Get()
    {
      List<Supplier>? suppliers = context.Suppliers?.ToList();
      return suppliers;
    }

    public bool Add(Supplier supplier)
    {
      try
      {
        context.Suppliers?.Add(supplier);
        context.SaveChanges();
        return true;
      }
      catch (Exception)
      {
        return false;
      }
    }


    public Supplier Get(int? id)
    {
      Supplier? supplier = context.Suppliers?.FirstOrDefault(c => c.SupplierID == id);
      return supplier;
    }


    public bool Update(Supplier supplier)
    {
      try
      {
        context.Suppliers?.Update(supplier);
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
        Supplier? supplier = context.Suppliers?.FirstOrDefault(c => c.SupplierID == id);
        supplier.Active = false;
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
