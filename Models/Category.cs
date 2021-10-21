using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace hh.Models
{
    public class Category
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("name")]
        [Display(Name = "Название")]
        public string Name { get; set; }

        public List<Resume> Resumes { get; set; }

        public Category()
        {
            Resumes = new List<Resume>();
        }
    }
}
