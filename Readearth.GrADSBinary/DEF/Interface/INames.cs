/**************版本信息**************
 * VerSion :   %clrversion%
 * 文 件 名:   INames.cs
 * 描    述: 
 * 
 * 版    本：  V1.0
 * 创 建 者：  LiFei
 * 创建时间：  2020/11/20 14:39
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
    /// EDEF 的名称枚举模式
    /// </summary>
    public interface INames
    {
        /// <summary>
        /// 名称列表
        /// </summary>
        List<string> Names { get; }
    }
}