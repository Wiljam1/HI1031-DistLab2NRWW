using AuctionApplication.Core;
using AuctionApplication.Persistence;
using AutoMapper;

namespace AuctionApplication.Mappings;

public class BidProfile : Profile
{
    public BidProfile()
    {
        CreateMap<BidDb, Bid>()
            .ReverseMap();
    }
}