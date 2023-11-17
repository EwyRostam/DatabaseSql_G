using Databas_Examination_G.Entities;
using Databas_Examination_G.Repositories;
using System.Diagnostics;

namespace Databas_Examination_G.Services
{
    internal class DirectorService
    {
        private readonly DirectorRepository _repo;

        public DirectorService(DirectorRepository repo)
        {
            _repo = repo;
        }

        public async Task<DirectorEntity> CreateDirectorAsync(DirectorEntity entity)
        {
            try
            {
                var result = await _repo.ExistsAsync(x => x.FirstName == entity.FirstName && x.LastName == entity.LastName);
                if (!result)
                {
                    var directorEntity = await _repo.CreateAsync(entity);
                    return directorEntity;
                }
                else
                return entity;

            }
            catch (Exception ex) { Debug.WriteLine(ex.Message); }

            return null!;

        }

        public async Task<IEnumerable<DirectorEntity>> GetAllAsync()
        {
            try
            {
                var list = await _repo.GetAllAsync();
                return list;
            } catch (Exception ex) { Console.WriteLine(ex.Message); }
            return null!;
            
        }

        public async Task<DirectorEntity> GetSpecificAsync(string firstName, string lastName)
        {
            try
            {
                var director = await _repo.GetSpecificAsync(x => x.FirstName == firstName && x.LastName == lastName);
                return director ?? null!;
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
            return null!;
        }

        public async Task<DirectorEntity> UpdateAsync(DirectorEntity entity)
        {
            try
            {
                var director = await _repo.UpdateAsync(entity);
                return director;
                
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
            return null!;
        }

        public async Task<bool> DeleteAsync(string firstName, string lastName)
        {
            var result = await _repo.DeleteAsync(x => x.FirstName == firstName && x.LastName == lastName); 
            return result;
        }
    }

}
