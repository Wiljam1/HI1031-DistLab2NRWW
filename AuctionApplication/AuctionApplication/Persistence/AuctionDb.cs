using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace AuctionApplication.Persistence;

public class AuctionDb
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(128)]
    public string Title { get; set; }

    [Required]
    public string UserName { get; set; }

    [Required]
    [DataType(DataType.DateTime)]
    public DateTime CreatedDate { get; set; }

    public List<BidDb> BidDbs { get; set; } = new List<BidDb>();
}