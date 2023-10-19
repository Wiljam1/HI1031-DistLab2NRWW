using AuctionApplication.Core;
using AuctionApplication.Persistence;
using AutoMapper;

namespace AuctionApplication.Mappings;

public class AuctionProfile : Profile
{
    public AuctionProfile()
    {
        //Default mapping
        CreateMap<AuctionDb, Auction>()
            .ReverseMap();
    }
}