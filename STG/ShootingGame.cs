using DxLibDLL;
using Stg.Enemies;
using Stg.Script;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stg
{
    public class ShootingGame
    {
        public ShootingGame(Key key, string scriptPath)
        {
            OwnChar = new OwnCharacter(this, new Position(320.0, 400.0));
            OwnBullets = new List<Bullet>();
            Enemies = new List<Enemy>();
            EnemyBullets = new List<Bullet>();

            imgBack = DX.LoadGraph("img/back.bmp");
            this.key = key;

            // スクリプト読み込み
            var loader = new ScriptLoader(this);
            var script = loader.Load(scriptPath);
            runner = new ScriptRunner(this, script);
        }

        /// <summary>
        /// 1フレームぶんの処理を行う。
        /// </summary>
        public void Update()
        {
            // スクリプト実行
            runner.Step();

            // 自機
            OwnChar.Update(key);

            // 敵
            Enemies.ForEach(enemy => enemy.Update());

            // 自機の弾
            OwnBullets.ForEach(bullet => bullet.Update());

            // 敵の弾
            EnemyBullets.ForEach(bullet => bullet.Update());

            // ボス
            if (Boss != null)
            {
                Boss.Update();
            }

            // 画面外の弾を削除
            OwnBullets.RemoveAll(bullet =>
                bullet.Position.X < -bullet.Radius
                || bullet.Position.Y < -bullet.Radius
                || bullet.Position.X > 640 + bullet.Radius
                || bullet.Position.Y > 480 + bullet.Radius);
            EnemyBullets.RemoveAll(bullet =>
                bullet.Position.X < -bullet.Radius
                || bullet.Position.Y < -bullet.Radius
                || bullet.Position.X > 640 + bullet.Radius
                || bullet.Position.Y > 480 + bullet.Radius);

            // 敵と弾の当たり判定
            for (var bi = OwnBullets.Count - 1; bi >= 0; --bi)
            {
                var bullet = OwnBullets[bi];
                for (var ei = Enemies.Count - 1; ei >= 0; --ei)
                {
                    var enemy = Enemies[ei];
                    if (collidesCircleCircle(
                        bullet.Position.X, bullet.Position.Y, bullet.Radius,
                        enemy.Position.X, enemy.Position.Y, enemy.Radius))
                    {
                        enemy.OnDamaged(bullet.Power);
                        OwnBullets.RemoveAt(bi);
                        break;
                    }
                }
            }
            Enemies.RemoveAll(enemy => enemy.Dead);

            // ボスと弾の当たり判定
            if (Boss != null)
            {
                for (var bi = OwnBullets.Count - 1; bi >= 0; --bi)
                {
                    var bullet = OwnBullets[bi];
                    if (collidesCircleCircle(
                        bullet.Position.X, bullet.Position.Y, bullet.Radius,
                        Boss.Position.X, Boss.Position.Y, Boss.Radius))
                    {
                        Boss.OnDamaged(bullet.Power);
                        OwnBullets.RemoveAt(bi);
                    }
                }
            }

            // 自機と敵の当たり判定
            foreach (var enemy in Enemies)
            {
                if (collidesCircleCircle(
                    OwnChar.Position.X, OwnChar.Position.Y, OwnChar.Radius,
                    enemy.Position.X, enemy.Position.Y, enemy.Radius))
                {
                    finished = true;
                }
            }

            // 自機とボスの当たり判定
            if (Boss != null)
            {
                if (collidesCircleCircle(
                    OwnChar.Position.X, OwnChar.Position.Y, OwnChar.Radius,
                    Boss.Position.X, Boss.Position.Y, Boss.Radius))
                {
                    finished = true;
                }
            }

            // 自機と敵の弾の当たり判定
            foreach (var bullet in EnemyBullets)
            {
                if (collidesCircleCircle(
                    OwnChar.Position.X, OwnChar.Position.Y, OwnChar.Radius,
                    bullet.Position.X, bullet.Position.Y, bullet.Radius))
                {
                    finished = true;
                }
            }
        }

        /// <summary>
        /// 画面を描画する。
        /// </summary>
        public void Draw()
        {
            DX.DrawGraph(0, 0, imgBack, DX.FALSE);
            Enemies.ForEach(enemy => enemy.Draw());
            OwnBullets.ForEach(bullet => bullet.Draw());
            EnemyBullets.ForEach(bullet => bullet.Draw());
            OwnChar.Draw();
            if (Boss != null)
            {
                Boss.Draw();
                DX.DrawFillBox(bossHpX1, bossHpY1, bossHpX1 + bossHpW, bossHpY1 + bossHpH, bossHpColBack);
                DX.DrawFillBox(bossHpX1, bossHpY1, bossHpX1 + Boss.HP * bossHpW / Boss.MaxHP, bossHpY1 + bossHpH, bossHpCol);
            }
        }

        /// <summary>
        /// ゲームが終了したか否か
        /// </summary>
        public bool Finished
        {
            get { return finished; }
        }

        /// <summary>
        /// 自機
        /// </summary>
        public OwnCharacter OwnChar { get; private set; }

        /// <summary>
        /// 自機の弾
        /// </summary>
        public List<Bullet> OwnBullets { get; private set; }

        /// <summary>
        /// 敵
        /// </summary>
        public List<Enemy> Enemies { get; private set; }

        /// <summary>
        /// 敵の弾
        /// </summary>
        public List<Bullet> EnemyBullets { get; private set; }

        /// <summary>
        /// ボス(出現していないときは null)
        /// </summary>
        public BossEnemy Boss { get; set; }

        /// <summary>
        /// 乱数
        /// </summary>
        public Random Rnd = new Random();

        // キー入力
        private Key key;

        /// <summary>
        /// 2つの円 (円1、円2) が接触しているか否かを計算する。
        /// </summary>
        /// <param name="x1">円1の中心の X 座標</param>
        /// <param name="y1">円1の中心の Y 座標</param>
        /// <param name="r1">円1の半径</param>
        /// <param name="x2">円2の中心の X 座標</param>
        /// <param name="y2">円2の中心の Y 座標</param>
        /// <param name="r2">円2の半径</param>
        /// <returns>2つの円が接触していれば true 、そうでなければ false</returns>
        private bool collidesCircleCircle(double x1, double y1, double r1, double x2, double y2, double r2)
        {
            var dx = x2 - x1;
            var dy = y2 - y1;
            var sr = r1 + r2;
            return dx * dx + dy * dy <= sr * sr;
        }

        // 終了フラグ
        private bool finished = false;

        // 画像
        private int imgBack;

        // ボスの HP 表示領域
        private int bossHpX1 = 4, bossHpY1 = 4, bossHpW = 632, bossHpH = 12;
        private uint bossHpColBack = DX.GetColor(128, 64, 64), bossHpCol = DX.GetColor(255, 0, 0);

        private ScriptRunner runner;
    }
}
