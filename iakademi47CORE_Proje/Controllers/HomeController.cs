using iakademi47CORE_Proje.Models.Concrete;
using iakademi47CORE_Proje.Models.MVVM;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using X.PagedList;

namespace iakademi47CORE_Proje.Controllers
{
  public class HomeController : Controller
  {
    //PM> Add-Migration "iakademi47Create"
    //PM> Update-Database
    Iakademi47Context context = new Iakademi47Context();

    UserRepository userRepository = new UserRepository();
    ProductRepository productRepository = new ProductRepository();
    CategoryRepository categoryRepository = new CategoryRepository();
    SupplierRepository supplierRepository = new SupplierRepository();
    OrderRepository orderRepository = new OrderRepository();
    MainPageModel mpm = new MainPageModel();

    //ctor + tab + tab = constructor = ilk çalışan metod
    public HomeController()
    {
      productRepository.mainpagecount = context.Settings.FirstOrDefault(s => s.SettingID == 1).MainpageCount;

      productRepository.subpagecount = context.Settings.FirstOrDefault(s => s.SettingID == 1).SubpageCount;
    }

    public async Task<IActionResult> Index()
    {
      //ORM
      //ado.net    select * from products
      //EntityFrameworkCore context.Products.Tolist(); , context.VW_ProductList.Tolist();
      //linq
      //dapper

      mpm.SliderProducts = await productRepository.Get("Slider", -1);
      mpm.NewProducts = await productRepository.Get("New", -1);
      mpm.SpecialProducts = await productRepository.Get("Special", -1);
      mpm.DiscountedProducts = await productRepository.Get("Discounted", -1);
      mpm.HighlightedProducts = await productRepository.Get("Highlighted", -1);
      mpm.TopsellerProducts = await productRepository.Get("Topseller", -1);
      mpm.StarProducts = await productRepository.Get("Star", -1);
      mpm.OpportunityProducts = await productRepository.Get("Opportunity", -1);
      mpm.NotableProducts = await productRepository.Get("Notable", -1);
      mpm.Productofday = await productRepository.Get(0);
      return View(mpm);
    }

    



    public IActionResult AboutUs()
    {
      return View();
    }

    public IActionResult Cart()
    {
      if (HttpContext.Request.Query["ProductID"].ToString() == "")
      {
        //menüde sağ üst köşeden geldik
        var cookie = Request.Cookies["sepetim"];
        if (cookie == null)
        {
          //sepet boş
          orderRepository.MyCart = "";
          ViewBag.Sepetim = orderRepository.SelectMyCart();
        }
        else
        {
          //sepet dolu
          orderRepository.MyCart = Request.Cookies["sepetim"];
          ViewBag.Sepetim = orderRepository.SelectMyCart();
        }
      }
      else   //sepet sayfasında ürün siliyorum,silme butonu ile geliyorum
      {
        string? ProductID = HttpContext.Request.Query["ProductID"];
        orderRepository.MyCart = Request.Cookies["sepetim"];
        orderRepository.DeleteFromMyCart(ProductID);
        var cookieOptions = new CookieOptions();
        Response.Cookies.Append("sepetim", orderRepository.MyCart, cookieOptions);
        cookieOptions.Expires = DateTime.Now.AddDays(1);
        TempData["Message"] = "Ürün Sepetten Silindi";
        ViewBag.Sepetim = orderRepository.SelectMyCart();
      }


      return View();
    }

    [HttpGet]
    public IActionResult Register()
    {
      return View();
    }

    [HttpPost]
    public IActionResult Register([Bind("NameSurname,Email,Password,Telephone,InvoicesAddress")] User user)
    {
      if (ModelState.IsValid)
      {
        bool answer = userRepository.LoginControl(user.Email);
        if (answer == false)
        {
          bool answer2 = userRepository.Add(user);
          if (answer2)
          {
            TempData["Message"] = "Başarıyla Kaydedildi";
            return RedirectToAction("Index");
          }
          else
          {
            TempData["Message"] = "Kayıt yapılamadı";
            return RedirectToAction("Register"); //    [HttpGet]
          }
        }
        else
        {
          TempData["Message"] = "Email zaten mevcut";
          return RedirectToAction("Register");
        }
      }
      return View();
    }

    [HttpGet]
    public IActionResult Login()
    {
      return View();
    }

    [HttpPost]
    public IActionResult Login([Bind("Email,Password,NameSurname,Telephone")] User user)
    {
      if (ModelState.IsValid)
      {
        string answer = userRepository.LoginControl(user);
        if (answer == "error")
        {
          TempData["Message"] = "Email/Şifre yanlış";
        }
        else if(answer.Contains("@"))
        {
          HttpContext.Session.SetString("Email", answer);
          return RedirectToAction("Index", "Home");
        }
        else
        {
          return RedirectToAction("Index", "Admin");
        }
      }
      return View();
    }


