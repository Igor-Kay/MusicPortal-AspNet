using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MusicPortal.BLL.DTO;
using MusicPortal.BLL.Interfaces;
using MusicPortal.DAL;
using MusicPortal.DAL.Models;
using MusicPortal.WEB.Models;
using MusicPortal.WEB.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;


namespace MusicPortal.WEB.Controllers
{
    
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IMusicService _musicService;
        private readonly IMapper _mapper;
        private readonly UserManager<Author> _userManager;
        private readonly SignInManager<Author> _signInManager;
      
        public HomeController(IMusicService musicService, IMapper mapper, UserManager<Author> userManager,
            SignInManager<Author> signInManager)
        {
            _musicService = musicService;
            _mapper = mapper;
            _userManager = userManager;
            _signInManager = signInManager;
            
        }
        
        [HttpGet]
        public IActionResult Index()
        {
            ViewBag.Musics = _mapper.Map<ICollection<MusicVM>>(_musicService.GetAll());
            return View();
        }
        [HttpPost]
        public IActionResult Index(string? searchString)
        {
            if (searchString == null) searchString = "";
            /*var emp = from e in _musicService.GetAll() where e.Name.ToLower().Contains(searchString) select e;*/
            var music = from e in _musicService.GetAll() where e.Name.ToLower().Contains(searchString) ||
                      e.Genre.ToString().ToLower().Contains(searchString) ||
                      e.Author.NickName.ToLower().Contains(searchString)
                      select e;
            ViewBag.Musics = _mapper.Map<ICollection<MusicVM>>(music);
            return View();
        }

        [Authorize]
        public async Task<IActionResult> Delete(Guid id)
        {
            var authorId = _userManager.GetUserId(User);
            var music = _mapper.Map<MusicVM>(await _musicService.GetAsync(x => x.Id == id));
            if (music.Author.Id == authorId) {
                await _musicService.DeleteAsync(id);
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
            
        }


        [HttpGet]
        public async Task<IActionResult> EditMusic(Guid id)
        {
            return View(_mapper.Map<MusicVM>(await _musicService.GetAsync(x => x.Id == id)));
        }
        [HttpPost]
        public async Task<IActionResult> EditMusic(MusicVM musicVM)
        {
            if (musicVM.Id == default)
            {
                musicVM.Id = Guid.NewGuid();
            }
            await _musicService.UpdateAsync(_mapper.Map<MusicDTO>(musicVM));
            return RedirectToAction("Index");
        }


        [Authorize]
        [HttpGet]
        public IActionResult Add()
        {
            
            return View(new MusicVM());
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Add(MusicVM musicVM, string AuthorId)
        {
            if (musicVM.Id == default)
            {
                musicVM.Id = Guid.NewGuid();
                /*musicVM.Author.Id = "5ec5b3ae - 4a0a - 432d - 9beb - fb29a6a22159";*/
                musicVM.Author = await _userManager.FindByNameAsync(AuthorId);

            }
            await _musicService.AddAsync(_mapper.Map<MusicDTO>(musicVM));
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> MusicPage(Guid id)
        {
            MusicVM music = _mapper.Map<MusicVM>(await _musicService.GetAsync(x => x.Id == id));
        
            return View(music);
        }
    }
}
