using AutoMapper;
using MusicPortal.BLL.DTO;
using MusicPortal.BLL.Interfaces;
using MusicPortal.DAL.Interface;
using MusicPortal.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MusicPortal.BLL.Services
{
    public class AuthorService : IAuthorService
    {
        private IUnitOfWork _uow { get; set; }
        private IMapper _mapper;

        public AuthorService(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }


        public async Task<AuthorDTO> GetAsync(Expression<Func<Author, bool>> predicate)
        {
            return _mapper.Map<AuthorDTO>(await _uow.GetRepository<Author>().GetAsync(predicate, x=> x.Musics));
        }

        public ICollection<AuthorDTO> GetAll()
        {
            return _mapper.Map<ICollection<AuthorDTO>>(_uow.GetRepository<Author>().GetAll());
        }

        public Task DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<AuthorDTO> UpdateAsync(AuthorDTO item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));
            Author author = await _uow.GetRepository<Author>().GetAsync(x => x.Id == item.Id);
            if (author == null)
            {
                author = _mapper.Map<Author>(item);
            }

            _mapper.Map(item, author);

            author = await _uow.GetRepository<Author>().UpdateAsync(author);
            await _uow.SaveChangesAsync();
            return _mapper.Map<AuthorDTO>(author);
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
