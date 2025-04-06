using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using Z_P_B_1.Models;
using System.Linq;

namespace Z_P_B_1.Services
{
    public class Database
    {
        private readonly SQLiteAsyncConnection _database;

        public Database(string path)
        {
            _database = new SQLiteAsyncConnection(path);
            _database.CreateTableAsync<Taryfikator>();
            _database.CreateTableAsync<SessionData>();
        }

        public Task<List<Taryfikator>> GetTar() {
            return _database.Table<Taryfikator>().ToListAsync();
        }

        public Task<int> AddTar(List<Taryfikator> elements)
        {
            foreach (var element in elements)
                _database.InsertAsync(element);
            return Task.FromResult(elements.Count);
        }

        public Task<int> ClearAllTar()
        {
            return _database.DeleteAllAsync<Taryfikator>();
        }

        public async Task<bool> SetTokens(SessionData elements)
        {
            return await Task.FromResult(await _database.InsertOrReplaceAsync(elements) == 1);
        }

        public async Task<SessionData> GetTokens()
        {
            return await _database.Table<SessionData>().FirstOrDefaultAsync(a=>a.id==1);
        }
        public async Task<string> GetJWT()
        {
            var elem = await _database.Table<SessionData>().FirstOrDefaultAsync(a => a.id == 1);
            if (elem == null)
                return null;
            return elem.accessToken;
        }
        public Task<int> LogOut()
        {
            return _database.DeleteAllAsync<SessionData>();
        }
    }
}
