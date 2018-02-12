using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace JsonApi.Models
{
    public class JsonItem
    {
        public long Id { get; set; }
        public string Data { get; set; }
    }
}
