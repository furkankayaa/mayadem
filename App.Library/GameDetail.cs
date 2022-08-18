using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace App.Library
{
    public class GameDetail
    {
        public int ID { get; set; }
        public string GameName { get; set; }
        public double GamePrice { get; set; }
        public string Publisher { get; set; }
        public string Description { get; set; }
        [Required]
        public int GenreID { get; set; }

    }
}
