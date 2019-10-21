using A9MTE_Stys.Interfaces;
using A9MTE_Stys.Model;
using SQLite;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Xamarin.Forms;

namespace A9MTE_Stys.Services
{
    public class DatabaseService : IDatabaseService
    {
        #region Fields
        private Random random = new Random();
        #endregion

        private bool DbCreate()
        {
            try
            {
                using (var db = new SQLiteConnection(GetDataPath(), SQLiteOpenFlags.Create |
                                                                    SQLiteOpenFlags.ReadWrite |
                                                                    SQLiteOpenFlags.FullMutex))
                {
                    db.CreateTable<JokeItem>();
                    db.CreateTable<QuoteDbItem>();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return false;
            }
        }

        public bool DeleteDatabase()
        {
            if (FileExists())
            {
                try
                {
                    File.Delete(GetDataPath());
                    return true;
                }
                catch { return false; }
            }
            else return false;
        }

        #region ChuckJokes

        public async Task<bool> AddJoke(JokeItem joke)
        {
            if (FileExists())
            {
                try
                {
                    using (var db = new SQLiteConnection(GetDataPath(), SQLiteOpenFlags.ReadWrite |
                                                                        SQLiteOpenFlags.FullMutex))
                    {
                        await Task.Run(() => db.Insert(joke));
                        return true;
                    }
                }
                catch { return false; }
            }
            else
            {
                if (DbCreate())
                {
                    try
                    {
                        using (var db = new SQLiteConnection(GetDataPath(), SQLiteOpenFlags.ReadWrite |
                                                                            SQLiteOpenFlags.FullMutex))
                        {
                            await Task.Run(() => db.Insert(joke));
                            return true;
                        }
                    }
                    catch { return false; }
                }
                else return false;
            }
        }

        public List<JokeItem> GetJokes()
        {
            if (FileExists())
            {
                try
                {
                    using (var db = new SQLiteConnection(GetDataPath(), SQLiteOpenFlags.ReadOnly |
                                                                        SQLiteOpenFlags.FullMutex))
                    {
                        return db.Table<JokeItem>().ToList();
                    }
                }
                catch { return null; }
            }
            else return null;
        }

        public JokeItem GetRandomJoke()
        {
            if (FileExists())
            {
                try
                {
                    using (var db = new SQLiteConnection(GetDataPath(), SQLiteOpenFlags.ReadOnly))
                    {
                        var vals = db.Table<JokeItem>().ToList();
                        var count = vals.Count;
                        return vals[random.Next(0, count - 1)];
                    }
                }
                catch { return null; }
            }
            else return null;
        }

        public async Task<bool> DeleteJoke(JokeItem joke)
        {
            try
            {
                using (var db = new SQLiteConnection(GetDataPath(), SQLiteOpenFlags.ReadWrite |
                                                                    SQLiteOpenFlags.FullMutex))
                {
                    await Task.Run(() => db.Delete(joke));
                    return true;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return false;
            }
        }

        #endregion

        #region TrumpQuotes

        public async Task<bool> AddQuote(QuoteDbItem quote)
        {
            if (FileExists())
            {
                try
                {
                    using (var db = new SQLiteConnection(GetDataPath(), SQLiteOpenFlags.ReadWrite |
                                                                        SQLiteOpenFlags.FullMutex))
                    {
                        await Task.Run(() => db.Insert(quote));
                        return true;
                    }
                }
                catch { return false; }
            }
            else
            {
                if (DbCreate())
                {
                    try
                    {
                        using (var db = new SQLiteConnection(GetDataPath(), SQLiteOpenFlags.ReadWrite |
                                                                            SQLiteOpenFlags.FullMutex))
                        {
                            await Task.Run(() => db.Insert(quote));
                            return true;
                        }
                    }
                    catch { return false; }
                }
                else return false;
            }
        }

        public List<QuoteDbItem> GetQuotes()
        {
            if (FileExists())
            {
                try
                {
                    using (var db = new SQLiteConnection(GetDataPath(), SQLiteOpenFlags.ReadOnly))
                    {
                        return db.Table<QuoteDbItem>().ToList();
                    }
                }
                catch { return null; }
            }
            else return null;
        }

        public QuoteDbItem GetRandomQuote()
        {
            if (FileExists())
            {
                try
                {
                    using (var db = new SQLiteConnection(GetDataPath(), SQLiteOpenFlags.ReadOnly))
                    {
                        var vals = db.Table<QuoteDbItem>().ToList();
                        var count = vals.Count;
                        return vals[random.Next(0, count - 1)];
                    }
                }
                catch { return null; }
            }
            else return null;
        }

        public async Task<bool> DeleteQuote(QuoteDbItem quote)
        {
            try
            {
                using (var db = new SQLiteConnection(GetDataPath(), SQLiteOpenFlags.ReadWrite |
                                                                    SQLiteOpenFlags.FullMutex))
                {
                    await Task.Run(() => db.Delete(quote));
                    return true;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return false;
            }
        }

        #endregion

        #region HelperMethods
        private string GetDataPath()
        {
            if (Device.RuntimePlatform == Device.UWP) return Path.Combine(ApplicationData.Current.LocalFolder.Path, "jokes.sqlite3");
            else return Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "jokes.sqlite3");
        }

        private bool FileExists() => File.Exists(GetDataPath()) ? true : false;

        #endregion
    }
}
