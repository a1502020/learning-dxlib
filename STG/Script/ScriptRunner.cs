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

        /// <summary>
        /// スクリプトの実行開始処理を行う。
        /// </summary>
        public void Begin()
        {
            Tasks = new List<ScriptTask>();

            // BGM 読み込み
            if (string.IsNullOrEmpty(Script.Bgm))
            {
                bgm = -1;
            }
            else
            {
                bgm = DX.LoadSoundMem(Path.Combine("bgm", Script.Bgm));
                DX.PlaySoundMem(bgm, Script.BgmLoop ? DX.DX_PLAYTYPE_LOOP : DX.DX_PLAYTYPE_BACK);
            }

            // 背景画像読み込み
            game.LoadBackgroundImage(Path.Combine("img", Script.Background));

            // 背景色
            game.BackR = Script.BackR;
            game.BackG = Script.BackG;
            game.BackB = Script.BackB;

            // 待ち時間制御用変数
            waitTime = 0;
            time = 0;
            prevTime = 0;
        }

        /// <summary>
        /// スクリプトの実行終了処理を行う。
        /// </summary>
        public void End()
        {
            if (bgm != -1)
            {
                DX.StopSoundMem(bgm);
                DX.DeleteSoundMem(bgm);
                bgm = -1;
            }
        }

        /// <summary>
        /// スクリプトを1フレームぶん実行する。
        /// </summary>
        public void Step()
        {
            // 1フレームぶんのスクリプトを実行
            while (pc < Script.Statements.Count && waitTime <= 0)
            {
                var st = Script.Statements[pc];
                var task = st.Run();
                if (task != null)
                {
                    Tasks.Add(task);
                }
                if (st.WaitTime > 0)
                {
                    prevTime += time;
                    time = waitTime = st.WaitTime;
                }
                ++pc;
            }
            Tasks.ForEach(task => task.Update());
            Tasks.RemoveAll(task => task.Done);

            // 時間待ち
            if (Script.Time == Script.TimeType.Frame)
            {
                if (waitTime > 0)
                {
                    --waitTime;
                }
            }
            if (Script.Time == Script.TimeType.BgmSample)
            {
                waitTime = time - (DX.GetSoundCurrentPosition(bgm) - prevTime);
            }
            if (Script.Time == Script.TimeType.BgmTime)
            {
                waitTime = time - (DX.GetSoundCurrentTime(bgm) - prevTime);
            }
        }

        /// <summary>
        /// スクリプトの実行が終了したか否か
        /// </summary>
        public bool Finished { get { return pc >= Script.Statements.Count && waitTime <= 0; } }

        /// <summary>
        /// スクリプト
        /// </summary>
        public Script Script { get; private set; }

        /// <summary>
        /// 2フレーム以上にわたって行われる処理
        /// </summary>
        public List<ScriptTask> Tasks { get; private set; }

        private ShootingGame game;

        private int pc = 0;
        private int time = 0, waitTime = 0, prevTime = 0;

        private int bgm;
    }
}
