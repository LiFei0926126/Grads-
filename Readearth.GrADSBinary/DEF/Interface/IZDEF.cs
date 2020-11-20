/**************版本信息**************
 * 文 件 名:   IZDef.cs
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
    /// 定义格点数据的z维格点值。
    /// </summary>
    public interface IZDef: ILinear, ILevels,IMapping
    {
        
        /// <summary>
        /// Z维长度
        /// </summary>
        int ZSize { get; }
    }
}
