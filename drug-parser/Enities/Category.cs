using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace drug_parser.Enities
{
    public class Category
    {
        public virtual int Id { get; protected set; }
        public virtual string Title { get; set; }
        public virtual IList<Drug> Drugs { get; set; }
    }
}
