﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.ErrorModelse
{
   public class ValidayionError
    {

        public string Field { get; set; } = null;
        public IEnumerable<string> Errors { get; set; } = [];

    }
}
