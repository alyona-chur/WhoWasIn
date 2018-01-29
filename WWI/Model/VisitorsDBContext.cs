using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace WWI.Model
{
    public class VisitorsDBContext : DbContext
    {
        public VisitorsDBContext()
            : base("name=VisitorsDBContext")
        {
        }
        
        public virtual DbSet<Entry> Entrys { get; set; }
        public virtual DbSet<User> Users { get; set; }
    }
}
