﻿using AuctionApplication.Core;
using AuctionApplication.Core.Interfaces;
using AutoMapper;
using Microsoft.CodeAnalysis.FlowAnalysis.DataFlow;
using Microsoft.EntityFrameworkCore;

namespace AuctionApplication.Persistence;

public class AuctionSqlPersistence : IAuctionPersistence
{
    private AuctionDbContext _dbContext;
    private IMapper _mapper;

    public AuctionSqlPersistence(AuctionDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public void Add(Auction auction)
    {
        AuctionDb adb = _mapper.Map<AuctionDb>(auction);
        _dbContext.AuctionDbs.Add(adb);
        _dbContext.SaveChanges();
    }

    public List<Auction> GetAll()
    {
        var auctionDbs = _dbContext.AuctionDbs
            //.Where(a => true)
            //.Include(a => a.BidDbs)
            .ToList();

        List<Auction> result = new List<Auction>();
        foreach (AuctionDb adb in auctionDbs)
        {
            //Auction auction = new Auction(

            //    adb.Id,
            //    adb.Title,
            //    adb.CreatedDate);

            Auction auction = _mapper.Map<Auction>(adb);
            result.Add(auction);
        }
        return result;
    }

    public List<Auction> GetAllByUserName(string userName)
    {
        var auctionDbs = _dbContext.AuctionDbs
            .Where(a => a.UserName.Equals(userName)) //Updated for Identity
            .Include(a => a.BidDbs)
            .ToList();

        List<Auction> result = new List<Auction>();
        foreach (AuctionDb adb in auctionDbs)
        {
            Auction auction = _mapper.Map<Auction>(adb);
            result.Add(auction);
        }
        return result;
    }

    public Auction GetById(int id)
    {
        //eager loading
        var auctionDb = _dbContext.AuctionDbs
            .Include(a => a.BidDbs)
            .Where(a => a.Id == id)
            .SingleOrDefault();

        Auction auction = _mapper.Map<Auction>(auctionDb);
        foreach (BidDb bdb in auctionDb.BidDbs)
        {
            auction.AddBid(_mapper.Map<Bid>(bdb));
        }

        return auction;
    }
}