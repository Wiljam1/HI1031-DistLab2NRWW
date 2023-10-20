﻿namespace AuctionApplication.Core.Interfaces
{
    public interface IAuctionService
    {
        List<Auction> GetAll();
        List<Auction> GetAllByUserName(string userName);
        Auction GetById(int id);
        void Add(Auction auction);
        void Add(Bid bid, int auctionId);
        void UpdateAuctionDescription(Auction auction);
        int GetHighestBidForAuction(Auction auction);
    }
}