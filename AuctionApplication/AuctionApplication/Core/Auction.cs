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
        get => _status;
        set
        {
            //TODO: Implement this
            //if (_status == Status.DONE && value != Status.DoNE)
            //  throw new InvalidOperationException("bid is donee");
            //logic for when Status is changed
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

    //public Auction(int id, string title, DateTime createdDate)
    //{
    //    Id = id;
    //    Title = title;
    //    CreatedDate = createdDate;
    //}

    //public Auction(int id, string title)
    //{
    //    Id = id;
    //    Title = title;
    //    CreatedDate = DateTime.Now;
    //}

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
        //TODO: implement logic
        return false;
    }

    public override string ToString()
    {
        return $"{Id}: {Title} - completed: {IsCompleted()}";
    }


}