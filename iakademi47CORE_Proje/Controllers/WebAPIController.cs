using iakademi47CORE_Proje.Models.MVVM;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;
using Newtonsoft.Json;  // indirmk gereiyor
using System.Net;
using System.Xml;
using System.Xml.Linq;

namespace iakademi47CORE_Proje.Controllers
{
    public class WebAPIController : Controller
    {

        public async Task<IActionResult> PharmacyOnDuty()
        {
            string json = new System.Net.WebClient().DownloadString("https://openapi.izmir.bel.tr/api/ibb/nobetcieczaneler");

            var pharmacy = JsonConvert.DeserializeObject<List<Pharmacy>>(json);

            return View(pharmacy);

            /*
             * using HttpClient client = new HttpClient();
             * string url = "https://openapi.izmir.bel.tr/api/ibb/nobetcieczaneler";
             * 
             * string json = await client.GetStringAsync(url);                                *-----CALISAN HALİ-----
             * var pharmacy = JsonConvert.DeserializeObject<List<Pharmacy>>(json);
             * return View(pharmacy);
             */
        }

        public IActionResult ArtAndCulture()
        {
            string json = new WebClient().DownloadString("https://openapi.izmir.bel.tr/api/ibb/kultursanat/etkinlikler");
            
            var activite = JsonConvert.DeserializeObject<List<Activite>>(json);

            return View(activite);
        }

        public IActionResult ExchangeRate()
        {
            string url = "http://www.tcmb.gov.tr/kurlar/today.xml";

            var xmlDoc = new XmlDocument();
            xmlDoc.Load(url);

            string dolaralis = xmlDoc.SelectSingleNode("Tarih_Date/Currency[@Kod='USD']/BanknoteBuying").InnerXml;
            ViewBag.dolaralis = dolaralis.Substring(0, 5);

            string dolarsatis = xmlDoc.SelectSingleNode("Tarih_Date/Currency[@Kod='USD']/BanknoteSelling").InnerXml;
            ViewBag.dolarsatis = dolarsatis.Substring(0, 5);

            string euroalis = xmlDoc.SelectSingleNode("Tarih_Date/Currency[@Kod='USD']/BanknoteBuying").InnerXml;
            ViewBag.euroalis = euroalis.Substring(0, 5);

            string eurosatis = xmlDoc.SelectSingleNode("Tarih_Date/Currency[@Kod='USD']/BanknoteSelling").InnerXml;
            ViewBag.eurosatis = eurosatis.Substring(0, 5);

            return View();
        }

        public IActionResult WeatherForecast()
        {
            string apikey = "ÜYELİK APİ KEY URL SONU";
            string city = "izmir";
            string url = "https://api.openweathermap.org/data/2.5/weather?q=" + city + "&mode=xml&lang=tr&units=metric&appid" + apikey;

            XDocument weather = XDocument.Load(url);

            ViewBag.temperature = weather.Descendants("temperature").ElementAt(0).Attribute("Value").Value;

            ViewBag.City = city.ToUpper();

            string icon = weather.Descendants("weather").ElementAt(0).Attribute("icon").Value;
            ViewBag.iconurl = "https://openweathermap.org/img/wn/" + icon + ".png";
            
            
            return View();
        }


    }
}
