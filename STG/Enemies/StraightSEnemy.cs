using DxLibDLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stg.Enemies
{
    public class StraightSEnemy : StraightEnemy
    {
        public StraightSEnemy(ShootingGame game, Position pos, double angle, double speed, int interval)
            : base(game, pos, angle, speed, interval)
        {
        }

        protected override void Shoot()
        {
            var angle = Math.Atan2(Game.OwnChar.Position.Y - Position.Y, Game.OwnChar.Position.X - Position.X);
            for (var i = 0; i < 5; ++i)
            {
                Game.EnemyBullets.Add(new Bullet(
                    1,
                    Position.Clone(),
                    5,
                    angle + (Game.Rnd.NextDouble() - 0.5) * Math.PI / 6,
                    4.0 + Game.Rnd.NextDouble(),
                    DX.GetColor(255, 255, 0)
                ));
            }
        }
    }
}
