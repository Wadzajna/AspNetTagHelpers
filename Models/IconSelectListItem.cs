using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Services.Models
{
    public class IconSelectListItem : SelectListItem
    {
        public int? Id { get; set; }

        public string? PreviewImageUrl { get; set; }

        public string? Icon { get; set; }

        public string? Class { get; set; }
    }
}
