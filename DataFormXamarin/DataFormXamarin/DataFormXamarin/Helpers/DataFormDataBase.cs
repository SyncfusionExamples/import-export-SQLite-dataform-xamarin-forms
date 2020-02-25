using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataFormXamarin
{
    public class DataFormDataBase
    {
        static readonly Lazy<SQLiteAsyncConnection> lazyInitializer = new Lazy<SQLiteAsyncConnection>(() =>
        {
            return new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
        });

        static SQLiteAsyncConnection Database => lazyInitializer.Value;
        static bool initialized = false;

        public DataFormDataBase()
        {
            InitializeAsync().SafeFireAndForget(false);
        }

        async Task InitializeAsync()
        {
            if (!initialized)
            {
                if (!Database.TableMappings.Any(m => m.MappedType.Name == typeof(ContactsInfo).Name))
                {
                    await Database.CreateTablesAsync(CreateFlags.None, typeof(ContactsInfo)).ConfigureAwait(false);
                    initialized = true;
                }
            }
        }

        public Task<List<ContactsInfo>> GetItemsAsync()
        {
            return Database.Table<ContactsInfo>().ToListAsync();
        }

        public Task<ContactsInfo> GetItemAsync(string name)
        {
            return Database.Table<ContactsInfo>().Where(i => i.Name == name).FirstOrDefaultAsync();
        }

        public Task<int> SaveItemAsync(ContactsInfo item)
        {
            return Database.InsertAsync(item);
        }

        public Task<int> DeleteItemAsync(ContactsInfo item)
        {
            return Database.DeleteAsync(item);
        }
    }
}
