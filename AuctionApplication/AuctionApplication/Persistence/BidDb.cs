using AuctionApplication.Core;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AuctionApplication.Persistence;

public class BidDb
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(256)]
    public string Description { get; set; }

    [Required]
    [DataType(DataType.DateTime)]
    public DateTime LastUpdated { get; set; }

    [Required]
    public Status Status { get; set; }

    // FK and navigation property
    [ForeignKey("AuctionId")]
    public AuctionDb AuctionDb { get; set; }

    public int AuctionId { get; set; }
}