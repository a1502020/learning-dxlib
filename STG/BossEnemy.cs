using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stg
{
    public abstract class BossEnemy : Enemy
    {
        public BossEnemy(ShootingGame game)
            : base(game)
        {
            MaxHP = 1;
            HP = 1;
        }

        /// <summary>
        /// 残り HP
        /// </summary>
        public abstract int HP { get; protected set; }

        /// <summary>
        /// 最大 HP
        /// </summary>
        public abstract int MaxHP { get; protected set; }
    }
}
