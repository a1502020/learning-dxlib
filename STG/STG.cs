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
            DX.DxLib_Init();
            DX.SetDrawScreen(DX.DX_SCREEN_BACK);

            // ゲーム本体
            var game = new ShootingGame();

            // メインループ
            while (DX.ProcessMessage() == 0 && !game.Finished)
            {
                game.Update();
                game.Draw();
                DX.ScreenFlip();
            }

            // 終了処理(finalization)
            DX.DxLib_End();
        }
    }
}
