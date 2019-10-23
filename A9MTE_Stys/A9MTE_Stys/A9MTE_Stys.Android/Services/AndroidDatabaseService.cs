using System;
using System.IO;
using System.Diagnostics;
using A9MTE_Stys.Droid.Services;
using A9MTE_Stys.Interfaces;
using A9MTE_Stys.Model;
using SQLite;
using Xamarin.Forms;
using System.Collections.Generic;
using System.Threading.Tasks;

[assembly: Dependency(typeof(AndroidDatabaseService))]
namespace A9MTE_Stys.Droid.Services
{
    public class AndroidDatabaseService : IDatabaseService
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
                    db.CreateTable<MemeDbItem>();
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

        #region ChuckMemes
        public async Task<bool> AddMeme(MemeDbItem meme)
        {
            if (FileExists())
            {
                try
                {
                    using (var db = new SQLiteConnection(GetDataPath(), SQLiteOpenFlags.ReadWrite |
                                                                        SQLiteOpenFlags.FullMutex))
                    {
                        await Task.Run(() => db.Insert(meme));
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
                            await Task.Run(() => db.Insert(meme));
                            return true;
                        }
                    }
                    catch { return false; }
                }
                else return false;
            }
        }

        public List<MemeDbItem> GetMemes()
        {
            if (FileExists())
            {
                try
                {
                    using (var db = new SQLiteConnection(GetDataPath(), SQLiteOpenFlags.ReadOnly))
                    {
                        return db.Table<MemeDbItem>().ToList();
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                    return null; 
                }
            }
            else return null;
        }

        public MemeDbItem GetRandomMeme()
        {
            if (FileExists())
            {
                try
                {
                    using (var db = new SQLiteConnection(GetDataPath(), SQLiteOpenFlags.ReadOnly))
                    {
                        var vals = db.Table<MemeDbItem>().ToList();
                        var count = vals.Count;
                        return vals[random.Next(0, count - 1)];
                    }
                }
                catch { return null; }
            }
            else return null;
        }

        public async Task<bool> DeleteMeme(MemeDbItem meme)
        {
            try
            {
                using (var db = new SQLiteConnection(GetDataPath(), SQLiteOpenFlags.ReadWrite |
                                                                    SQLiteOpenFlags.FullMutex))
                {
                    await Task.Run(() => db.Delete(meme));
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
            return Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "jokes.sqlite3");
        }

        private bool FileExists() => File.Exists(GetDataPath()) ? true : false;

        #endregion
    }
}