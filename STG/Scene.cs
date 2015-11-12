using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stg
{
    public abstract class Scene
    {
        public Scene()
        {
            NextScene = this;
        }

        public abstract void Update();
        public abstract void Draw();
        public abstract Scene NextScene { get; protected set; }
    }
}
