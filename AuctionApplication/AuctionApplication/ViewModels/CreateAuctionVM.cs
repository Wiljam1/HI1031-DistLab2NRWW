using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace AuctionApplication.ViewModels;

//Keyless: https://learn.microsoft.com/sv-se/ef/core/modeling/keyless-entity-types?tabs=data-annotations
[Keyless]
public class CreateAuctionVM
{
    [Required]
    [StringLength(32, ErrorMessage = "Max length 32 characters")]
    public string Title { get; set; }

    [Required]
    [StringLength(256, ErrorMessage = "Max length 256 characters")]
    public string Description { get; set; }

    [Required]
    [Range(0, Int32.MaxValue, ErrorMessage = "InitialPrice must be greater than 0")]
    public int InitialPrice { get; set; }

    [Required]
    [DataType(DataType.DateTime)]
    public DateTime FinalDate { get; set; }
}