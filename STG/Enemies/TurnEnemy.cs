using DxLibDLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stg.Enemies
{
    public class TurnEnemy : Enemy
    {
        public TurnEnemy(ShootingGame game,
            Position startPos, int startTime,
            Position stopPos, int stopTime,
            Position leavePos, int leaveTime,
            int interval, int bulletsCount)
            : base(game)
        {
            Radius = 20;
            Position = startPos.Clone();
            this.StartPos = startPos.Clone();
            this.StartTime = startTime;
            this.StopPos = stopPos.Clone();
            this.StopTime = stopTime;
            this.LeavePos = leavePos.Clone();
            this.LeaveTime = leaveTime;
            this.Interval = interval;
            this.BulletsCount = bulletsCount;
        }

        public override void Update()
        {
            if (phase == 0)
            {
                // start
                Position.X = (StartPos.X - StopPos.X) * (frame - 2 * StartTime) * frame / (StartTime * StartTime) + StartPos.X;
                Position.Y = (StartPos.Y - StopPos.Y) * (frame - 2 * StartTime) * frame / (StartTime * StartTime) + StartPos.Y;

                ++frame;
                if (frame >= StartTime)
                {
                    phase = 1;
                    frame = 0;
                }
            }
            else if (phase == 1)
            {
                // stop
                Position.X = StopPos.X;
                Position.Y = StopPos.Y;

                if (frame < Interval * BulletsCount && frame % Interval == 0)
                {
                    var angle = Math.Atan2(Game.OwnChar.Position.Y - Position.Y, Game.OwnChar.Position.X - Position.X);
                    Game.EnemyBullets.Add(new Bullet(1, Position.Clone(), 5, angle, 5.0, DX.GetColor(255, 255, 0)));
                }

                ++frame;
                if (frame >= StopTime)
                {
                    phase = 2;
                    frame = 0;
                }
            }
            else if (phase == 2)
            {
                // end
                Position.X = (LeavePos.X - StopPos.X) * frame / LeaveTime + StopPos.X;
                Position.Y = (LeavePos.Y - StopPos.Y) * frame / LeaveTime + StopPos.Y;

                ++frame;
                if (frame >= LeaveTime)
                {
                    phase = 3;
                    frame = 0;
                    Dead = true;
                }
            }
        }

        public override void Draw()
        {
            DX.DrawCircle((int)Position.X, (int)Position.Y, Radius, DX.GetColor(0, 128, 255));
        }

        public Position StartPos { get; private set; }
        public int StartTime { get; private set; }
        public Position StopPos { get; private set; }
        public int StopTime { get; private set; }
        public Position LeavePos { get; private set; }
        public int LeaveTime { get; private set; }
        public int Interval { get; private set; }
        public int BulletsCount { get; private set; }

        private int phase = 0;
        private int frame = 0;
    }
}
