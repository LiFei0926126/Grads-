/**************版本信息**************
 * 文 件 名:   IYDef.cs
 * 描    述: 
 * 
 * 版    本：  V1.0
 * 创 建 者：  LiFei
 * 创建时间：  2019/4/29 18:39
 * ======================================
 * 历史更新记录
 * 版本：V          修改时间：         修改人：
 * 修改内容：
 * ======================================
**************版本信息**************/
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
