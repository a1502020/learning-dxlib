using DxLibDLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stg.Scenes
{
    public sealed class TitleScene : Scene
    {
        public TitleScene()
        {
        }

        public override void Update()
        {
            DX.GetHitKeyStateAll(out keys[0]);
            if (keys[DX.KEY_INPUT_SPACE] != 0)
            {
                NextScene = new StgScene();
            }
        }

        public override void Draw()
        {
            DX.DrawFillBox(0, 0, 640, 480, DX.GetColor(0, 0, 0));
            DX.DrawString(0, 0, "Title", DX.GetColor(255, 255, 255));
        }

        public override Scene NextScene { get; protected set; }

        private byte[] keys = new byte[256];
    }
}
