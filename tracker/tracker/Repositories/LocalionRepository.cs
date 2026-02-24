using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tracker.Models;
namespace tracker.Repositories
{
    public class LocalionRepository : ILocationRepository
    {
        private SQLiteAsyncConnection connection;
        public async Task<List<Microsoft.Maui.Devices.Sensors.Location>> GetAllAsync()
        {
            await CreateConnectionAsync();
            var locations = await connection.Table<Location>().ToListAsync;
            return locations;
        }

        public async Task SaveAsync(Models.Location location)
        {
            await CreateConnectionAsync();
            await connection.InsertAsync(location);
        }

        private SQLiteAsyncConnection connection;

        private async Task CreateConnectionAsync()
        {
            if (connection != null) return;

            var databasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                "Locations.db");

            connection = new SQLiteAsyncConnection(databasePath);
            await connection.CreateTableAsync<Location>();
        }
    }
}
