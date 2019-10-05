using A9MTE_Stys.Interfaces;
using A9MTE_Stys.Model;
using SQLite;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace A9MTE_Stys.Services
{
    public class DatabaseService : IDatabaseService
    {
        private string GetDataPath() => Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "jokes.sqlite3");
        private bool FileExists() => File.Exists(GetDataPath()) ? true : false;

        private bool DbCreate()
        {
            try
            {
                using (var db = new SQLiteConnection(GetDataPath(), SQLiteOpenFlags.Create |
                                                                    SQLiteOpenFlags.ReadWrite |
                                                                    SQLiteOpenFlags.FullMutex))
                {
                    db.CreateTable<JokeItem>();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return false;
            }
        }

        public bool AddJoke(JokeItem joke)
        {
            if (FileExists())
            {
                try
                {
                    using (var db = new SQLiteConnection(GetDataPath(), SQLiteOpenFlags.ReadWrite))
                    {
                        db.Insert(joke);
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
                        using (var db = new SQLiteConnection(GetDataPath(), SQLiteOpenFlags.ReadWrite))
                        {
                            db.Insert(joke);
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
                    using (var db = new SQLiteConnection(GetDataPath(), SQLiteOpenFlags.ReadOnly))
                    {
                        return db.Table<JokeItem>().ToList();
                    }
                }
                catch { return null; }
            }
            else return null;
        }

        public bool DeleteJoke(JokeItem joke)
        {
            try
            {
                using (var db = new SQLiteConnection(GetDataPath(), SQLiteOpenFlags.ReadWrite |
                                                                    SQLiteOpenFlags.FullMutex))
                {
                    db.Delete(joke);
                    return true;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return false;
            }
        }
    }
}
