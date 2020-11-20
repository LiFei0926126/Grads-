/**************版本信息**************
 * 文 件 名:   IPDef.cs
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
    /// 投影信息
    /// </summary>
    public interface IPDef
    {
        /// <summary>
        /// 投影类型
        /// </summary>
        Pro_Type Pro_Type { get; }
        /// <summary>
        ///x方向本地网格尺寸
        /// </summary>
        int ISize { get; }

        /// <summary>
        ///y方向本地网格尺寸
        /// </summary>
        int JSize { get; }
    }
}
