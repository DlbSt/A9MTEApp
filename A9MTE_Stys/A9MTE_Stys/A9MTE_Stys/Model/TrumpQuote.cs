using System;
using System.Collections.Generic;
using System.Text;

namespace A9MTE_Stys.Model
{
    public class TrumpQuote
    {
        public string appeared_at { get; set; }
        public string created_at { get; set; }
        public string quote_id { get; set; }
        public List<string> tags { get; set; }
        public DateTime updated_at { get; set; }
        public string value { get; set; }
        public Links _links { get; set; }
        public Embedded _embedded { get; set; }
    }

    public class Links
    {
        public Self self { get; set; }
    }

    public class Self
    {
        public string href { get; set; }
    }

    public class Embedded
    {
        public List<Author> author { get; set; }
        public List<Source> source { get; set; }
    }

    public class Author
    {
        public string created_at { get; set; }
        public string bio { get; set; }
        public string author_id { get; set; }
        public string name { get; set; }
        public string slug { get; set; }
        public string udpdated_at { get; set; }
    }

    public class Source
    {
        public string created_at { get; set; }
        public string filename { get; set; }
        public string quote_source_id { get; set; }
        public string remarks { get; set; }
        public string updated_at { get; set; }
        public string url { get; set; }
    }
}
