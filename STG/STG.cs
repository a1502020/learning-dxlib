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

            // 自機
            var ownPos = new Position(320.0, 240.0);
            var ownRadius = 16;
            var ownColor = DX.GetColor(255, 0, 0);
            var ownSpeed = 5.0;

            // キー入力用バッファ
            var keys = new byte[256];

            // メインループ
            while (DX.ProcessMessage() == 0)
            {
                // キー入力情報取得
                DX.GetHitKeyStateAll(out keys[0]);

                // 移動(斜め移動が速くなるのは気にしないことにした)
                if (keys[DX.KEY_INPUT_W] != 0)
                {
                    ownPos.Y -= ownSpeed;
                }
                if (keys[DX.KEY_INPUT_A] != 0)
                {
                    ownPos.X -= ownSpeed;
                }
                if (keys[DX.KEY_INPUT_S] != 0)
                {
                    ownPos.Y += ownSpeed;
                }
                if (keys[DX.KEY_INPUT_D] != 0)
                {
                    ownPos.X += ownSpeed;
                }

                // 画面外に行かないようにする
                if (ownPos.X < ownRadius)
                {
                    ownPos.X = ownRadius;
                }
                if (ownPos.Y < ownRadius)
                {
                    ownPos.Y = ownRadius;
                }
                if (ownPos.X > 640 - ownRadius)
                {
                    ownPos.X = 640 - ownRadius;
                }
                if (ownPos.Y > 480 - ownRadius)
                {
                    ownPos.Y = 480 - ownRadius;
                }

                // 画面を黒で塗りつぶし
                DX.DrawFillBox(0, 0, 640, 480, DX.GetColor(0, 0, 0));

                // 自機を描画
                DX.DrawCircle((int)ownPos.X, (int)ownPos.Y, ownRadius, ownColor);

                DX.ScreenFlip();
            }

            // 終了処理(finalization)
            DX.DxLib_End();
        }
    }
}
