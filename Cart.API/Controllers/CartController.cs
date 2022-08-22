using App.Library;
using Cart.API.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cart.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CartController : ControllerBase
    {
        private readonly CartItemsContext _context;
        public CartController(CartItemsContext context)
        {
            _context = context;
        }

        [Route("[action]")]
        public GenericResponse<List<CartItemDetail>> GetAll()
        {

            var cartItems = _context.CartItemDetails.ToList();

            GenericResponse<List<CartItemDetail>> toReturn = new GenericResponse<List<CartItemDetail>>() { Response = cartItems, Code = ResponseCode.OK };

            return toReturn;
        }

        [Route("[action]")]
        [HttpPost]
        public GenericResponse<CartItemDetail> Post([FromBody] GameDetailResponse game)
        {
            //var uname = User.FindFirst("UserName").Value;
            //Console.WriteLine("***********CHECK**************");
            //Console.WriteLine(uname);
            var uname = "a";
            //GameDetailResponse game = genericGame.Response;
            CartItemDetail addItem = new CartItemDetail { ID=game.ID ,GameName = game.GameName, GamePrice = game.GamePrice, ImageUrl = game.ImageUrl, Publisher = game.Publisher, UserName = uname };

            _context.CartItemDetails.Add(addItem);
            _context.SaveChanges();
            GenericResponse<CartItemDetail> toReturn = new GenericResponse<CartItemDetail> { Response = addItem, Code = ResponseCode.OK };
            return toReturn;
        }
    }
}
