﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopsRUs.Domain.Model
{
   public class BaseModel
    {
        public int Id { get; set; }

        public DateTime CreateDate { get; set; } = DateTime.Now.Date;

    }
}
