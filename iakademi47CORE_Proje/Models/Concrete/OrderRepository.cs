using iakademi47CORE_Proje.Models.MVVM;

namespace iakademi47CORE_Proje.Models.Concrete
{
  public class OrderRepository
  {
    public int ProductID { get; set; }
    public int Quantity { get; set; }
    public string? MyCart { get; set; }
    public decimal UnitPrice { get; set; }
    public string? ProductName { get; set; }
    public string? PhotoPath { get; set; }
    public int Kdv { get; set; }

    Iakademi47Context context = new Iakademi47Context();

    //sepete ekle
    public bool AddToMyCart(string id)
    {
     // string isim = "sedat";
     // string[] isimler = { "sedat", "mustafa", "merve", "yiğit" };//dizi,array

      //bu metod bittiginde hala false ise ürün sepete eklendi
      bool exists = false;

      if (MyCart == "")
      {
        MyCart = id + "=" + Quantity; //10=1
      }
      else
      {
        //10=1&20=1&30=1&40=1
        string[] MyCartArray = MyCart.Split('&');
        //MyCart = 10=1&20=1&30=1&40=1
        //10=1  MyCartArray[0]
        //20=1  MyCartArray[1]
        //30=1  MyCartArray[2]
        //40=1  MyCartArray[3]
        for (int i = 0; i < MyCartArray.Length; i++)
        {
          string[] MyCartArrayLoop = MyCartArray[i].Split('=');
          //MyCartArrayLoop[0] = ProductID
          //MyCartArrayLoop[1] = Adet , Quantity
          if (MyCartArrayLoop[0] == id)
          {
            //bu ürün daha önceden sepete eklenmiş
            exists = true;
          }
        }

        //for bitti,eğer for icindeki if e girmediyse exists=false
        //for bitti,eğer for icindeki if e girdiyse exists=true
        if (exists == false) //ürün daha önce sepete eklenmemiş
        {
          MyCart = MyCart + "&" + id.ToString() + "=1";
        }
      }
      return exists;
    }


    public List<OrderRepository> SelectMyCart()
    {
      List<OrderRepository> list = new List<OrderRepository>();
      string[] MyCartArray = MyCart.Split('&');

      if (MyCart != "")  //sepette ürün varken for u yapsın
      {

        for (int i = 0; i < MyCartArray.Length; i++)
        {
          string[] MyCartArrayLoop = MyCartArray[i].Split('=');
          int ProductID = Convert.ToInt32(MyCartArrayLoop[0]);
          int Quantity = Convert.ToInt32(MyCartArrayLoop[1]);

         //sepetten gelen ProductID nin karsılığını databaseden bul
          Product? product = context.Products?.FirstOrDefault(p => p.ProductID == ProductID);

          //product icinde veritabanındaki verileri kayıtları var,bunları propertylere yazdırıyorum
          OrderRepository orderRepository = new OrderRepository();
          orderRepository.ProductID = product.ProductID;
          orderRepository.Quantity = Convert.ToInt32(Quantity);
          orderRepository.UnitPrice = Convert.ToDecimal(product.UnitPrice);
          orderRepository.ProductName = product.ProductName;
          orderRepository.PhotoPath = product.PhotoPath;
          orderRepository.Kdv = product.Kdv;
          list.Add(orderRepository);
        }
      }
      return list;
    }

    public void DeleteFromMyCart(string id)
    {
      string NewMyCart = "";
      int count = 1;
      string[] MyCartArray = MyCart.Split('&');

      for (int i = 0; i < MyCartArray.Length; i++)
      {
        string[] MyCartArrayLoop = MyCartArray[i].Split('=');
        string ProductID = MyCartArrayLoop[0];
        string Quantity = MyCartArrayLoop[1];

        if (ProductID != id) //silinmeyecek ürünler için if e gireceğim
        {
          if (count == 1)
          {
            NewMyCart = ProductID + "=" + Quantity;
            count++;
          }
          else
          {
            //NewMyCart = NewMyCart + "&" + ProductID + "=" + Quantity;
            NewMyCart += "&" + ProductID + "=" + Quantity;
          }
        }
      }
      MyCart = NewMyCart;
    }


    public string Add(string Email)
    {
      List<OrderRepository> List = SelectMyCart();
      DateTime OrderDate = DateTime.Now;
      string OrderGroupGUID = DateTime.Now.ToString().Replace(":", "").Replace(" ", "").Replace(".", "");
      foreach (var item in List)
      {
        Order order = new Order();
        order.OrderDate = OrderDate;
        order.OrderGroupGUID = OrderGroupGUID;
        order.UserID = context.Users.FirstOrDefault(u => u.Email == Email).UserID;
        order.ProductID = item.ProductID;
        order.Quantity = item.Quantity;
        context.Orders.Add(order);
        context.SaveChanges();
      }
      return OrderGroupGUID;
    }


    public List<Vw_MyOrder> GetMyOrders(string Email)
    {
      int UserID = context.Users.FirstOrDefault(u => u.Email == Email).UserID;

      List<Vw_MyOrder> myOrders = context.Vw_MyOrders.Where(o => o.UserID == UserID).ToList();

      return myOrders;
    }


  }
}
