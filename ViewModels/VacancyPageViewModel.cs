using hh.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace hh.ViewModels
{
    public class VacancyPageViewModel
    {
        [JsonPropertyName("vacancies")]
        public IEnumerable<Vacancy> Vacancies { get; set; }

        [JsonPropertyName("maxPage")]
        public int MaxPage { get; set; }

        [JsonPropertyName("curPage")]
        public int CurPage { get; set; }
    }
}
