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

            var rnd = new Random();

            var ownChar = new OwnCharacter(new Position(320.0, 240.0), 16, DX.GetColor(255, 0, 0));
            var ownBullets = new List<Bullet>();
            var enemies = new List<Enemy>();

            // キー入力用バッファ
            var keys = new byte[256];

            // メインループ
            while (DX.ProcessMessage() == 0)
            {
                DX.GetHitKeyStateAll(out keys[0]);

                // 自機
                ownChar.Update(keys);

                // 敵
                if (rnd.Next(60) == 0)
                {
                    enemies.Add(new Enemy());
                }
                enemies.ForEach(enemy => enemy.Update());

                // 弾
                if (keys[DX.KEY_INPUT_SPACE] != 0)
                {
                    ownBullets.Add(new Bullet(ownChar.Position, 5, 3 * Math.PI / 2, 10.0, DX.GetColor(255, 255, 0)));
                }
                ownBullets.ForEach(bullet => bullet.Update());

                // 画面外の弾を削除
                ownBullets.RemoveAll(bullet =>
                    bullet.Position.X < -bullet.Radius
                    || bullet.Position.Y < -bullet.Radius
                    || bullet.Position.X > 640 + bullet.Radius
                    || bullet.Position.Y > 480 + bullet.Radius);

                // 描画
                DX.DrawFillBox(0, 0, 640, 480, DX.GetColor(0, 0, 0));
                enemies.ForEach(enemy => enemy.Draw());
                ownBullets.ForEach(bullet => bullet.Draw());
                ownChar.Draw();

                DX.ScreenFlip();
            }

            // 終了処理(finalization)
            DX.DxLib_End();
        }
    }
}
