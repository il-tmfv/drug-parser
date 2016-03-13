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

        public virtual void AddDrug(Drug drug)
        {
            this.Drugs.Add(drug);
            drug.Category = this;
        }

        public Category()
        {
            Drugs = new List<Drug>();
        }
    }
}
