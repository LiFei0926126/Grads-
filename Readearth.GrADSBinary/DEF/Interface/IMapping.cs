/**************版本信息**************
 * VerSion :   %clrversion%
 * 文 件 名:   IMapping.cs
 * 描    述: 
 * 
 * 版    本：  V1.0
 * 创 建 者：  LiFei
 * 创建时间：  2020/11/20 13:44
 * ======================================
 * 历史更新记录
 * 版本：V          修改时间：         修改人：
 * 修改内容：
 * ======================================
**************版本信息**************/
namespace Readearth.GrADSBinary.DEF
{
    /// <summary>
    /// 映射
    /// </summary>
    public interface IMapping
    {
        /// <summary>
        /// 映射类型
        /// </summary>
        Mapping_Type MappingType { get; }
    }
}