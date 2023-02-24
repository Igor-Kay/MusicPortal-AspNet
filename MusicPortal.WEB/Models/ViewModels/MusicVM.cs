using MusicPortal.DAL.Enum;
using MusicPortal.DAL.Models;
using System;

namespace MusicPortal.WEB.Models.ViewModels
{
    public class MusicVM
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public GenreEnum Genre { get; set; }

        public Author Author { get; set; }
    }
}
