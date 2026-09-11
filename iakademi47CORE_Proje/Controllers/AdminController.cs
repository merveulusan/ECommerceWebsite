using iakademi47CORE_Proje.Models.Concrete;
using iakademi47CORE_Proje.Models.MVVM;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace iakademi47CORE_Proje.Controllers
{
  public class AdminController : Controller
  {
    Iakademi47Context context = new Iakademi47Context();
    CategoryRepository categoryRepository = new CategoryRepository();
    SupplierRepository supplierRepository = new SupplierRepository();
    StatusRepository statusRepository = new StatusRepository();
    ProductRepository productRepository = new ProductRepository();
    SettingRepository settingRepository = new SettingRepository();

    [HttpGet]
    public IActionResult Login()
    {
      return View();
    }

    [HttpPost]
    public IActionResult Login(User user)
    {
      return RedirectToAction("Index");
    }

    [HttpGet]
    public IActionResult Index()
    {
      return View();
    }

    public IActionResult CategoryIndex()
    {
      List<Category> categories = categoryRepository.Get("all");
      return View(categories);
    }

    [HttpGet]
    public IActionResult CategoryCreate()
    {
      CategoryFill("main");
      return View();
    }

    

    [HttpPost]
    public IActionResult CategoryCreate([Bind("CategoryName,ParentID")] Category category)
    {
      if (ModelState.IsValid)
      {
        if (category.ParentID == null)
        {
          category.ParentID = 0;
        }
        bool answer = categoryRepository.Add(category);
        if (answer)
        {
          TempData["Message"] = "Başarıyla Kaydedildi";
        }
        else
        {
          TempData["Message"] = "Kayıt yapılamadı";
        }
      }
      return RedirectToAction("CategoryCreate");
    }


    [HttpGet]
    public IActionResult CategoryEdit(int? id)
    {
      CategoryFill("main");
      if (id == null || context.Categories == null)
      {
        return NotFound();
      }

      Category category = categoryRepository.Get(id);

      return View(category);
    }

    [HttpPost]
    public IActionResult CategoryEdit([Bind("CategoryName,ParentID,CategoryID,Active")] Category category)
    {
      if (ModelState.IsValid)
      {
        if (category.ParentID == null)
        {
          category.ParentID = 0;
        }
        bool answer = categoryRepository.Update(category);
        if (answer)
        {
          //logRepository.Add(Log26072026,Session[Email],category.CategoryName,datetime.Now,"Güncelledi");
          TempData["Message"] = "Başarıyla Güncellendi";
        }
        else
        {
          TempData["Message"] = "Güncelleme yapılamadı";
        }
      }
      return RedirectToAction(nameof(CategoryIndex)); // [HttpGet]
    }

    [HttpGet]
    public IActionResult CategoryDelete(int? id)
    {
      if (id == null || context.Categories == null)
      {
        return NotFound();
      }

      Category category = categoryRepository.Get(id);

      if (category == null)
      {
        return NotFound();
      }
      return View(category);
    }



    [HttpPost, ActionName("CategoryDelete")] //Routing
    public IActionResult CategoryDeleteConfirmed(int id)
    {
      bool answer = categoryRepository.Delete(id);
      if (answer == true)
      {
        TempData["Message"] = "Silindi";
        return RedirectToAction(nameof(CategoryIndex));
      }
      else
      {
        TempData["Message"] = "HATA";
        return RedirectToAction(nameof(CategoryDelete));
      }
    }

    public IActionResult SupplierIndex()
    {
      List<Supplier> suppliers = supplierRepository.Get();
      return View(suppliers);
    }


    [HttpGet]
    public IActionResult SupplierCreate()
    {
      return View();
    }


    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult SupplierCreate([Bind("BrandName,PhotoPath,Active")] Supplier supplier)
    {
      if (ModelState.IsValid)
      {
        bool answer = supplierRepository.Add(supplier);

        if (answer)
        {
          TempData["Message"] = supplier.BrandName + " Markası Başarıyla Kaydedildi";
        }
        else
        {
          TempData["Message"] = "Kayıt yapılamadı";
        }
      }
      return RedirectToAction(nameof(SupplierCreate));
    }


    [HttpGet]
    public IActionResult SupplierEdit(int? id)
    {
      if (id == null || context.Suppliers == null)
      {
        return NotFound();
      }
      Supplier supplier = supplierRepository.Get(id);
      return View(supplier);
    }


    [HttpPost]
    public IActionResult SupplierEdit([Bind("BrandName,PhotoPath,Active,SupplierID")] Supplier supplier)
    {
     // RegularExpression
      if (ModelState.IsValid)
      {
        if (supplier.PhotoPath == null)
        {
          //markanın eski bilgileri 
          Supplier? supplier1 = context.Suppliers?.FirstOrDefault(s => s.SupplierID == supplier.SupplierID);

          supplier.PhotoPath = supplier1.PhotoPath;
        }

        bool answer = supplierRepository.Update(supplier);
        if (answer)
        {
          TempData["Message"] = supplier.BrandName + " Markası Güncellendi";
          return RedirectToAction(nameof(SupplierIndex));
        }
        else
        {
          TempData["Message"] = "HATA";
        }
      }
      return RedirectToAction(nameof(SupplierEdit));
    }


    [HttpGet]
    public IActionResult SupplierDelete(int? id)
    {
      if (id == null || context.Suppliers == null)
      {
        return NotFound();
      }

      Supplier supplier = supplierRepository.Get(id);

      if (supplier == null)
      {
        return NotFound();
      }
      return View(supplier);
    }

    [HttpPost, ActionName("SupplierDelete")] //Routing
    public IActionResult SupplierDeleteConfirmed(int id)
    {
      bool answer = supplierRepository.Delete(id);
      if (answer == true)
      {
        TempData["Message"] = "Silindi";
        return RedirectToAction("SupplierIndex");
      }
      else
      {
        TempData["Message"] = "HATA";
        return RedirectToAction(nameof(SupplierDelete));
      }
    }

    [HttpGet]
    public IActionResult StatusIndex()
    {
      List<Status> statuses = statusRepository.Get();
      return View(statuses);
    }

    [HttpGet]
    public IActionResult StatusCreate()
    {
      return View();
    }

    [HttpPost]
    public IActionResult StatusCreate([Bind("StatusName,Active")] Status status)
    {
      bool answer = statusRepository.Add(status);
      if (answer == true)
      {
        TempData["Message"] = "Eklendi";
      }
      else
      {
        TempData["Message"] = "HATA";
      }
      return View();
    }


    [HttpGet]
    public IActionResult StatusEdit(int? id)
    {
      if (id == null || context.Statuses == null)
      {
        return NotFound();
      }
      Status statuses = statusRepository.Get(id);
      return View(statuses);
    }

    [HttpPost]
    public IActionResult StatusEdit(Status status)
    {
      bool answer = statusRepository.Update(status);
      if (answer == true)
      {
        TempData["Message"] = "Güncellendi";
        return RedirectToAction(nameof(StatusIndex));
      }
      else
      {
        TempData["Message"] = "HATA";
        return RedirectToAction("StatusEdit");
      }
    }

    [HttpGet]
    public IActionResult StatusDelete(int id)
    {
      bool answer = statusRepository.Delete(id);
      if (answer == true)
      {
        TempData["Message"] = "Silindi";
      }
      else
      {
        TempData["Message"] = "HATA";
      }
      return RedirectToAction(nameof(StatusIndex));
    }

    public IActionResult ProductIndex()
    {
      //Get metodunu , Anasayfada kategori ve Marka tıklanınca ürün getirirkende kullanıyoruz
      //adminController icindende ürün listesi getireceğiz

      //ikinci parametre onun icin
      List<Product> products = productRepository.Get(0, "");
      return View(products);
    }

    [HttpGet]
    public IActionResult ProductCreate()
    {
      CategoryFill("all"); //Kategori combobox(DropDownList) içini dolduracağız
      SupplierFill();  //Marka combobox(DropDownList) içini dolduracağız
      StatusFill(); //Statüs combobox(DropDownList) içini dolduracağız

      return View();
    }

    void CategoryFill(string Value)
    {
      List<Category> categories = categoryRepository.Get(Value);
      ViewData["categoryList"] = categories.Select(c => new SelectListItem { Text = c.CategoryName, Value = c.CategoryID.ToString() });
    }

    void SupplierFill()
    {
      List<Supplier> suppliers = supplierRepository.Get();
      ViewData["supplierList"] = suppliers.Select(c => new SelectListItem { Text = c.BrandName, Value = c.SupplierID.ToString() });
    }

    void StatusFill()
    {
      List<Status> statuses = statusRepository.Get();
      ViewData["statusList"] = statuses.Select(c => new SelectListItem { Text = c.StatusName, Value = c.StatusID.ToString() });
    }


    [HttpPost]
    public IActionResult ProductCreate(Product product)
    {
      if (ModelState.IsValid)
      {
        bool answer = productRepository.Add(product);
        if (answer)
        {
          TempData["Message"] = "Eklendi";
        }
        else
        {
          TempData["Message"] = "HATA";
        }
      }
      return RedirectToAction(nameof(ProductCreate));
    }

    [HttpGet]
    public async Task<IActionResult> ProductEdit(int? id)
    {
      CategoryFill("all");
      SupplierFill();
      StatusFill();

      if (id == null || context.Products == null)
      {
        return NotFound();
      }

      Product product = await productRepository.Get(id);

      return View(product);
    }


    [HttpPost]
    public IActionResult ProductEdit(Product product)
    {
      //veritabanından kaydını getirdim
      Product prd = context.Products.FirstOrDefault(s => s.ProductID == product.ProductID);
      //formdan gelmeyen , bazı kolonları null yerine , eski bilgilerini bastım
      product.AddDate = prd.AddDate;
      product.HighLighted = prd.HighLighted;
      product.TopSeller = prd.TopSeller;

      if (product.PhotoPath == null)
      {
        product.PhotoPath = prd.PhotoPath;
      }

      bool answer = productRepository.Update(product);
      if (answer == true)
      {
        TempData["Message"] = "Güncellendi";
        return RedirectToAction("ProductIndex");
      }
      else
      {
        TempData["Message"] = "HATA";
        return RedirectToAction(nameof(ProductEdit));
      }
    }


    [HttpGet]
    public async Task<IActionResult> ProductDelete(int? id)
    {
      if (id == null || context.Products == null)
      {
        return NotFound();
      }

      Product product = await productRepository.Get(id);

      if (product == null)
      {
        return NotFound();
      }
      return View(product);
    }


    [HttpPost, ActionName("ProductDelete")] //routing
    public IActionResult ProductDeleteConfirmed(int id)
    {
      bool answer = productRepository.Delete(id);
      if (answer == true)
      {
        TempData["Message"] = "Silindi";
        return RedirectToAction("ProductIndex");
      }
      else
      {
        TempData["Message"] = "HATA";
        return RedirectToAction(nameof(ProductDelete));
      }
    }


    [HttpGet]
    public async Task<IActionResult> ProductDetails(int? id)
    {
      Product product = await productRepository.Get(id);
      return View(product);
    }

    [HttpGet]
    public IActionResult SettingEdit()
    {
      Setting setting = settingRepository.Get();

      return View(setting);
    }

    [HttpPost]
    public IActionResult SettingEdit(Setting setting)
    {
      bool answer = SettingRepository.Update(setting); //static
      if (answer == true)
      {
        TempData["Message"] = "Güncellendi";
      }
      else
      {
        TempData["Message"] = "HATA";
      }
      return RedirectToAction(nameof(SettingEdit));
    }


  }
}
