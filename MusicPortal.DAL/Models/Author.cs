using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicPortal.DAL.Models
{
    public class Author: IdentityUser
    {
        public string NickName { get; set; }
        public ICollection<Music>? Musics { get; set; }
    }
}
