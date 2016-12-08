using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using xNet;

namespace VkAnalyzePolls
{
    internal class VkApi
    {
        private const string VkApiUrl = "https://api.vk.com/method/"; //Ссылка для запросов
        private readonly string _token; //Токен, использующийся при запросах

        public VkApi(string accessToken)
        {
            _token = accessToken;
        }

        public Dictionary<string, string> GetInformation(string userId, string[] fields)
            //Получение заданной информации о пользователе с заданным ID 
        {
            HttpRequest httpRequest = new HttpRequest();
            httpRequest.AddUrlParam("user_ids", userId);
            httpRequest.AddUrlParam("access_token", _token);

            string paramsText = "";
            foreach (string i in fields)
            {
                paramsText += i + ",";
            }
            paramsText = paramsText.Remove(paramsText.Length - 1);

            httpRequest.AddUrlParam("fields", paramsText);

            string result = httpRequest.Get(VkApiUrl + "users.get").ToString();
            result = result.Substring(13, result.Length - 15); //WTF?
            Dictionary<string, string> userInfo = JsonConvert.DeserializeObject<Dictionary<string, string>>(result);
            return userInfo;
        }

        public string GetCityById(string cityId) //Перевод ID города в название
        {
            HttpRequest httpRequest = new HttpRequest();
            httpRequest.AddUrlParam("city_ids", cityId);
            httpRequest.AddUrlParam("access_token", _token);
            string result = httpRequest.Get(VkApiUrl + "database.getCitiesById").ToString();
            result = result.Substring(13, result.Length - 15);
            Dictionary<string, string> userCity = JsonConvert.DeserializeObject<Dictionary<string, string>>(result);
            return userCity["name"];
        }

        public string GetCountryById(string countryId) //Перевод ID страны в название
        {
            HttpRequest httpRequest = new HttpRequest();
            httpRequest.AddUrlParam("country_ids", countryId);
            httpRequest.AddUrlParam("access_token", _token);
            string result = httpRequest.Get(VkApiUrl + "database.getCountriesById").ToString();
            result = result.Substring(13, result.Length - 15);
            Dictionary<string, string> userCountry = JsonConvert.DeserializeObject<Dictionary<string, string>>(result);
            return userCountry["name"];
        }
    }
}