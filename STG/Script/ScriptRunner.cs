using DxLibDLL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stg.Script
{
    public class ScriptRunner
    {
        public ScriptRunner(ShootingGame game, Script script)
        {
            this.game = game;
            this.Script = script;
        }

        public void Begin()
        {
            if (string.IsNullOrEmpty(Script.Bgm))
            {
                bgm = -1;
            }
            else
            {
                bgm = DX.LoadMusicMem(Path.Combine("bgm", Script.Bgm));
                DX.PlayMusicMem(bgm, Script.BgmLoop ? DX.DX_PLAYTYPE_LOOP : DX.DX_PLAYTYPE_BACK);
            }
        }

        public void End()
        {
            if (bgm != -1)
            {
                DX.StopMusicMem(bgm);
                DX.DeleteMusicMem(bgm);
                bgm = -1;
            }
        }

        public void Step()
        {
            while (pc < Script.Statements.Count && waitTime <= 0)
            {
                var st = Script.Statements[pc];
                st.Run();
                waitTime = st.WaitTime;
                ++pc;
            }
            if (waitTime > 0)
            {
                --waitTime;
            }
        }

        public bool Finished { get { return pc >= Script.Statements.Count && waitTime <= 0; } }

        public Script Script { get; private set; }

        private ShootingGame game;

        private int pc = 0;
        private int waitTime = 0;

        private int bgm;
    }
}
