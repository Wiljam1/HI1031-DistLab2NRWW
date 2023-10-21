using Microsoft.IdentityModel.Tokens;

namespace AuctionApplication.Core;

//TODO: Update class according to assignment instructions
public class Auction
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string UserName { get; set; }
    public int InitialPrice { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime FinalDate { get; set; }

    private Status _status;

    public Status Status
    {
        get
        {
            IsCompleted();
            return _status;
        }
        set
        {
            if (_status == Status.DONE && value != Status.DONE)
              throw new InvalidOperationException("Auction is already finished!");
            _status = value;
        }
    }

    private List<Bid> _bids = new List<Bid>();
    public IEnumerable<Bid> Bids => _bids;

    public Auction(string title, string description, int initialPrice, DateTime finalDate)
    {
        Title = title;
        Description = description;
        InitialPrice = initialPrice;
        CreatedDate = DateTime.Now;
        FinalDate = finalDate;
    }

    public Auction() {}

    public void AddBid(Bid newBid)
    {
        _bids.Add(newBid);
    }

    public IEnumerable<Bid> GetSortedBidsByAmount()
    {
        return _bids.OrderByDescending(b => b.Amount);
    }

    public bool IsCompleted()
    {
        if (Bids.IsNullOrEmpty())
        {
            _status = Status.NO_BID;
            return false;
        }
        if (FinalDate < DateTime.Now)
        {
            _status = Status.DONE;
            return true;
        }

        _status = Status.IN_PROGRESS;
        return false;
    }

    public override string ToString()
    {
        return $"{Id}: {Title} - completed: {IsCompleted()}";
    }


}