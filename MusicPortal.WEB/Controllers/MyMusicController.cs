using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MusicPortal.BLL.Interfaces;
using MusicPortal.DAL.Models;
using MusicPortal.WEB.Models.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MusicPortal.WEB.Controllers
{
    [Authorize]
    public class MyMusicController : Controller
    {


        private readonly ILogger<HomeController> _logger;
        private readonly IMusicService _musicService;
        private readonly IMapper _mapper;
        private readonly UserManager<Author> _userManager;
        private readonly SignInManager<Author> _signInManager;

        public MyMusicController(IMusicService musicService, IMapper mapper, UserManager<Author> userManager,
            SignInManager<Author> signInManager)
        {
            _musicService = musicService;
            _mapper = mapper;
            _userManager = userManager;
            _signInManager = signInManager;

        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            ViewBag.MyMusic = _mapper.Map<ICollection<MusicVM>>(_musicService.GetAllMyMusic(await _userManager.GetUserAsync(User))); ;
            return View();
        }
    }
}
