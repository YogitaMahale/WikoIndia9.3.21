using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofocus.Models.Dtos;
namespace Autofocus.Repository.IRepository
{
    public interface IRepositoryAPI<T> where T : class
    {
        //Task<T> GetAsync(string url, int Id,string token);
        //Task<IEnumerable<T>> GetAllAsync(string url);
        //Task<bool> CreateAsync(string url, T objToCreate);
        //Task<bool> UpdateAsync(string url, T objToUpdate);
        //Task<bool> DeleteAsync(string url, int Id);


        Task<T> GetAsync(string url, int Id);
        Task<IEnumerable<T>> GetAllAsync(string url);
        Task<bool> CreateAsync(string url, T objToCreate);
        Task<bool> UpdateAsync(string url, T objToUpdate);
        Task<bool> DeleteAsync(string url, int Id);
    }
}
