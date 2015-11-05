using DxLibDLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STG
{
    class ShootingGame
    {
        public ShootingGame()
        {
        }

        /// <summary>
        /// 1フレームぶんの処理を行う。
        /// </summary>
        public void Update()
        {
            DX.GetHitKeyStateAll(out keys[0]);

            // 自機
            ownChar.Update(keys);

            // 敵
            if (rnd.Next(60) == 0)
            {
                enemies.Add(new Enemy());
            }
            enemies.ForEach(enemy => enemy.Update(enemyBullets, ownChar.Position));

            // 自機の弾
            if (bulletFrame == 0 && keys[DX.KEY_INPUT_SPACE] != 0)
            {
                ownBullets.Add(new Bullet(ownChar.Position, 5, 3 * Math.PI / 2, 10.0, DX.GetColor(255, 255, 0)));
                bulletFrame = 12;
            }
            if (bulletFrame > 0)
            {
                --bulletFrame;
            }
            ownBullets.ForEach(bullet => bullet.Update());

            // 敵の弾
            enemyBullets.ForEach(bullet => bullet.Update());

            // 画面外の弾を削除
            ownBullets.RemoveAll(bullet =>
                bullet.Position.X < -bullet.Radius
                || bullet.Position.Y < -bullet.Radius
                || bullet.Position.X > 640 + bullet.Radius
                || bullet.Position.Y > 480 + bullet.Radius);
            enemyBullets.RemoveAll(bullet =>
                bullet.Position.X < -bullet.Radius
                || bullet.Position.Y < -bullet.Radius
                || bullet.Position.X > 640 + bullet.Radius
                || bullet.Position.Y > 480 + bullet.Radius);

            // 敵と弾の当たり判定
            for (var bi = ownBullets.Count - 1; bi >= 0; --bi)
            {
                var bullet = ownBullets[bi];
                for (var ei = enemies.Count - 1; ei >= 0; --ei)
                {
                    var enemy = enemies[ei];
                    if (collidesCircleCircle(
                        bullet.Position.X, bullet.Position.Y, bullet.Radius,
                        enemy.Position.X, enemy.Position.Y, enemy.Radius))
                    {
                        // 接触している
                        ownBullets.RemoveAt(bi);
                        enemies.RemoveAt(ei);
                    }
                }
            }

            // 自機と敵の当たり判定
            foreach (var enemy in enemies)
            {
                if (collidesCircleCircle(
                    ownChar.Position.X, ownChar.Position.Y, ownChar.Radius,
                    enemy.Position.X, enemy.Position.Y, enemy.Radius))
                {
                    // 接触している
                    // まだ何もない
                }
            }

            // 自機と敵の弾の当たり判定
            foreach (var bullet in enemyBullets)
            {
                if (collidesCircleCircle(
                    ownChar.Position.X, ownChar.Position.Y, ownChar.Radius,
                    bullet.Position.X, bullet.Position.Y, bullet.Radius))
                {
                    // 接触している
                    // まだ何もない
                }
            }
        }

        /// <summary>
        /// 画面を描画する。
        /// </summary>
        public void Draw()
        {
            DX.DrawFillBox(0, 0, 640, 480, DX.GetColor(0, 0, 0));
            enemies.ForEach(enemy => enemy.Draw());
            ownBullets.ForEach(bullet => bullet.Draw());
            enemyBullets.ForEach(bullet => bullet.Draw());
            ownChar.Draw();
        }

        /// <summary>
        /// ゲームが終了したか否か
        /// </summary>
        public bool Finished
        {
            get { return false; }
        }

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

        private Random rnd = new Random();

        // 自機
        private OwnCharacter ownChar = new OwnCharacter(new Position(320.0, 240.0), 16, DX.GetColor(255, 0, 0));

        // 自機の弾
        private List<Bullet> ownBullets = new List<Bullet>();

        // 敵
        private List<Enemy> enemies = new List<Enemy>();

        // 敵の弾
        private List<Bullet> enemyBullets = new List<Bullet>();

        // キー入力用バッファ
        private byte[] keys = new byte[256];

        // 弾の発射間隔制御用カウンタ
        private int bulletFrame = 0;
    }
}
