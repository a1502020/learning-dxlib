using DxLibDLL;
using Stg.Scenes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stg
{
    class StgMain
    {
        public static void Main(string[] args)
        {
            // 初期化(initialization)
            DX.SetMainWindowText("Shooting");
            DX.ChangeWindowMode(DX.TRUE);
            DX.DxLib_Init();
            DX.SetDrawScreen(DX.DX_SCREEN_BACK);

            // タイトル画面
            Scene scene = new TitleScene();

            // メインループ
            while (DX.ProcessMessage() == 0 && scene != null)
            {
                scene.Update();
                scene.Draw();
                DX.ScreenFlip();
                scene = scene.NextScene;
            }

            // 終了処理(finalization)
            DX.DxLib_End();
        }
    }
}
