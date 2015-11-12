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
        public StgScene(Key key)
        {
            this.key = key;
            game = new ShootingGame(key);
        }

        public override void Update()
        {
            game.Update();
        }

        public override void Draw()
        {
            game.Draw();
        }

        public override Scene NextScene { get; protected set; }

        private ShootingGame game;
        private Key key;
    }
}
