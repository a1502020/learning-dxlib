using DxLibDLL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stg.Scenes
{
    public class ClearScene : Scene
    {
        public ClearScene(Key key, ShootingGame game)
        {
            this.key = key;
            this.game = game;

            scriptName = Path.GetFileNameWithoutExtension(game.ScriptPath);
        }

        public override void Update()
        {
            game.Update();

            if (key.IsPressed(DX.KEY_INPUT_Z))
            {
                NextScene = new TitleScene(key);
            }
        }

        public override void Draw()
        {
            game.Draw();

            drawStringWithShadow(string.Format("Stage {0} Clear!", scriptName), 8, 8);
            drawStringWithShadow("(Press Z to back to title screen)", 8, 26);
        }

        public override Scene NextScene { get; protected set; }

        private void drawStringWithShadow(string str, int x, int y)
        {
            DX.DrawString(x + 1, y + 1, str, DX.GetColor(0, 0, 0));
            DX.DrawString(x, y, str, DX.GetColor(255, 255, 255));
        }

        private Key key;
        private ShootingGame game;

        private string scriptName;
    }
}
