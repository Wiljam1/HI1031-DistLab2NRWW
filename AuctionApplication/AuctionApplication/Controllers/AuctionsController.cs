﻿using AuctionApplication.Areas.Identity.Data;
using AuctionApplication.Core;
using AuctionApplication.Core.Interfaces;
using AuctionApplication.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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

        //TODO: Låt ej inloggade användare få se alla auctions
        // GET: AuctionsController
        public ActionResult Index() //visa alla auctions?
        {
            string userName = User.Identity.Name; // should be unique
            List<Auction> auctions = _auctionService.GetAllByUserName(userName);
            List<AuctionVM> auctionVMs = new();
            foreach (var auction in auctions)
            {
                auctionVMs.Add(AuctionVM.FromAuction(auction));
            }
            return View(auctionVMs);
        }

        
        // GET: AuctionsController/Details/5
        public ActionResult Details(int id) //visa detaljer om en auction
        {
            Auction auction = _auctionService.GetById(id); //ALLA METODER SOM TAR EN RESURS (id eller något) BEHÖVER KONTROLLERA AUTENTICET
            if (auction == null) return NotFound();

            // check if current user "owns" this auction!
            if(!auction.UserName.Equals(User.Identity.Name)) return BadRequest();

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
                    UserName = User.Identity.Name, // may be null if create auction page is accessed when not logged in.
                    InitialPrice = vm.InitialPrice,
                    FinalDate = vm.FinalDate,

                };
                _auctionService.Add(auction);
                return RedirectToAction("Index");
            }
            return View(vm);
        }
        
        // GET: AuctionsController/Edit/5
        
        public ActionResult Edit(int id)
        {
            Auction auction = _auctionService.GetById(id); //ALLA METODER SOM TAR EN RESURS (id eller något) BEHÖVER KONTROLLERA AUTENTICET
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
                if (auction == null)
                {
                    return NotFound(); 
                }
                //auction.Description = description;
                
                _auctionService.UpdateAuctionDescription(auction);

                return RedirectToAction("Index");
            }
           
            return View();
        }

        /*
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
        */
    }
}
