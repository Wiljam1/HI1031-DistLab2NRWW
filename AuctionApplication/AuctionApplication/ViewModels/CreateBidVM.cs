using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;

namespace AuctionApplication.ViewModels;

[Keyless]
public class CreateBidVM
{
    [Required]
    //[BindNever]
    public string Title { get; set; }

    [Required]
    [Range(0, Int32.MaxValue, ErrorMessage = "Amount must be greater than 0")]
    public int Amount { get; set; }

    //[BindNever]
    [Required]
    public int AuctionId { get; set; }

    public int HighestBidAmount { get; set; }
}