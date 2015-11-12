using DxLibDLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stg.Scenes
{
    public sealed class StgScene : Scene
    {
        public override void Update()
        {
            game.Update();
        }

        public override void Draw()
        {
            game.Draw();
        }

        public override Scene NextScene { get; protected set; }

        private ShootingGame game = new ShootingGame();
    }
}
