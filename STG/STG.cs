using DxLibDLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STG
{
    class STG
    {
        public static void Main(string[] args)
        {
            // 初期化(initialization)
            DX.SetMainWindowText("Shooting");
            DX.ChangeWindowMode(DX.TRUE);
            DX.SetDrawScreen(DX.DX_SCREEN_BACK);
            DX.DxLib_Init();

            // 乱数
            var rnd = new Random();

            // 自機
            var ownChar = new OwnCharacter(new Position(320.0, 240.0), 16, DX.GetColor(255, 0, 0));

            // 自機の弾
            var ownBullets = new List<Bullet>();

            // 敵機
            var enemies = new List<Enemy>();

            // キー入力用バッファ
            var keys = new byte[256];

            // メインループ
            while (DX.ProcessMessage() == 0)
            {
                // キー入力情報取得
                DX.GetHitKeyStateAll(out keys[0]);

                // 自機の処理
                ownChar.Update(keys);

                // ランダムで敵出現
                if (rnd.Next(60) == 0)
                {
                    enemies.Add(new Enemy());
                }

                // 敵の処理
                foreach (var enemy in enemies)
                {
                    enemy.Update();
                }

                // 弾を発射
                if (keys[DX.KEY_INPUT_SPACE] != 0)
                {
                    ownBullets.Add(new Bullet(ownChar.Position, 5, 3 * Math.PI / 2, 10.0, DX.GetColor(255, 255, 0)));
                }

                // 画面を黒で塗りつぶし
                DX.DrawFillBox(0, 0, 640, 480, DX.GetColor(0, 0, 0));

                // 敵の描画
                foreach (var enemy in enemies)
                {
                    enemy.Draw();
                }

                // 弾の描画と移動
                foreach (var bullet in ownBullets)
                {
                    DX.DrawCircle((int)bullet.Position.X, (int)bullet.Position.Y, bullet.Radius, bullet.Color);
                    bullet.Position.X += bullet.Speed * Math.Cos(bullet.Angle);
                    bullet.Position.Y += bullet.Speed * Math.Sin(bullet.Angle);
                }

                // 画面外の弾を削除
                ownBullets.RemoveAll(bullet =>
                    bullet.Position.X < -bullet.Radius
                    || bullet.Position.Y < -bullet.Radius
                    || bullet.Position.X > 640 + bullet.Radius
                    || bullet.Position.Y > 480 + bullet.Radius);

                // 自機を描画
                ownChar.Draw();

                DX.ScreenFlip();
            }

            // 終了処理(finalization)
            DX.DxLib_End();
        }
    }
}
