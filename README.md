# How to import and export data objects from SQLite Offline database into dataform (SfDataForm)?

SfDataForm allows you to bind data from local database using SQLite. To achieve this, follow the below steps:

**Step 1:** Install the sqlite-net-pcl NuGet package in your shared code project.

**Step 2:** Configure app constants to provide common configurations.

**Step 3:** Create a database access class and initialize the data manipulation methods.

**Step 4:** Populate the data into the database.

**Step 6:** Save the input values into the database after editing the form.

The following code example explains about app constants to provide the common configurations.

``` C#
public static class Constants
{
        public const string DatabaseFilename = "SampleSQLites.db3";

        public const SQLite.SQLiteOpenFlags Flags =
            SQLite.SQLiteOpenFlags.ReadWrite |
            SQLite.SQLiteOpenFlags.Create |
            SQLite.SQLiteOpenFlags.SharedCache;

        public static string DatabasePath
        {
            get
            {
                var basePath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                return Path.Combine(basePath, DatabaseFilename);
            }
        }
 }

```
Refer to the following code to create a database access class and initialize the data manipulation methods.

``` C#
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
```
You can refer to the following link to create the SQLite connection.

https://docs.microsoft.com/en-us/xamarin/xamarin-forms/data-cloud/data/databases

The following code explains how to populate the data into the database.

``` C#
public DataFormViewModel()
{
            ContactsInfo contact = new ContactsInfo();
            contact.Name = "Jhon";
            contact.ContactNumber = 12345;
            contact.Email = "abc@mail.com";
            contact.BirthDate = new DateTime(1990, 01, 01);

            App.Database.SaveItemAsync(contact);            
            …
  }
```
Refer to the following code to get  data from the database and store it in a property to bind with DataObject.

``` C#
public DataFormViewModel()
{
            …     
            this.GetDataModel();
}

/// <summary>
/// Gets the data from database
/// </summary>
private async void GetDataModel()        
{
      var dataObject = await App.Database.GetItemsAsync();
      this.Contact = dataObject.FirstOrDefault();        
}
```
Save the input values into the database after editing the form.
``` C#
/// <summary>        
/// Gets input values from DataForm and save into the database
/// </summary>        
/// <param name="dataForm"></param>        
private void SaveInputValues(SfDataForm dataForm)        
{
        var dataObject = dataForm.DataObject as ContactsInfo;
        App.Database.SaveItemAsync(dataObject);        
}
```
