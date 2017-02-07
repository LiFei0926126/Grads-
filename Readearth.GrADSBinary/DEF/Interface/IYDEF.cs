using System;
namespace Readearth.GrADSBinary.DEF
{
    /// <summary>
    ///YDEF
    /// </summary>
    public interface IYDEF
    {
        /// <summary>
        /// LINEAR 模式的增长值
        /// </summary>
        double INCREMENT { get; }
        /// <summary>
        /// LEVELS 模式下的等级
        /// </summary>
        System.Collections.Generic.List<double> LEVELS { get; }
        /// <summary>
        /// 
        /// </summary>
        double STRAT { get; }
        /// <summary>
        /// 模式
        /// </summary>
        XYZDEF_Type YDEFType { get; }
        /// <summary>
        /// Y维长度
        /// </summary>
        int YSize { get; }
    }
}
