using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace AuctionApplication.ViewModels;

//Behöver ha keyless för att jag råka välja DbContext när man gör Views... https://learn.microsoft.com/sv-se/ef/core/modeling/keyless-entity-types?tabs=data-annotations
[Keyless]
public class CreateAuctionVM
{
    //Allt som använder får mata in när den skapar en auktion.
    [Required]
    [StringLength(128, ErrorMessage = "Max length 128 characters")]
    public string Title { get; set; }
}