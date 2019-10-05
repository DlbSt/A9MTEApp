using A9MTE_Stys.Enums;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace A9MTE_Stys.Model
{
    public class JokeItem
    {
        [PrimaryKey]
        public int Id { get; set; }
        public string Icon { get; set; }
        public string RestId { get; set; }
        public Uri Url { get; set; }
        public string Joke { get; set; }
        public CategoryEnum Category { get; set; }
    }
}
