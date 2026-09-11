using iakademi47CORE_Proje.Models.MVVM;
using Microsoft.EntityFrameworkCore;

namespace iakademi47CORE_Proje.Models.Concrete
{
	public class SearchRepository
	{
		public static List<Sp_Search> Get(string id)
		{
			using (Iakademi47Context context = new Iakademi47Context())
			{
				//stored procedure
				var products = context.Sp_Searches.FromSqlRaw($"SP_arama {id}").ToList();
				return products;
			}
		}
	}
}
