using AuctionApplication.Core;

namespace AuctionApplication.ViewModels
{
    public class EditAuctionVM
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string UserName { get; set; }
        public int InitialPrice { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime FinalDate { get; set; }
        public Status Status { get; set; }

        public static EditAuctionVM FromAuction(Auction auction)
        {
            return new EditAuctionVM()
            {
                Id = auction.Id,
                Title = auction.Title,
                Description = auction.Description,
                UserName = auction.UserName,
                InitialPrice = auction.InitialPrice,
                CreatedDate = auction.CreatedDate,
                FinalDate = auction.FinalDate,
                Status = auction.Status
            };
        }
    }
}
