using Stg.Enemies;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stg.Script
{
    public class ScriptLoader
    {
        public ScriptLoader(ShootingGame game)
        {
            this.game = game;
        }

        /// <summary>
        /// スクリプトを読み込む。
        /// </summary>
        /// <param name="reader">スクリプトの入力ストリーム</param>
        /// <returns>読み込んだスクリプト</returns>
        /// <exception cref="FormatException"></exception>
        public Script Load(StreamReader reader)
        {
            var res = new Script();
            var readingBody = false;
            while (reader.Peek() >= 0)
            {
                var line = reader.ReadLine();
                var pos = line.IndexOf("//");
                if (pos >= 0)
                {
                    line = line.Substring(0, pos);
                }
                line = line.Trim();
                if (line == "")
                {
                    continue;
                }
                if (line == "--")
                {
                    readingBody = true;
                    continue;
                }

                if (readingBody)
                {
                    readBody(line, res);
                }
                else
                {
                    readHeader(line, res);
                }
            }
            return res;
        }

        /// <summary>
        /// スクリプトを読み込む。
        /// </summary>
        /// <param name="filePath">スクリプトのファイルパス</param>
        /// <returns>読み込んだスクリプト</returns>
        /// <exception cref="FormatException"></exception>
        public Script Load(string filePath)
        {
            using (var reader = new StreamReader(filePath))
            {
                return this.Load(reader);
            }
        }

        private ShootingGame game;

        private void readHeader(string line, Script res)
        {
            var sp = line.Split(' ').ToList();
            var cmd = sp[0];
            if (cmd == "bgm")
            {
                if (sp.Count < 2)
                {
                    throw new FormatException("bgm with too few arguments.");
                }
                res.Bgm = sp[1];
                res.BgmLoop = (sp.Count < 3 || sp[2] == "loop");
            }
            else if (cmd == "time")
            {
                if (sp.Count < 2)
                {
                    throw new FormatException("time with too few arguments.");
                }

                if (sp[1] == "frame")
                {
                    res.Time = Script.TimeType.Frame;
                }
                else if (sp[1] == "bgm_sample")
                {
                    res.Time = Script.TimeType.BgmSample;
                }
                else if (sp[1] == "bgm_time")
                {
                    res.Time = Script.TimeType.BgmTime;
                }
                else
                {
                    throw new FormatException("time with invalid arguments.");
                }
            }
            else if (cmd == "color")
            {
                if (sp.Count < 4)
                {
                    throw new FormatException("color with too few arguments.");
                }

                res.BackR = int.Parse(sp[1]);
                res.BackG = int.Parse(sp[2]);
                res.BackB = int.Parse(sp[3]);
            }
            else if (cmd == "back")
            {
                if (sp.Count < 2)
                {
                    throw new FormatException("back with too few arguments.");
                }

                res.Background = sp[1];
            }
            else
            {
                throw new FormatException(string.Format("unknown header \"{0}\"", cmd));
            }
        }

        private void readBody(string line, Script res)
        {
            int time;
            if (int.TryParse(line, out time))
            {
                res.Statements.Add(new WaitStatement(time));
                return;
            }
            var sp = line.Split(' ').ToList();
            var c = sp[0];
            if (c == "e")
            {
                res.Statements.Add(new EnemyStatement(game, sp));
            }
            else if (c == "b")
            {
                res.Statements.Add(new BossStatement(game, sp));
            }
            else if (c == "color")
            {
                res.Statements.Add(new ColorStatement(game, sp));
            }
        }
    }
}
