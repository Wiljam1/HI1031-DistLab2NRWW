using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace AuctionApplication.ViewModels;

//Behöver ha keyless för att jag råka välja DbContext när man gör Views... https://learn.microsoft.com/sv-se/ef/core/modeling/keyless-entity-types?tabs=data-annotations
[Keyless]
public class CreateAuctionVM
{
    //Allt som använder får mata in när den skapar en auktion.
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