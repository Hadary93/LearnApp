﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataProcessing.Lib
{
    public class Cue
    {
        public int Id { get; set; }
        public string? Text { get; set; } = string.Empty;
        public string? start { get; set; }
        public string? end { get; set; }
    }
}
