using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicOnline
{
   public class Song
    {
        public string id { get; set; }
        public string name { get; set; }
        public string artist { get; set; }
        public string link { get; set; }
        public string cover { get; set; }
        public string msg { get; set; }
        public List<string> qualities { get; set; }
        public List<string> source_list { get; set; }
        public string source_base { get; set; }
        public string lyric { get; set; }
        public string mv_link { get; set; }


        public override Boolean Equals(Object obj)
        {
            if (this.id == ((Song)obj).id)
            {
                return true;
            }
            return false;
        }
    }
}
