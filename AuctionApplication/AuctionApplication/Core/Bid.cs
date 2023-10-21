using Microsoft.AspNetCore.Http.Metadata;

namespace AuctionApplication.Core;

public class Bid
{
    public int Id { get; set; }
    public string UserName { get; set; }
    public int Amount { get; set; }
    private DateTime _placedBidTime;

    public DateTime PlacedBidTime
    {
        get => _placedBidTime;
    }

    public Bid(string userName, int amount)
    {
        UserName = userName;
        Amount = amount;
        _placedBidTime = DateTime.Now;
    }

    public Bid(int id, string userName)
    {
        Id = id;
        UserName = userName;
        _placedBidTime = DateTime.Now;
    }

    public Bid(int id, string userName, DateTime placedBidTime)
    {
        Id = id;
        UserName = userName;
        _placedBidTime = placedBidTime;
    }

    public Bid() { }

    public override string ToString()
    {
        return $"{Id}: {UserName} - {Amount}";
    }

}