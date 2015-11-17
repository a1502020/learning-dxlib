using DxLibDLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stg.Enemies
{
    public sealed class TestBoss : BossEnemy
    {
        public TestBoss(ShootingGame game)
            : base(game)
        {
            Position = new Position(320, 100);
            Radius = 32;
            MaxHP = 100;
            HP = 100;
        }

        public override void Update()
        {
        }

        public override void Draw()
        {
            DX.DrawCircle((int)Position.X, (int)Position.Y, Radius, DX.GetColor(0, 0, 0));
        }

        public override void OnDamaged(int damage)
        {
            HP -= damage;
            if (HP < 0)
            {
                HP = 0;
                Dead = true;
            }
        }

        public override int HP { get; protected set; }

        public override int MaxHP { get; protected set; }
    }
}
