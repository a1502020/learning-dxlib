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
                bgm = DX.LoadSoundMem(Path.Combine("bgm", Script.Bgm));
                DX.PlaySoundMem(bgm, Script.BgmLoop ? DX.DX_PLAYTYPE_LOOP : DX.DX_PLAYTYPE_BACK);
            }
        }

        public void End()
        {
            if (bgm != -1)
            {
                DX.StopSoundMem(bgm);
                DX.DeleteSoundMem(bgm);
                bgm = -1;
            }
        }

        public void Step()
        {
            while (pc < Script.Statements.Count && waitTime <= 0)
            {
                var st = Script.Statements[pc];
                st.Run();
                time = waitTime = st.WaitTime;
                ++pc;
            }
            switch (Script.Time)
            {
                case Script.TimeType.Frame:
                    if (waitTime > 0)
                    {
                        --waitTime;
                    }
                    break;
                case Script.TimeType.BgmSample:
                    waitTime = time - DX.GetSoundCurrentPosition(bgm);
                    break;
                case Stg.Script.Script.TimeType.BgmTime:
                    waitTime = time - DX.GetSoundCurrentTime(bgm);
                    break;
            }
        }

        public bool Finished { get { return pc >= Script.Statements.Count && waitTime <= 0; } }

        public Script Script { get; private set; }

        private ShootingGame game;

        private int pc = 0;
        private int time = 0;
        private int waitTime = 0;

        private int bgm;
    }
}
