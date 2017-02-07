using System;
namespace Readearth.GrADSBinary.DEF
{
    /// <summary>
    /// 定义格点数据的z维格点值。
    /// </summary>
    public interface IZDEF
    {
        /// <summary>
        /// 增长量
        /// </summary>
        double INCREMENT { get; }

        /// <summary>
        /// levels列表
        /// </summary>
        System.Collections.Generic.List<double> LEVELS { get; }
        /// <summary>
        /// 起始值
        /// </summary>
        double STRAT { get; }
        /// <summary>
        /// ZDEF类型
        /// </summary>
        XYZDEF_Type ZDEFType { get; }
        /// <summary>
        /// Z维长度
        /// </summary>
        int ZSize { get; }
    }
}
