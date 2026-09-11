using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace iakademi47CORE_Proje.Models.MVVM
{
  public class Supplier
  {
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [DisplayName("ID")]
    public int SupplierID { get; set; }

    [Required(ErrorMessage = "Marka Adı Zorunlu Alan")]
    [StringLength(40, ErrorMessage = "En fazla 40 Karakter")]
    [DisplayName("Marka Adı")]
    //regular expression
    //[RegularExpression(@"^[a-zA-Z''-'\s]{1,40}$",ErrorMessage = "Geçersiz Karakter kullandınız")]
    public string? BrandName { get; set; } = string.Empty;

    [DisplayName("Resim")]
    public string? PhotoPath { get; set; }

    [DisplayName("Aktif/Pasif")]
    public bool Active { get; set; }
  }
}
