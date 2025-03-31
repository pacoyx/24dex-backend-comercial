using System.ComponentModel.DataAnnotations;

namespace DexterCompany.Models
{
    public class TypeDocument
    {
        public int Id { get; set; }
        [StringLength(50)]
        public string Name { get; set; } = string.Empty;
        [StringLength(100)]
        public string Description { get; set; } = string.Empty;
        [StringLength(1)]
        public string Status { get; set; } = string.Empty;
        [StringLength(2)]
        public string DocId { get; set; } = string.Empty;
        
    }
}