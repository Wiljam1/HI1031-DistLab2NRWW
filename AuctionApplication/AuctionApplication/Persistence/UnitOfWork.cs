namespace AuctionApplication.Persistence;

public class UnitOfWork : IDisposable
{
    private AuctionDbContext _context;
    private GenericRepository<AuctionDb> auctionRepository;
    private GenericRepository<BidDb> bidRepository;

    public UnitOfWork(AuctionDbContext context)
    {
        _context = context;
    }

    public GenericRepository<AuctionDb> AuctionRepository
    {
        get
        {
            if (this.auctionRepository == null)
            {
                this.auctionRepository = new
                    GenericRepository<AuctionDb>(_context);
            }
            return auctionRepository;
        }
    }
    public GenericRepository<BidDb> BidRepository
    {
        get
        {
            if (this.bidRepository == null)
            {
                this.bidRepository = new GenericRepository<BidDb>
                    (_context);
            }
            return bidRepository;
        }
    }
    public void Save()
    {
        _context.SaveChanges();
    }
    private bool disposed = false;
    protected virtual void Dispose(bool disposing)
    {
        if (!this.disposed)
        {
            if (disposing)
            {
                _context.Dispose();
            }
        }
        this.disposed = true;
    }
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}