    public  IActionResult CategoryPage(int id)
    {
      List<Product> products = productRepository.Get(id, "Category");
      Category category =  categoryRepository.Get(id);
      ViewBag.Header = category.CategoryName;
      ViewBag.Count = productRepository.Count(id);
      return View(products);
    }

    public IActionResult SupplierPage(int id)
    {
      List<Product> products = productRepository.Get(id, "Supplier");
      Supplier supplier = supplierRepository.Get(id);
      ViewBag.Header = supplier.BrandName;
      return View(products);
    }

    
    public async Task<IActionResult> NewProducts()
    {
      ViewBag.Count = productRepository.Count("New");

      //mpm.NewProducts = await productRepository.Get("New", -1);
      List<Product>? NewProducts = await productRepository.Get("New", 0);
      return View(NewProducts);
    }


    //alt sayfa AJAX yaparken yeni ürünler
    public async Task<PartialViewResult> _PartialNewProducts(int pageno)
    {
      List<Product>? NewProducts =await productRepository.Get("New", pageno);
      return PartialView(NewProducts);
    }


    public IActionResult JQueryPage()
    {
      return View();
    }

    public async Task<IActionResult> SpecialProducts()
    {
      ViewBag.Count = productRepository.Count("Special");
      List<Product>? SpecialProducts = await productRepository.Get("Special", 0);
      return View(SpecialProducts);
    }

    //alt sayfa AJAX yaparken özel ürünler
    public async Task<PartialViewResult> _PartialSpecialProducts(int pageno)
    {
      List<Product>? SpecialProducts = await productRepository.Get("Special", pageno);
      return PartialView(SpecialProducts);
    }

    
    public async Task<IActionResult> DiscountedProducts()
    {
      ViewBag.Count = productRepository.Count("Discounted");
      List<Product>? DiscountedProducts = await productRepository.Get("Discounted", 0);
      return View(DiscountedProducts);
    }

    //alt sayfa AJAX yaparken indirimli ürünler
    public async Task<PartialViewResult> _PartialDiscountedProducts(int pageno)
    {
      List<Product>? DiscountedProducts = await productRepository.Get("Discounted", pageno);
      return PartialView(DiscountedProducts);
    }


    public async Task<IActionResult> HighlightedProducts()
    {
      ViewBag.Count = productRepository.Count("Highlighted");
      List<Product>? HighlightedProducts = await productRepository.Get("Highlighted", 0);
      return View(HighlightedProducts);
    }

    public async Task<PartialViewResult> _PartialHighlightedProducts(int pageno)
    {
      List<Product>? HighlightedProducts = await productRepository.Get("Highlighted", pageno);
      return PartialView(HighlightedProducts);
    }


    public IActionResult TopsellerProducts(int page = 1)
    {
      //manage nuget pacgages install
      // X.PagedList  (10.5.9)
      // X.PagedList.Mvc.Core  (10.5.9)
      productRepository.Page = page;
      PagedList<Product> model = productRepository.Get();
      ViewBag.Count = productRepository.Count("Topseller");

      return View("TopsellerProducts", model);
    }

    public IActionResult Details(int id)
    {
      // HighLighted kolonunun değerini arttırdım(öne cıkanlar)
      ProductRepository.Highlighted_Increase(id);

      //entityframeworkcore
      //mpm.ProductDetails = context.Products?.FirstOrDefault(p => p.ProductID == id);

      //linq
      mpm.ProductDetails = (from p in context.Products where p.ProductID == id select p).FirstOrDefault();

      //linq
      mpm.CategoryName = (from p in context.Products
                          join c in context.Categories
                        on p.CategoryID equals c.CategoryID
                          where p.ProductID == id
                          select c.CategoryName).FirstOrDefault();

      //linq
      mpm.BrandName = (from p in context.Products
                       join s in context.Suppliers
                     on p.SupplierID equals s.SupplierID
                       where p.ProductID == id
                       select s.BrandName).FirstOrDefault();

      mpm.RelatedProducts = context.Products?.Where(p => p.Related == mpm.ProductDetails!.Related && p.ProductID != id).OrderBy(c => c.ProductName).ToList();

      return View(mpm);
    }

    public IActionResult ContactUs()
    {
      return View();
    }


