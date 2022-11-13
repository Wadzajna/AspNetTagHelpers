using Microsoft.AspNetCore.Mvc.Rendering;
using Services.Models;

namespace MyCoreWeb2.Models
{
    public class HomeModelView
    {
        public ProductView Product { get; set; } = new ProductView();

        public List<SelectListItem> ProductFrameColorsSelectList { get; set; } = new List<SelectListItem>();

        public List<SelectListItem> ProductSizeSelectList { get; set; } = new List<SelectListItem>();


        public List<IconSelectListItem> ProductFrameColorsIconSelectList { get; set; } = new List<IconSelectListItem>();
        public List<IconSelectListItem> ProductSizeIconSelectList { get; set; } = new List<IconSelectListItem>();
        public List<IconSelectListItem> ProductionStatusesIconSelectList { get; set; } = new List<IconSelectListItem>();
    }
}
