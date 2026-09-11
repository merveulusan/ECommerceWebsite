using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace iakademi47CORE_Proje.Models.MVVM
{
  public class Order
  {
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [DisplayName("ID")]
    public int OrderID { get; set; }
    public DateTime OrderDate { get; set; }
    [StringLength(30)]
    public string? OrderGroupGUID { get; set; }
    [DisplayName("KULLANICI ADI")]
    public int UserID { get; set; }
    [DisplayName("Ürün AdıÜRÜN ADI")]
    public int ProductID { get; set; }
    [DisplayName("ADET")]
    public int Quantity { get; set; }
  }
}
