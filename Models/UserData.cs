﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiabeticAide.Models
{
    public class UserData
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int Reading { get; set; }
        public string Note { get; set; }
        public DateTime DateTime { get; set; }
    }
}
