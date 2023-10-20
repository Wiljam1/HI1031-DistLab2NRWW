using AuctionApplication.Core;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AuctionApplication.Persistence;

public class BidDb
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string UserName { get; set; }

    [Required]
    [Range(0, Int32.MaxValue)]
    public int Amount { get; set; }

    [Required]
    [DataType(DataType.DateTime)]
    public DateTime PlacedBidTime { get; set; }

    // FK and navigation property
    [ForeignKey("AuctionId")]
    public AuctionDb AuctionDb { get; set; }

    public int AuctionId { get; set; }
}