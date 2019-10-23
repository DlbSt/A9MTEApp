using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace A9MTE_Stys.Model
{
    public class MemeDbItem
    {
        [PrimaryKey]
        public int Id { get; set; }
        public byte[] Image { get; set; }
    }
}
