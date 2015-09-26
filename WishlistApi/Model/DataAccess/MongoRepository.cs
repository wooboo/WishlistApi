using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MongoDB.Driver;
using WishlistApi.Model.Domain;

namespace WishlistApi.Model.DataAccess
{
    public class MongoRepository<T> : IRepository<T> where T : EntityBase
    {
        private readonly string _name;
        private readonly IMongoDatabase _mongoDatabase;
        private readonly IMongoCollection<T> _mongoCollection; 

        public MongoRepository(string name, IMongoDatabase mongoDatabase)
        {
            _name = name;
            _mongoDatabase = mongoDatabase;
            _mongoCollection = _mongoDatabase.GetCollection<T>(name);
        }

        public async Task<IEnumerable<T>> QueryAsync(Expression<Func<T, bool>> predicate)
        {
            var result = await _mongoCollection.FindAsync(predicate).ConfigureAwait(false);
            return await result.ToListAsync().ConfigureAwait(false);
        }

        public async Task<T> GetAsync(string id)
        {
            var filter = Builders<T>.Filter.Eq(o=>o.Id, id);
            return await _mongoCollection.Find(filter).SingleAsync();
        }

        public async Task AddAsync(T data)
        {
            if (data.Id == null)
            {
                data.Id = Guid.NewGuid().ToString();
            }
            await _mongoCollection.InsertOneAsync(data).ConfigureAwait(false);
        }

        public async Task UpdateAsync(string id, T data)
        {
            var filter = Builders<T>.Filter.Eq(o => o.Id, id);

            await _mongoCollection.ReplaceOneAsync(filter, data).ConfigureAwait(false);
        }

        public async Task DeleteAsync(string id)
        {
            var filter = Builders<T>.Filter.Eq(o => o.Id, id);

            await _mongoCollection.DeleteOneAsync(filter).ConfigureAwait(false);
        }
    }
}
