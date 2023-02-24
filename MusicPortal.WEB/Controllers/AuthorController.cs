using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MusicPortal.BLL.DTO;
using MusicPortal.BLL.Interfaces;
using MusicPortal.BLL.Services;
using MusicPortal.DAL.Models;
using MusicPortal.WEB.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MusicPortal.WEB.Controllers
{
    public class AuthorController : Controller
    {
        private readonly ILogger<AuthorController> _logger;
        private readonly IAuthorService _authorService;
        private readonly IMapper _mapper;
        private readonly UserManager<Author> _userManager;
        private readonly SignInManager<Author> _signInManager;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public AuthorController(IAuthorService authorService, IMapper mapper, UserManager<Author> userManager,
            SignInManager<Author> signInManager, IWebHostEnvironment webHostEnvironment)
        {
            _authorService = authorService;
            _mapper = mapper;
            _userManager = userManager;
            _signInManager = signInManager;
            _webHostEnvironment = webHostEnvironment;

        }

        [HttpGet]
        public IActionResult Index()
        {
            ViewBag.Authors = _mapper.Map<ICollection<AuthorVM>>(_authorService.GetAll());
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            ViewBag.MusicsAuthor = _mapper.Map<ICollection<MusicVM>>((await _authorService.GetAsync(x => x.Id == id.ToString()))?.Musics);
            AuthorVM authorVM = _mapper.Map<AuthorVM>(await _authorService.GetAsync(x => x.Id == id.ToString()));
            return View(authorVM);
        }

        [HttpGet]
        public async Task<IActionResult> Profile()
        {
            var idSign = _userManager.GetUserId(User);
            AuthorVM authorVM = _mapper.Map<AuthorVM>(await _authorService.GetAsync(x => x.Id == idSign));
            return View(authorVM);
        }

        [HttpPost]
        public async Task<IActionResult> Profile(AuthorVM authorVM)
        {
            var author = await _userManager.GetUserAsync(User);

            author.NickName = authorVM.NickName;
            author.Age = authorVM.Age;
            
            

            await _authorService.UpdateAsync(_mapper.Map<AuthorDTO>(author));
            return RedirectToAction("Index");
        }
    }
}
