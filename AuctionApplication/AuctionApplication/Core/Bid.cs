using Microsoft.AspNetCore.Http.Metadata;

namespace AuctionApplication.Core;

//TODO: Update class according to assignment instructions
public class Bid
{
    public int Id { get; set; }
    public string Description { get; set; }
    private DateTime _lastUpdated;

    public DateTime LastUpdated
    {
        get => _lastUpdated;
    }

    private Status _status;

    public Status Status
    {
        get => _status;
        set
        {
            //TODO: Implement this
            //if (_status == Status.DONE && value != Status.DoNE)
              //  throw new InvalidOperationException("bid is donee");
            _status = value;
            _lastUpdated = DateTime.Now;
        }
    }

    public Bid(string description, Status status = Status.NO_BID)
    {
        Description = description;
        _lastUpdated = DateTime.Now;
        _status = status;
    }


    public Bid(int id, string description, Status status = Status.NO_BID)
    {
        Id = id;
        Description = description;
        _lastUpdated = DateTime.Now;
        _status = status;
    }

    public Bid(int id, string description, DateTime lastUpdated, Status status = Status.NO_BID)
    {
        Id = id;
        Description = description;
        _lastUpdated = lastUpdated;
        _status = status;
    }

    public Bid() { }

    public override string ToString()
    {
        return $"{Id}: {Description} - {Status}";
    }

}