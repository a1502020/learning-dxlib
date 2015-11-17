using DxLibDLL;
using Stg.Script;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stg.Scenes
{
    public sealed class StgScene : Scene
    {
        public StgScene(Key key, string scriptPath)
        {
            this.key = key;
            game = new ShootingGame(key, scriptPath);
        }

        public override void Update()
        {
            game.Update();
            if (game.Finished)
            {
                NextScene = new TitleScene(key);
            }
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
