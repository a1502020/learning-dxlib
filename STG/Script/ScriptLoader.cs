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
            while (reader.Peek() >= 0)
            {
                var line = reader.ReadLine();
                if (line == "" || line.StartsWith("//"))
                {
                    continue;
                }

                int time;
                if (int.TryParse(line, out time))
                {
                    res.Statements.Add(new WaitStatement(time));
                    continue;
                }

                var sp = line.Split(' ').ToList();
                var c = sp[0];
                if (c == "e")
                {
                    res.Statements.Add(new EnemyStatement(game, sp));
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
    }
}
