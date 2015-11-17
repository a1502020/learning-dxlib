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

        public Script Load(StreamReader reader)
        {
            var res = new Script();
            var readingBody = false;
            while (reader.Peek() >= 0)
            {
                var line = reader.ReadLine();
                if (line == "" || line.StartsWith("//"))
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
        }
    }
}
