using hh.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace hh.ViewModels
{
    public class CategoryAndCategories
    {
        public List<Category> Categories { get; set; }
        [JsonPropertyName("name")]
        public Category Category { get; set; }
    }
}
