using LuckyMeProject.Data;
using LuckyMeProject.Models;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace LuckyMeProject.Controllers
{
	public class HomeController : Controller
	{
		private AppDbContext _dbContext = new AppDbContext();

		[HttpGet]
		public async Task<ActionResult> Index()
		{
			if (Session["UserId"] != null)
			{
				var userid = Convert.ToInt32(Session["UserId"]);
				ViewBag.Data = await _dbContext.Players.SingleOrDefaultAsync(a => a.Id == userid); ;
			}
			return View();
		}

		[HttpGet]
		public async Task<ActionResult> LoadPage(string pageType)
		{
			if (pageType == "login")
			{
				return PartialView("_Login");

			}else if (pageType == "register")
			{
				return PartialView("_Register");

			}else if (pageType == "spin")
			{
				var userid = Convert.ToInt32(Session["UserId"]);
				var data = await _dbContext.Players.SingleOrDefaultAsync(a => a.Id == userid);
				return PartialView("_Spin", data);
			}
			return PartialView("");
		}

		[HttpPost]
		public async Task<ActionResult> Login(Player model)
		{
			if (ModelState.IsValid)
			{
				var data = await _dbContext.Players.SingleOrDefaultAsync(s => s.UserName.Equals(model.UserName) && s.Password.Equals(model.Password));
				if (data != null)
				{
					//add session
					Session["UserId"] = data.Id;
					Session["UserName"] = data.UserName;

					return Json(new { haserror = false, message = "Login successful" });
				}
			}

			return Json(new { haserror = true, error = "Invalid login attempt!" });
		}

		[HttpPost]
		public async Task<ActionResult> Register(Player model)
		{
			if (ModelState.IsValid)
			{
				var check = _dbContext.Players.SingleOrDefaultAsync(s => s.UserName == model.UserName);
				if (check != null)
				{
					
					_dbContext.Configuration.ValidateOnSaveEnabled = false;
					_dbContext.Players.Add(model);
					await _dbContext.SaveChangesAsync();
					return Json(new { haserror = false, message = "Registration Successful" });
				}
				else
				{
					return Json(new { haserror = true, error = "Player already exists" });
				}
			}

			return Json(new { haserror = true, error = "Check the form fields!" });
		}

		[HttpGet]
		public async Task<ActionResult> AddCredit()
		{
			var userId = Convert.ToInt32(Session["UserId"]);
			var data = await _dbContext.Players.SingleOrDefaultAsync(a => a.Id == userId);
			data.Credits += 5;
			await _dbContext.SaveChangesAsync();

			return Json(data.Credits, JsonRequestBehavior.AllowGet);
		}

		[HttpPost]
		public async Task<ActionResult> Payout(Payout data, int credit)
		{
			var userid = Convert.ToInt32(Session["UserId"]);
			var playerInfo = await _dbContext.Players.SingleOrDefaultAsync(a => a.Id == userid);

			bool won = false;

		    if (data.Slot1 == "1.jpg" && data.Slot2 == "1.jpg" && data.Slot3 == "1.jpg")
			{
				playerInfo.Credits += 10;
				playerInfo.Wins += 2;

			}else if (data.Slot1 == "2 .jpg" && data.Slot2 == "2.jpg" && data.Slot3 == "2.jpg")
			{
				playerInfo.Credits += 10;
				playerInfo.Wins += 2;

			}else if (data.Slot1 == "3.jpg" && data.Slot2 == "3.jpg" && data.Slot3 == "3.jpg")
			{
				playerInfo.Credits += 10;
				playerInfo.Wins += 1;
			}
			else if(data.Slot1 != null && data.Slot2 == "1.jpg" && data.Slot3 == "1.jpg")
			{
				playerInfo.Credits += 2;
				playerInfo.Wins += 1;

			}else if (data.Slot1 != null && data.Slot2 == "2.jpg" && data.Slot3 == "2.jpg")
			{
				playerInfo.Credits += 2;
				playerInfo.Wins += 1;
			}
			else if (data.Slot1 != null && data.Slot2 == "3.jpg" && data.Slot3 == "3.jpg")
			{
				playerInfo.Credits += 2;
				playerInfo.Wins += 1;
			}
			else
			{
				playerInfo.Credits -= credit;
			}

			await _dbContext.SaveChangesAsync();
			return Json(new { won,  });
		}
		
		[HttpGet]
		public ActionResult Logout()
		{
			Session.Clear();//remove session
			return RedirectToAction("Index");
		}
		
	}
}