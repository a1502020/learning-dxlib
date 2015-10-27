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
            DX.ChangeWindowMode(DX.TRUE);
            DX.DxLib_Init();

            while (DX.ProcessMessage() == 0)
            {
            }

            DX.DxLib_End();
        }
    }
}
