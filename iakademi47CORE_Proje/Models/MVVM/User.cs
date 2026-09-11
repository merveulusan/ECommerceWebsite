using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace iakademi47CORE_Proje.Models.MVVM
{
  public class User
  {
    //prop + tab + tab = properties
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int UserID { get; set; }

    [StringLength(100, ErrorMessage = "En fazla 100 Karakter")]
    [Required(ErrorMessage = "Kullanıcı Adı Zorunlu Alan")]
    [DisplayName("KULLANICI ADI")]
    public string? NameSurname { get; set; }

    [StringLength(100, ErrorMessage = "En fazla 100 Karakter")]
    [Required(ErrorMessage = "Email Zorunlu Alan")]
    [EmailAddress(ErrorMessage = "Doğru Email Adresi Girmediniz")]
    [DisplayName("EMAİL")]
    public string? Email { get; set; }

    [StringLength(100, ErrorMessage = "En fazla 100 Karakter")]
    [Required(ErrorMessage = "Şifre Zorunlu Alan")]
    [DataType(DataType.Password)]
    [DisplayName("ŞİFRE")]
    public string? Password { get; set; }

    [DisplayName("TELEFON")]
    [Required(ErrorMessage = "Telefon Zorunlu Alan")]
    public string? Telephone { get; set; }

    [DisplayName("Fatura Adresi")]
    public string? InvoicesAddress { get; set; } //fatura adresi

    public bool IsAdmin { get; set; } //normal kullanıcımı? calısan personelmi?

    [DisplayName("Aktif/Pasif")]
    public bool Active { get; set; }
  }
}
