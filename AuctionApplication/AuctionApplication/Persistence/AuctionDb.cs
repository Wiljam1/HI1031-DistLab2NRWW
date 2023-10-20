using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace AuctionApplication.Persistence;

public class AuctionDb
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(32)]
    public string Title { get; set; }

    [Required]
    [MaxLength(256)]
    public string Description { get; set; }

    [Required]
    public string UserName { get; set; }

    [Required]
    [Range(0, Int32.MaxValue, ErrorMessage = "InitialPrice must be greater than 0")]
    public int InitialPrice { get; set; }

    [Required]
    [DataType(DataType.DateTime)]
    public DateTime CreatedDate { get; set; }

    [Required]
    [DataType(DataType.DateTime)]
    public DateTime FinalDate { get; set; }


    public List<BidDb> BidDbs { get; set; } = new List<BidDb>();
}