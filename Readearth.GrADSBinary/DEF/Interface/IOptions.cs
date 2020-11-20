/**************版本信息**************
 * 文 件 名:   IOptions.cs
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
namespace Readearth.GrADSBinary.DEF
{
    /// <summary>
    /// 可选项
    /// </summary>
    public interface IOptions
    {
        /// <summary>
        /// 是否使用模板
        /// </summary>
        bool UseTemplates { get; }
        /// <summary>
        /// 是否进行大小端转换
        /// </summary>
        bool Byteswapped { get; }
    }
}
