using System.Text.Json.Serialization;
using TCMA.BLL.Enums;

namespace TCMA.BLL.Models
{
    public class ComponentFilterModel
    {
        public string? Search { get; set; }

        public int Page { get; set; } = 1;

        public int PageSize { get; set; } = 20;

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public ComponentSortField SortBy { get; set; } = ComponentSortField.Id;

        public bool IsDescending { get; set; } = false;
    }
}
