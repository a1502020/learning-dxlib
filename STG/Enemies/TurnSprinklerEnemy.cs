using DxLibDLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stg.Enemies
{
    public class TurnSprinklerEnemy : TurnEnemy
    {
        public TurnSprinklerEnemy(ShootingGame game,
            Position startPos, int startTime,
            Position stopPos, int stopTime,
            Position leavePos, int leaveTime,
            int interval, int bulletsCount,
            double startAngle, double endAngle)
            : base(game, startPos, startTime, stopPos, stopTime, leavePos, leaveTime, interval, bulletsCount)
        {
            this.StartAngle = startAngle;
            this.EndAngle = endAngle;
        }

        protected override void Shoot(int bulletNum)
        {
            var angle = BulletsCount == 1
                ? StartAngle
                : StartAngle + bulletNum * (EndAngle - StartAngle) / (BulletsCount - 1);
            Game.EnemyBullets.Add(new Bullet(1, Position.Clone(), 5, angle, 5.0, DX.GetColor(255, 255, 0)));
        }

        /// <summary>
        /// 弾の発射開始時の方向
        /// </summary>
        public double StartAngle { get; private set; }

        /// <summary>
        /// 弾の発射終了時の方向
        /// </summary>
        public double EndAngle { get; private set; }
    }
}