    public IActionResult CartProcess(int id)
    {
      string refererUrl = Request.Headers["Referer"].ToString();
      string url = "";

     

      if (id > 0)
      {
        orderRepository.ProductID = id;
        orderRepository.Quantity = 1;

        var cookieOptions = new CookieOptions();
        var cookie = Request.Cookies["sepetim"];

        if (cookie == null)
        {
          //sepet boş
          cookieOptions.Expires = DateTime.Now.AddDays(1);
          cookieOptions.Path = "/";
          orderRepository.MyCart = "";
          orderRepository.AddToMyCart(id.ToString());
          Response.Cookies.Append("sepetim", orderRepository.MyCart, cookieOptions);
          TempData["Message"] = "Ürün sepetinize eklendi";
        }
        else
        {
          //sepet doluysa
          // tarayıcıdaki sepetim içerisindeki daha önceki ürünleri property'e gönderdim.
          orderRepository.MyCart = cookie;
          //sepet dolu aynı ürün varmı
          if (orderRepository.AddToMyCart(id.ToString()) == false)
          {
            HttpContext.Response.Cookies.Append("sepetim", orderRepository.MyCart, cookieOptions);
            cookieOptions.Expires = DateTime.Now.AddDays(1);
            TempData["Message"] = "Ürün sepetinize eklendi";
          }
          else
          {
            TempData["Message"] = "Bu ürün zeten sepetinizde var";
          }
        }
      }

        Uri refererUri = new Uri(refererUrl, UriKind.Absolute);
        url = refererUri.AbsolutePath; 
        return Redirect(url);
    }


    [HttpGet]
    public IActionResult Order()
    {
      if (HttpContext.Session.GetString("Email") != null)
      {
        //kullanıcı Login.cshtml den giriş yapıp , Session alıp gelmiştir,Modelle kullanıcının bilgilerini gösterecegim
        User? user = UserRepository.Get(HttpContext.Session.GetString("Email"));

        return View(user);
      }
      else
      {
        //kullanıcı Login.cshtml ye gitmemiş , Session alıp gelmemiş
        return RedirectToAction("Login");
      }
    }

    [HttpPost]
    public IActionResult Order(IFormCollection frm)
    {
      // 1. yol string kredikartno = Request.Form["kredikartno"];
      //string kredikartay = frm["kredikartno"]; // 2. yol

      string txt_individual = Request.Form["txt_individual"]; //bireysel
      string txt_corporate = Request.Form["txt_corporate"]; //kurumsal

      if (txt_individual != null)
      {
        //bireysel fatura xml dosyası
        //digital planet
      }
      else
      {
        //kurumsal fatura xml dosyası
      }
      string kredikartno = Request.Form["kredikartno"];
      string kredikartay = frm["kredikartay"];
      string kredikartyil = frm["kredikartyil"];
      string kredikartcvs = frm["kredikartcvs"];
      //payu-iyzico
      return RedirectToAction("backref");
    }


    public static string OrderGroupGUID = "";

    public IActionResult backref()
    {
      //bankadan gelen sonuc ok ise
      //sipariş tablosuna kaydet
      //sepetim cookie sinden sepeti temizleyecegiz
      //e-fatura olustur metodunu cagır
      var cookieOptions = new CookieOptions();
      var cookie = Request.Cookies["sepetim"];
      if (cookie != null)
      {
        orderRepository.MyCart = cookie;
        OrderGroupGUID = orderRepository.Add(HttpContext.Session.GetString("Email").ToString());

        cookieOptions.Expires = DateTime.Now.AddDays(1);
        Response.Cookies.Delete("sepetim");
        //tarayıcıdan sepeti sil
        //sms gönder  netgsm
        //Email gönder şirket server ından gönderilir(mail server)
      }
      return RedirectToAction("ConfirmPage");
    }

    public IActionResult ConfirmPage()
    {
      ViewBag.OrderGroupGUID = OrderGroupGUID;
      return View();
    }

    public IActionResult Logout()
    {
      HttpContext.Session.Remove("Email");
      return RedirectToAction("Index");
    }

    public IActionResult MyOrders()
    {
      //daha önceden giriş yapmış
      if (HttpContext.Session.GetString("Email") != null)
      {
        List<Vw_MyOrder> orders = orderRepository.GetMyOrders(HttpContext.Session.GetString("Email").ToString());
        return View(orders);
      }
      else
      { //yapmamış
        return RedirectToAction("Login");
      }
    }

    public PartialViewResult gettingSearch(string id)
        {
            id=id.ToUpper(new System.Globalization.CultureInfo("tr-TR"));
            List<Sp_Search> ulist = SearchRepository.Get(id);
            string json = JsonConvert.SerializeObject(ulist);
            var response =JsonConvert.DeserializeObject<List<Search>>(json);
            return PartialView(response);
        }



  }
}
