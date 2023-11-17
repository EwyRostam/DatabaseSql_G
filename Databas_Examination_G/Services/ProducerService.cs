using Databas_Examination_G.Entities;
using Databas_Examination_G.Repositories;
using System.Diagnostics;

namespace Databas_Examination_G.Services
{
    internal class ProducerService
    {
        private readonly ProductionCompanyRepository _repo;

        public ProducerService(ProductionCompanyRepository repo)
        {
            _repo = repo;
        }

        public async Task<ProductionCompanyEntity> CreateProducerAsync(ProductionCompanyEntity entity)
        {
            try
            {
                var result = await _repo.ExistsAsync(x => x.Name == entity.Name);
                if (!result)
                {
                    var genreEntity = await _repo.CreateAsync(entity);
                    return genreEntity;
                }
                else
                return entity;

            }
            catch (Exception ex) { Debug.WriteLine(ex.Message); }

            return null!;

        }
        public async Task<IEnumerable<ProductionCompanyEntity>> GetAllAsync()
        {
            try
            {
                var list = await _repo.GetAllAsync();
                return list;
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
            return null!;

        }

        public async Task<ProductionCompanyEntity> GetSpecificAsync(string name)
        {
            try
            {
                var producer = await _repo.GetSpecificAsync(x => x.Name == name);
                return producer ?? null!;
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
            return null!;
        }

        public async Task<ProductionCompanyEntity> UpdateAsync(ProductionCompanyEntity entity)
        {
            try
            {
                var producer = await _repo.UpdateAsync(entity);
                return producer;

            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
            return null!;
        }

        public async Task<bool> DeleteAsync(string name)
        {
            var result = await _repo.DeleteAsync(x => x.Name == name);
            return result;
        }
    }
}
