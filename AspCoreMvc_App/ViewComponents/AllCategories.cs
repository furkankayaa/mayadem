using App.Library;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspCoreMvc_App.ViewComponents
{
    public class AllCategories: ViewComponent
    {

        public IViewComponentResult Invoke(GenreDetail category)
        {
            var allGames = GetRequest.GetAllGames();
            return View(allGames);
        }
        
    }
}
