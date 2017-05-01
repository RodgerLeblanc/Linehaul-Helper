using Linehaul_Helper.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using SQLite;

namespace Linehaul_Helper.Helpers
{
    class SqlDatabase
    {
        static SQLiteAsyncConnection database;

        public static SQLiteAsyncConnection Database
        {
            get
            {
                if (database == null)
                {
                    database = new SQLiteAsyncConnection(DependencyService.Get<IFileHelper>().GetLocalFilePath("TodoSQLite.db3"));
                }
                return database;
            }
        }

        public SqlDatabase(string dbPath)
        {
            database = new SQLiteAsyncConnection(dbPath);
            database.CreateTableAsync<TodoItem>().Wait();
        }
    }

    public class TodoItem
    {
    }
}
