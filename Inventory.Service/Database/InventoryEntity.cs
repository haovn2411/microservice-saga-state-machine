using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Inventory.Service.Database
{
    [Table("Inventory")]
    public class InventoryEntity
    {
        [Key]
        public string id { get; set; }
        public string name { get; set; }
        public float price { get; set; }
        public int unit {  get; set; }
    }
}
