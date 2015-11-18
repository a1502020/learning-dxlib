using DxLibDLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stg.Enemies
{
    public class RandomWalkerEnemy : Enemy
    {
        public RandomWalkerEnemy(ShootingGame game,
            int rangeX1, int rangeY1, int rangeX2, int rangeY2,
            Position startPos, Position endPos,
            int moveFrame, int stopFrame, int moveCount)
            : base(game)
        {
            Radius = 20;
            Position = new Position(320, -20);
            this.RangeX1 = rangeX1;
            this.RangeY1 = rangeY1;
            this.RangeX2 = rangeX2;
            this.RangeY2 = rangeY2;
            this.StartPos = startPos.Clone();
            this.EndPos = endPos.Clone();
            this.MoveFrame = moveFrame;
            this.StopFrame = stopFrame;
            this.MoveCount = moveCount;
            prevPos = this.StartPos;
            nextPos = new Position((double)Game.Rnd.Next(RangeX1, RangeX2), (double)Game.Rnd.Next(RangeY1, RangeY2));
        }

        public override void Update()
        {
            if (moving)
            {
                Position.X = prevPos.X + (prevPos.X - nextPos.X) * (frame - 2 * MoveFrame) * frame / (MoveFrame * MoveFrame);
                Position.Y = prevPos.Y + (prevPos.Y - nextPos.Y) * (frame - 2 * MoveFrame) * frame / (MoveFrame * MoveFrame);

                ++frame;
                if (frame >= MoveFrame)
                {
                    frame = 0;
                    moving = false;
                    ++cnt;
                    if (cnt >= MoveCount + 1)
                    {
                        this.Dead = true;
                    }
                    else
                    {
                        this.Shoot();
                    }
                }
            }
            else
            {
                Position.X = nextPos.X;
                Position.Y = nextPos.Y;

                ++frame;
                if (frame >= StopFrame)
                {
                    frame = 0;
                    moving = true;
                    prevPos = nextPos;
                    nextPos = (cnt == MoveCount)
                        ? EndPos
                        : new Position((double)Game.Rnd.Next(RangeX1, RangeX2), (double)Game.Rnd.Next(RangeY1, RangeY2));
                }
            }
        }

        public override void Draw()
        {
            DX.DrawCircle((int)Position.X, (int)Position.Y, Radius, DX.GetColor(255, 128, 255));
        }

        public int RangeX1 { get; private set; }
        public int RangeY1 { get; private set; }
        public int RangeX2 { get; private set; }
        public int RangeY2 { get; private set; }
        public Position StartPos { get; private set; }
        public Position EndPos { get; private set; }
        public int MoveFrame { get; private set; }
        public int StopFrame { get; private set; }
        public int MoveCount { get; private set; }

        protected void Shoot()
        {
            var angle = Math.Atan2(Game.OwnChar.Position.Y - Position.Y, Game.OwnChar.Position.X - Position.X);
            Game.EnemyBullets.Add(new Bullet(1, Position, 10, angle, 5.0, DX.GetColor(255, 255, 0)));
        }

        private bool moving = true;
        private int frame = 0;
        private Position prevPos, nextPos;
        private int cnt = 0;
    }
}
