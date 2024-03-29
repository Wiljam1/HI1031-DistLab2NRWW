﻿using AuctionApplication.Areas.Identity.Data;
using AuctionApplication.Core;
using AuctionApplication.Core.Interfaces;
using AuctionApplication.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AuctionApplication.Controllers
{
    [Authorize]
    public class AuctionsController : Controller
    {

        private readonly IAuctionService _auctionService;
        private readonly UserManager<AuctionApplicationUser> _userManager;

        public AuctionsController(IAuctionService auctionService, UserManager<AuctionApplicationUser> userManager)
        {
            _auctionService = auctionService;
            _userManager = userManager;
        }

        [AllowAnonymous]
        // GET: AuctionsController
        public ActionResult Index()
        {
            List<Auction> auctions = _auctionService.GetAllActive();
            List<AuctionVM> auctionVMs = new();
            foreach (var auction in auctions)
            {
                auctionVMs.Add(AuctionVM.FromAuction(auction));
            }
            return View(auctionVMs);
        }

        // GET: AuctionsController/Details/5
        public ActionResult Details(int id)
        {
            Auction auction = _auctionService.GetById(id);
            if (auction == null) return NotFound();

            AuctionDetailsVM detailsVM = AuctionDetailsVM.FromAuction(auction);
            return View(detailsVM);
        }
        
        // GET: AuctionsController/Create
        public ActionResult Create()
        {
            return View();
        }
        
        // POST: AuctionsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateAuctionVM vm)
        {
            if (ModelState.IsValid)
            {
                Auction auction = new Auction()
                {
                    Title = vm.Title,
                    Description = vm.Description,
                    UserName = User.Identity.Name,
                    InitialPrice = vm.InitialPrice,
                    CreatedDate = DateTime.Now,
                    FinalDate = vm.FinalDate,

                };

                if(_auctionService.Add(auction))
                    return RedirectToAction("Index");
            }
            return View(vm);
        }
        
        // GET: AuctionsController/Edit/5
        public ActionResult Edit(int id)
        {
            Auction auction = _auctionService.GetById(id);
            if (!auction.UserName.Equals(User.Identity.Name) || auction.IsExpired()) return Forbid();
            if (auction == null) return NotFound();
            EditAuctionVM editAuctionVM = new EditAuctionVM
            {
                Id = auction.Id,
                Title = auction.Title,
                Description = auction.Description,
                UserName = auction.UserName,
                InitialPrice = auction.InitialPrice,
                CreatedDate = auction.CreatedDate,
                FinalDate = auction.FinalDate,
                Status = auction.Status
            };

            return View(editAuctionVM);
        }
        

        // POST: AuctionsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Auction auction)
        {
            if (ModelState.IsValid)
            {
                _auctionService.UpdateAuctionDescription(auction);

                return RedirectToAction("Details", new { id = auction.Id });
            }
            return View();
        }

        // GET: AuctionsController/CreateBid/id
        public ActionResult CreateBid(int id)
        {
            Auction auction = _auctionService.GetById(id);
            if (auction.UserName.Equals(User.Identity.Name) || auction.IsExpired()) return Forbid();
            if (auction == null) return NotFound();

            int highestBidAmount = _auctionService.GetHighestBidForAuction(auction);

            var createBidVM = new CreateBidVM
            {
                Title = auction.Title,
                AuctionId = id,
                HighestBidAmount = highestBidAmount
            };
            return View(createBidVM);
        }

        // POST: AuctionsController/CreateBid/
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateBid(CreateBidVM vm)
        {
            if (ModelState.IsValid)
            {
                if (vm.Amount <= vm.HighestBidAmount) 
                    return View(vm);

                Bid bid = new Bid(User.Identity.Name, vm.Amount);
                _auctionService.Add(bid, vm.AuctionId);
                return RedirectToAction("Details", new { id = vm.AuctionId });
            }
            return View(vm);
        }

        // GET: AuctionsController/ActiveBids/
        public ActionResult ActiveBids()
        {
            List<Auction> auctions = _auctionService.GetActiveAuctionsWithBid(User.Identity.Name);
            List<AuctionVM> auctionVMs = new();
            foreach (var auction in auctions)
            {
                auctionVMs.Add(AuctionVM.FromAuction(auction));
            }
            return View(auctionVMs);
        }

        // GET: AuctionsController/WonAuctions/
        public ActionResult WonAuctions() 
        {
            List<Auction> auctions = _auctionService.GetWonAuctions(User.Identity.Name);
            List<AuctionVM> auctionVMs = new();
            foreach (var auction in auctions)
            {
                auctionVMs.Add(AuctionVM.FromAuction(auction));
            }
            return View(auctionVMs);
        }

        /*
         --- NOT IMPLEMENTED ---
        // GET: AuctionsController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: AuctionsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        --- ---
        */
    }
}
