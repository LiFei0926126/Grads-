/**************版本信息**************
 * 文 件 名:   IEDef.cs
 * 描    述: 
 * 
 * 版    本：  V1.0
 * 创 建 者：  LiFei
 * 创建时间：  2020/11/20 14:38
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
    /// IEDef
    /// </summary>
    public interface IEDef:INames
    {
        /// <summary>
        /// EDef集合成员定义类型
        /// </summary>
        EDef_Type EDef_Type { get; }

        /// <summary>
        /// 数量
        /// </summary>
        int EDefNum { get; }
    }
}