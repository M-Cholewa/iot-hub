﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageProcessing.Infrastructure
{
    public class BaseTelemetry
    {
        public string FieldName { get; set; } = "";
        public object Value { get; set; } = "";
        public string Unit { get; set; } = "";
    }
}
