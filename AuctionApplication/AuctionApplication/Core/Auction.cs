namespace AuctionApplication.Core;

//TODO: Update class according to assignment instructions
public class Auction
{
    public int Id { get; set; }
    public string Title { get; set; }
    public DateTime CreatedDate { get; set; }
    public string UserName { get; set; }
    private List<Bid> _bids = new List<Bid>();
    public IEnumerable<Bid> Bids => _bids;

    public Auction(string title)
    {
        Title = title;
        CreatedDate = DateTime.Now;
    }

    public Auction(int id, string title, DateTime createdDate)
    {
        Id = id;
        Title = title;
        CreatedDate = createdDate;
    }

    public Auction(int id, string title)
    {
        Id = id;
        Title = title;
        CreatedDate = DateTime.Now;
    }

    public Auction() {}

    public void AddBid(Bid newBid)
    {
        _bids.Add(newBid);
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