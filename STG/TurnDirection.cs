using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stg
{
    public enum TurnDirection
    {
        /// <summary>
        /// same as Clockwise
        /// </summary>
        Right = 1,

        /// <summary>
        /// same as Right
        /// </summary>
        Clockwise = 1,

        /// <summary>
        /// same as Counterclockwise
        /// </summary>
        Left = -1,

        /// <summary>
        /// same as Left
        /// </summary>
        Counterclockwise = -1,
    }
}
