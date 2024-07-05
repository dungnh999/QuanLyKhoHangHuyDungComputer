﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.DTO
{
    public class OptionProductDTO
    {
        public int ID { get; set; }
        public int STT { get; set; }
        public string NAME { get; set; }
        public string CODE { get; set; }
        public string STATUS { get; set; }
        public string VALUE { get; set; }
        public string PARENT { get; set; }
        public Nullable<System.DateTime> CREATE_AT { get; set; }
        public Nullable<System.DateTime> UPDATE_AT { get; set; }
        public string CREATE_BY { get; set; }
        public string UPDATE_BY { get; set; }
    }
}