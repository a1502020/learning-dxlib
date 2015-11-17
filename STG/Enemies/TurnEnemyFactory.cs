﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stg.Enemies
{
    class TurnEnemyFactory : EnemyFactory
    {
        public TurnEnemyFactory(ShootingGame game)
        {
            this.game = game;
        }

        public Position StartPos { get; set; }
        public int StartTime { get; set; }
        public Position StopPos { get; set; }
        public int StopTime { get; set; }
        public Position LeavePos { get; set; }
        public int LeaveTime { get; set; }

        public override Enemy Create()
        {
            return new TurnEnemy(game, StartPos, StartTime, StopPos, StopTime, LeavePos, LeaveTime);
        }

        private ShootingGame game;
    }
}