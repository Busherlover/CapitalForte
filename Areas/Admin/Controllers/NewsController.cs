using Finances.Models;
using Finances.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Finances.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class NewsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public NewsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            List<News> objNewsList = _unitOfWork.NewsRepository.GetAll().ToList();
            return View(objNewsList);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(News obj)
        {
            if (ModelState.IsValid)
            {
                obj.CreatedDate = DateTime.Now;
                _unitOfWork.NewsRepository.Add(obj);
                _unitOfWork.Save();
                TempData["success"] = "News created successfully";
                return RedirectToAction("Index");
            }

            return View();
        }

        public IActionResult Edit(int? Id)
        {
            if (Id == null || Id == 0)
            {
                return NotFound();
            }
            News? objNews = _unitOfWork.NewsRepository.Get(u => u.Id == Id);
            if (objNews == null)
            {
                return NotFound();
            }
            return View(objNews);
        }
        [HttpPost]
        public IActionResult Edit(News obj)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.NewsRepository.Update(obj);
                _unitOfWork.Save();
                TempData["success"] = "News updated successfully";
                return RedirectToAction("Index");
            }

            return View();
        }

        public IActionResult Delete(int? Id)
        {
            if (Id == null || Id == 0)
            {
                return NotFound();
            }
            News? objNews = _unitOfWork.NewsRepository.Get(u => u.Id == Id);
            if (objNews == null)
            {
                return NotFound();
            }
            return View(objNews);
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? Id)
        {
            News? objNews = _unitOfWork.NewsRepository.Get(u => u.Id == Id);
            if (objNews == null)
            {
                return NotFound();
            }
            _unitOfWork.NewsRepository.Remove(objNews);
            _unitOfWork.Save();
            TempData["success"] = "News deleted successfully";
            return RedirectToAction("Index");
        }

    }
}
