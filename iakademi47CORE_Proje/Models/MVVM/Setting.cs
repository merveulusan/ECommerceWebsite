using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace iakademi47CORE_Proje.Models.MVVM
{
  public class Setting
  {
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int SettingID { get; set; }
    public string? Telephone { get; set; }

    [EmailAddress]
    public string? Email { get; set; }

    [DisplayName("Adres")]
    public string? Address { get; set; }

    [DisplayName("Ana Sayfada Görünecek Ürün Sayısı")]
    public int MainpageCount { get; set; }

    [DisplayName("Alt Sayfada Görünecek Ürün Sayısı")]
    public int SubpageCount { get; set; }
  }
}
