using System.ComponentModel.DataAnnotations;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Services.Models;

namespace MyCoreWeb2.Models
{
    public class OrderView
    {
        public OrderView()
        {
            AddProduct = new ProductView()
            {
                ProductSize_Id = 1,
                ProductLayout_Id = 1,
                ProductFrameColor_Id = 1
            };
        }


        public List<ProductView> Products { get; set; } = new();

        public ProductView AddProduct { get; set; }

        public string? OrderKey { get; set; }

        [Display(Name = "Počet přidaných produktů")]
        [Range(0, 100)]
        public int ProductCount => Products.Count;


        [Required, MaxLength(100)]
        [Display(Name = "Jméno", Description = "Zadejte křestní jméno")]
        public string? Name { get; set; }

        [Required, MaxLength(100)]
        [Display(Name = "Příjmení", Description = "Zadejte příjmení")]
        public string? Surname { get; set; }

        [MaxLength(100)]
        [Display(Name = "Příjmení za svobodna", Description = "Zadejte příjmení za svobodna")]
        public string? MaidenName { get; set; }

        [EmailAddress]
        [Required, MaxLength(100)]
        [Display(Name = "Email", Description = "Zadejte emailovou adresu")]
        public string? EmailContact { get; set; }

        [Phone]
        [Required, MaxLength(50)]
        [Display(Name = "Tel.", Description = "Zadejte telefonní číslo")]
        public string? PhoneContact { get; set; }

        [MaxLength(50)]
        [Display(Name = "Facebook", Description = "Zadejte název vašeho profilu na facebooku")]
        public string? FacebookContact { get; set; }

        [MaxLength(50)]
        [Display(Name = "Instagram", Description = "Zadejte název vašeho profilu na instagramu")]
        public string? InstagramContact { get; set; }

        [Required]
        [Display(Name = "Máte od nás poukaz ?", Description = "Zadejte ano, pokud jste od nás obdrželi poukaz")]
        public bool? HaveVoucher { get; set; } = true;

        [Display(Name = "Číslo poukazu")]
        public string? VoucherNumber { get; set; }

        [Required]
        [Display(Name = "Datum svatby", Description = "Zadejte datum vaší svatby")]
        public DateTime? WeddingDate { get; set; }

        
        [Display(Name = "Datum odeslání", Description = "Zadejte datum vaší svatby")]
        public DateTime? DateOfShipment { get; set; }




        public List<IconSelectListItem>? ProductFrameColorsIconSelectList { get; set; }
        public List<IconSelectListItem>? ProductSizeIconSelectList { get; set; }
        public List<IconSelectListItem>? ProductLayoutIconSelectList { get; set; }
        public List<IconSelectListItem>? ProductionStatusesIconSelectList { get; set; }
    }
}
