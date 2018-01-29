using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WWI.Model
{
    public class EntryElement
    {
        public string UserName { get; set; }
        public int EntryID { get; set; }
        public int UserID { get; set; }
        public DateTime EntryDate { get; set; }
        public String EntryDateStr { get; set; }
    }
}
