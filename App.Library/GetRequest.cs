using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace App.Library
{
    public static class GetRequest
    {
        //Returns response string to get request
        public static string GetApi(string ApiUrl)
        {

            var responseString = "";
            var request = (HttpWebRequest)WebRequest.Create(ApiUrl);
            request.Method = "GET";
            request.ContentType = "application/json";

            using (var response1 = request.GetResponse())
            {
                using (var reader = new StreamReader(response1.GetResponseStream()))
                {
                    responseString = reader.ReadToEnd();
                }
            }
            return responseString;

        }

        //Returns GameDetailResponse with category name
        public static GameDetailResponse GetGameDetailResponse(GameDetail game)
        {

            var genres = GetCategories();
            var catName = genres.Where(x => x.GenreID == game.GenreID).FirstOrDefault().CategoryName;

            GameDetailResponse myResponse = new GameDetailResponse
            {
                ID = game.ID,
                Description = game.Description,
                GameName = game.GameName,
                GamePrice = game.GamePrice,
                Publisher = game.Publisher,
                ImageUrl = game.ImageUrl,
                GenreID = game.GenreID,
                CategoryName = catName
            };

            return myResponse;
        }

        //Gets List of all GenreDetails
        public static List<GenreDetail> GetCategories()
        {
            List<GenreDetail> genres = new List<GenreDetail> { };

            //While working on Docker container
            var categories = GetApi($"http://genres.api/api/Genres/getall");

            //While working on local
            //var categories = GetApi($"http://localhost:5002/api/Genres/getall");

            JArray jArray = JArray.Parse(categories);
            foreach (JObject jObject in jArray)
            {
                genres.Add(new GenreDetail { GenreID = (int)jObject["genreID"], CategoryName = (string)jObject["categoryName"] });
            }

            return genres;
        }

        //Gets List of all GameDetails
        public static List<GameDetailResponse> GetAllGames()
        {
            List<GameDetailResponse> games = new List<GameDetailResponse> { };

            //While working on Docker container
            var gameResponse = GetApi($"http://games.api/api/Games/getall");

            //While working on local
            //var gameResponse = GetApi($"http://localhost:5000/api/Games/getall");
            JObject responseObject = JObject.Parse(gameResponse);

            JArray jArray = (JArray)responseObject["response"];
            foreach (JObject jObject in jArray)
            {
                games.Add(new GameDetailResponse { ID= (int)jObject["id"], GameName= (string)jObject["gameName"], Description= (string)jObject["description"], GamePrice= (double)jObject["gamePrice"], Publisher= (string)jObject["publisher"] , ImageUrl= (string)jObject["imageUrl"], GenreID = (int)jObject["genreID"], CategoryName = (string)jObject["categoryName"] });
            }

            return games;
        }

        public static GameDetailResponse GetGameById(int id)
        {
            List<GameDetailResponse> games = new List<GameDetailResponse> { };

            //While working on Docker container
            var gameResponse = GetApi($"http://games.api/api/Games/getbyid/{id}");

            //While working on local
            //var gameResponse = GetApi($"http://localhost:5000/api/Games/getbyid/{id}");
            JObject responseObject = JObject.Parse(gameResponse);

            JObject jObject = (JObject)responseObject["response"];
            
            
            var toReturn = new GameDetailResponse { ID = (int)jObject["id"], GameName = (string)jObject["gameName"], Description = (string)jObject["description"], 
                GamePrice = (double)jObject["gamePrice"], Publisher = (string)jObject["publisher"], ImageUrl = (string)jObject["imageUrl"], GenreID = (int)jObject["genreID"], CategoryName = (string)jObject["categoryName"] };    

            return toReturn;
        }

        public static List<CartItemDetail> GetCartItems(string userName)
        {
            List<CartItemDetail> cartItems = new List<CartItemDetail>();

            //While working on Docker container
            var cartResponse = GetApi($"http://cart.api/api/cart/getall?userName={userName}");

            //While working on local
            //var cartResponse = GetApi($"http://localhost:5004/api/cart/getall?userName={userName}");
            JObject responseObject = JObject.Parse(cartResponse);

            JArray jArray = (JArray)responseObject["response"];
            foreach (JObject jObject in jArray)
            {
                cartItems.Add(new CartItemDetail { ID = (int)jObject["id"], GameName = (string)jObject["gameName"], GamePrice = (double)jObject["gamePrice"], Publisher = (string)jObject["publisher"], ImageUrl = (string)jObject["imageUrl"], UserName= userName });
            }

            return cartItems;

        }
    }
}
