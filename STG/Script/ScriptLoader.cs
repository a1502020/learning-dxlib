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
                if (line == "")
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
                    if (sp.Count < 2)
                    {
                        throw new FormatException();
                    }
                    var name = sp[1];
                    if (name == "simple")
                    {
                        var factory = new SimpleEnemyFactory(game);
                        res.Statements.Add(new EnemyStatement(factory));
                    }
                    else if (name == "boar")
                    {
                        var factory = new BoarEnemyFactory(game);
                        res.Statements.Add(new EnemyStatement(factory));
                    }
                    else
                    {
                        throw new FormatException(string.Format("Unknown enemy name \"{0}\"", name));
                    }
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
