using MongoDB.Bson;
using MongoDB.Driver;
using PoesiaFacil.Data.Context;
using PoesiaFacil.Data.Repositories.Contracts;
using PoesiaFacil.Entities;
using PoesiaFacil.Models.ViewModels;
using System;
using static NuGet.Packaging.PackagingConstants;

namespace PoesiaFacil.Data.Repositories
{
    public class PoemRepository : BaseRepository<Poem>, IPoemRepository
    {
        public PoemRepository() : base("Poems")
        {
            
        }

        public async Task<Pagination<Poem>> GeAllPaginatedAsync(int page = 1, int size = 10, string search = "")
        {
            var data = await _collection
                .Find(x => x.Title.ToLower().Contains(search.ToLower()) || x.Text.ToLower().Contains(search.ToLower()))
                .Skip((page - 1) * size)
                .Limit(size)
                .ToListAsync();

            var pagination = new Pagination<Poem>()
            {
                Items = data,
                PageIndex = page,
                Search = search,
                TotalPages = data.Count
            };

            return pagination;
        }
    }
}
