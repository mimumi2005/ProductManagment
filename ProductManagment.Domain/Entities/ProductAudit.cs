using System.ComponentModel.DataAnnotations;

namespace ProductManagment.Domain.Entities
{
    public class ProductAudit
    {
        [Key]
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string UserId { get; set; }
        public string ChangeType { get; set; }
        public DateTime ChangeDate { get; set; }
        public string OldValues { get; set; }
        public string NewValues { get; set; }
    }
}
