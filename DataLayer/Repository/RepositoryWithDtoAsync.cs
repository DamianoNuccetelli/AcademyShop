using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Repository
{
    public class RepositoryWithDtoAsync<T, TDto> : IRepositoryWithDtoAsync<T, TDto> where T : class
    {
        private readonly IRepositoryAsync<T> _repository;
        private readonly IMapper _mapper;

        public RepositoryWithDtoAsync(IRepositoryAsync<T> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TDto>> GetAllAsync()
        {
            var entities = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<TDto>>(entities);
        }

        public async Task<TDto> GetDetailsAsync(int id)
        {
            var entity = await _repository.GetDetailsAsync(id);
            return _mapper.Map<TDto>(entity);
        }

        public async Task<TDto> AddAsync(TDto dto)
        {
            var entity = _mapper.Map<T>(dto);
            entity = await _repository.AddAsync(entity);
            return _mapper.Map<TDto>(entity);
        }

        public async Task<bool> UpdateAsync(TDto dto)
        {
            try
            {
                var entity = _mapper.Map<T>(dto);
                return await _repository.UpdateAsync(entity);
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _repository.DeleteAsync(id);
        }
    }
}