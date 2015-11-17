using DxLibDLL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stg.Scenes
{
    public sealed class TitleScene : Scene
    {
        public TitleScene(Key key)
        {
            this.key = key;

            scriptPaths = Directory.GetFiles(scriptsDir, "*.txt").ToList();
            choices = scriptPaths
                .Select(path => Path.GetFileNameWithoutExtension(path))
                .ToList();
            choices.Add("/* EXIT */");
        }

        public override void Update()
        {
            if (key.IsPressed(DX.KEY_INPUT_DOWN))
            {
                ++cursor;
                if (cursor >= choices.Count)
                {
                    cursor = 0;
                }
            }
            if (key.IsPressed(DX.KEY_INPUT_UP))
            {
                --cursor;
                if (cursor < 0)
                {
                    cursor = choices.Count - 1;
                }
            }
            if (key.IsPressed(DX.KEY_INPUT_SPACE))
            {
                if (cursor < scriptPaths.Count)
                {
                    NextScene = new StgScene(key, scriptPaths[cursor]);
                }
                else
                {
                    NextScene = null;
                }
            }
        }

        public override void Draw()
        {
            DX.DrawFillBox(0, 0, 640, 480, DX.GetColor(0, 0, 0));
            for (var i = 0; i < choices.Count; ++i)
            {
                DX.DrawString(32, 8 + 16 * i, choices[i], DX.GetColor(255, 255, 255));
            }
            DX.DrawCircle(16, 16 + cursor * 16, 8, DX.GetColor(255, 0, 0));
        }

        public override Scene NextScene { get; protected set; }

        private Key key;
        private List<string> scriptPaths, choices;
        private int cursor = 0;

        private static readonly string scriptsDir = "./scripts";
    }
}
