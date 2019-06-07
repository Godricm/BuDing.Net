using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace BuDing.Consul.Config
{
    public class KeyValueElement
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string Value { get; set; }
    }
}
