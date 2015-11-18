using DxLibDLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stg.Enemies
{
    public class TurnSEnemy : TurnEnemy
    {
        public TurnSEnemy(ShootingGame game,
            Position startPos, int startTime,
            Position stopPos, int stopTime,
            Position leavePos, int leaveTime,
            int interval, int bulletsCount)
            : base(game, startPos, startTime, stopPos, stopTime, leavePos, leaveTime, interval, bulletsCount)
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
