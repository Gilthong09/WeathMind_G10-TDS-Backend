using AutoMapper;
using WealthMind.Core.Application.Interfaces.Repositories;
using WealthMind.Core.Application.Interfaces.Services;

namespace WealthMind.Core.Application.Services.MainServices
{
    public class GenericService<SaveViewModel, ViewModel, Model> : IGenericService<SaveViewModel, ViewModel, Model>
        where SaveViewModel : class
        where ViewModel : class
        where Model : class
    {
        private readonly IGenericRepository<Model> _repository;
        private readonly IMapper _mapper;

        public GenericService(IGenericRepository<Model> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public virtual async Task Update(SaveViewModel vm, string id)
        {
            Model entity = _mapper.Map<Model>(vm);
            await _repository.UpdateAsync(entity, id);
        }

        public virtual async Task<SaveViewModel> Add(SaveViewModel vm)
        {
            Model entity = _mapper.Map<Model>(vm);

            try
            {
                entity = await _repository.AddAsync(entity);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }



            SaveViewModel entityVm = _mapper.Map<SaveViewModel>(entity);

            return entityVm;
        }

        public virtual async Task Delete(string id)
        {
            var product = await _repository.GetByIdAsync(id);
            await _repository.DeleteAsync(product);
        }

        public virtual async Task<SaveViewModel> GetByIdSaveViewModel(string id, bool trackChanges = false)
        {
            var entity = await _repository.GetByIdAsync(id, trackChanges);
            SaveViewModel vm = _mapper.Map<SaveViewModel>(entity);
            return vm;
        }

        public virtual async Task<List<ViewModel>> GetAllViewModel()
        {
            var entityList = await _repository.GetAllAsync();

            return _mapper.Map<List<ViewModel>>(entityList);
        }

        public virtual async Task<List<ViewModel>> GetAllByUserIdAsync(string userId, bool trackChanges = false)
        {
            var entityList = await _repository.GetAllByUSerIdAsync(userId, trackChanges);
            return _mapper.Map<List<ViewModel>>(entityList);
        }
    }
}
