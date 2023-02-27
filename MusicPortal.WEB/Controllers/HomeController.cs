using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
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
using System.IO;
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
        private readonly IWebHostEnvironment _webHostEnvironment;

        public HomeController(IMusicService musicService, IMapper mapper, UserManager<Author> userManager,
            SignInManager<Author> signInManager, IWebHostEnvironment webHostEnvironment)
        {
            _musicService = musicService;
            _mapper = mapper;
            _userManager = userManager;
            _signInManager = signInManager;
            _webHostEnvironment = webHostEnvironment;
            
        }
        
        [HttpGet]
        public IActionResult Index(int page = 1)
        {
            int pageSize = 3;

        
            ICollection<MusicVM> musics = _mapper.Map<ICollection<MusicVM>>(_musicService.GetAll());
            var count = musics.Count();
            var items = musics.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);
            IndexViewModel viewModel = new IndexViewModel
            {
                PageViewModel = pageViewModel,
                Musics = items
            };
            return View(viewModel);
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


        

        [HttpGet]
        public async Task<IActionResult> MusicPage(Guid id)
        {
            MusicVM music = _mapper.Map<MusicVM>(await _musicService.GetAsync(x => x.Id == id));
        
            return View(music);
        }
    }
}
