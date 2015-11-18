using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stg.Script
{
    public class Script
    {
        public Script()
        {
            Statements = new List<Statement>();
            Background = "default.bmp";
            BackR = 255;
            BackG = 255;
            BackB = 255;
        }

        public List<Statement> Statements { get; private set; }

        /// <summary>
        /// BGM のファイル名
        /// </summary>
        public string Bgm { get; set; }

        /// <summary>
        /// 背景画像のファイル名
        /// </summary>
        public string Background { get; set; }

        /// <summary>
        /// BGM をループ再生するか否か
        /// </summary>
        public bool BgmLoop { get; set; }

        public enum TimeType
        {
            Frame = 0,
            BgmSample,
            BgmTime,
        }

        /// <summary>
        /// 時間の単位
        /// </summary>
        public TimeType Time { get; set; }

        /// <summary>
        /// 初期背景色の赤色成分
        /// </summary>
        public int BackR { get; set; }

        /// <summary>
        /// 初期背景色の緑色成分
        /// </summary>
        public int BackG { get; set; }

        /// <summary>
        /// 初期背景色の青色成分
        /// </summary>
        public int BackB { get; set; }
    }
}
