﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Library
{
    public class GameOrderLink
    {
        [Key]
        public int LinkId { get; set; }
        public int GameId { get; set; }
        public OrderedGamesDetail Game { get; set; }
        public int OrderId { get; set; }
        public OrderDetail Order { get; set; }
    }
}
