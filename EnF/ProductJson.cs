using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnF
{
    public class Imalist
    {
        public string Image { get; set; }
        public int ProductId { get; set; }
    }

    public class ImalistRoot
    {
        public List<Imalist> imalist { get; set; }
    }
}
