using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.Identity.Client;

namespace DexterCompany.Models
{
    public class ProdService: BaseAuditableEntity
    {
        public int Id { get; set; }
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;
        [MaxLength(250)]
        public string Description { get; set; } = string.Empty;
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }
        [MaxLength(1)] 
        public string Status { get; set; } = "A";
        public int CatServiceId { get; set; }
        [MaxLength(1)] 
        public string IsPeso { get; set; } = "N";
        [MaxLength(1)] 
        public string IsLavado { get; set; } = "N";             
    }
}