using System;
using System.Collections.Generic;
using System.Text;

namespace Readearth.GrADSBinary.DEF
{
    /// <summary>
    /// 
    /// </summary>
    public enum CTL_File_Mode
    {
        /// <summary>
        ///  一对一
        /// 说明：一个CTL文件对应一个数据文件
        /// </summary>
        ONE= 1,
        /// <summary>
        ///  一对多
        ///  说明：一个CTL文件对应多个数据文件
        /// </summary>
        MANY = 10
    }
}
