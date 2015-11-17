using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stg.Script
{
    public class Script
    {
        public Script()
        {
            Statements = new List<Statement>();
        }

        public List<Statement> Statements { get; private set; }

        public string Bgm { get; set; }
        public bool BgmLoop { get; set; }
    }
}
