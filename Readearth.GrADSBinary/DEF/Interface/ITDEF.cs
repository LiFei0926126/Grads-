using System;
using System.Collections.Generic;

namespace Readearth.GrADSBinary.DEF
{
    /// <summary>
    /// 时间维定义
    /// </summary>
   public interface ITDEF
    {
        /// <summary>
        /// 时间维增量
        /// </summary>
        int INCREMENT { get; }
        /// <summary>
        /// 时间维起始时间（北京时）
        /// </summary>
        DateTime BJT_STRATTIME { get; }
        /// <summary>
        /// 时间维起始时间（世界时）
        /// </summary>
        DateTime UTC_STRATTIME { get; }
        /// <summary>
        /// 预报时间列表（世界时）
        /// </summary>
        List<DateTime> UTC_ForecastTimes { get; }
            /// <summary>
            /// 预报时间列表（北京时）
            /// </summary>
        List<DateTime> BJT_ForecastTimes { get; }
        /// <summary>
        /// 时间维长度
        /// </summary>
        int TSize { get; }
    }
}
