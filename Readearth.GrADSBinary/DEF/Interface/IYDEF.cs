using System.Collections.Generic;

namespace Readearth.GrADSBinary.DEF
{
    /// <summary>
    ///YDEF
    /// </summary>
    public interface IYDef: ILinear, ILevels,IMapping
    {
        /// <summary>
        /// Y维长度
        /// </summary>
        int YSize { get; }
    }
}
