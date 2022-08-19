using App.Library;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspCoreMvc_App.ViewComponents
{
    public class CategoryQuery:ViewComponent
    {
        public IViewComponentResult Invoke(GenreDetail category)
        {
            var allGames = GetRequest.GetAllGames();
            var categoryGames = allGames.Where(x => x.GenreID == category.GenreID).ToList();
            return View(categoryGames);
        }
    }
}
