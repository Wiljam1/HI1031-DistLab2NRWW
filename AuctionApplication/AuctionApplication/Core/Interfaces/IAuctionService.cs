namespace AuctionApplication.Core.Interfaces
{
    public interface IAuctionService
    {
        List<Auction> GetAllActive();
        List<Auction> GetWonAuctions(string username);
        List<Auction> GetActiveAuctionsWithBid(string username);
        Auction GetById(int id);
        bool Add(Auction auction);
        bool Add(Bid bid, int auctionId);
        void UpdateAuctionDescription(Auction auction);
        int GetHighestBidForAuction(Auction auction);
    }
}