using System.ComponentModel.DataAnnotations;

namespace MyCoreWeb2.Models
{
    public class ProductView
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        [Display(Name = "Název produktu", Description = "Zadejte název produktu. Název produktu je povinné pole")]
        public string? Name { get; set; }

        [MaxLength(500)]
        [Display(Name = "Popis produktu", Description = "Zadejte popis produktu. Popis produktu je volitelné pole")]
        public string? Description { get; set; }

        [Required]
        [Display(Name = "Pořadí")]
        public int DisplayOrder { get; set; }

        [Required]
        [Display(Name = "Rozložení obrazu", Description = "Zadejte rozložení obrazu. Toto je volitelné pole")]
        public int ProductFrameColor_Id { get; set; }

        [Required]
        [Display(Name = "Velikost obrazu", Description = "Zadejte velikost obrazu. Toto je volitelné pole")]
        public int ProductSize_Id { get; set; }

        [Required]
        [Display(Name = "Rozložení obrazu", Description = "Zadejte velikost obrazu. Toto je volitelné pole")]
        public int ProductLayout_Id { get; set; }

        [Required]
        [Display(Name = "Stav produkce", Description = "Zadejte stav produkce. Toto je volitelné pole")]
        public int? ProductionStatus_Id { get; set; }

        [Required]
        [Display(Name = "Smazáno")]
        public bool IsDeleted { get; set; }


        [Display(Name = "Volitelný produkt")]
        public bool? IsCustom { get; set; }
    }
}
