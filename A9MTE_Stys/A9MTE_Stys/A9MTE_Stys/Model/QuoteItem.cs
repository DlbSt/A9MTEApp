using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace A9MTE_Stys.Model
{
    public class QuoteItem
    {
        [PrimaryKey]
        public int Id { get; set; }
        public string Quote { get; set; }
        public List<string> Tags { get; set; }
        public string Icon { get; set; }
    }
}
