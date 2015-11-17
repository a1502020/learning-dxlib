﻿using DxLibDLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stg.Enemies
{
    public class StraightEnemy : Enemy
    {
        /// <summary>
        /// StraightEnemy を初期化する。
        /// </summary>
        public StraightEnemy(ShootingGame game, Position pos, double angle, double speed, int interval)
            : base(game)
        {
            Radius = 20;
            Color = DX.GetColor(0, 0, 255);
            this.Position = pos.Clone();
            this.Angle = angle;
            this.Speed = speed;
            this.Interval = interval;
        }

        /// <summary>
        /// 1フレームぶんの処理を行う。
        /// </summary>
        /// <param name="bullets"></param>
        /// <param name="ownPos"></param>
        public override void Update()
        {
            // 移動
            Position.X += Speed * Math.Cos(Angle);
            Position.Y += Speed * Math.Sin(Angle);

            // 弾を撃つ
            ++frameCnt;
            if (frameCnt == Interval)
            {
                frameCnt = 0;
                var bulletAngle = Math.Atan2(Game.OwnChar.Position.Y - Position.Y, Game.OwnChar.Position.X - Position.X);
                Game.EnemyBullets.Add(new Bullet(1, Position, 10, bulletAngle, 5.0, DX.GetColor(128, 255, 255)));
            }
        }

        /// <summary>
        /// 描画する。
        /// </summary>
        public override void Draw()
        {
            DX.DrawCircle((int)Position.X, (int)Position.Y, Radius, Color);
        }

        /// <summary>
        /// 色
        /// </summary>
        public uint Color { get; protected set; }

        /// <summary>
        /// 進行方向[rad]
        /// </summary>
        public double Angle { get; private set; }

        /// <summary>
        /// 速度[px/frame]
        /// </summary>
        public double Speed { get; private set; }

        /// <summary>
        /// 弾の発射間隔[frame]
        /// </summary>
        public int Interval { get; private set; }

        private int frameCnt = 0;
    }
}