/**************版本信息**************
 * 文 件 名:   ILcc_PDef.cs
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
    /// LCC 投影信息
    /// </summary>
    public interface ILcc_PDef : IPDef
    {
        /// <summary>
        /// grid X increment in meters
        /// </summary>
        double DX { get; }

        /// <summary>
        /// grid Y increment in meters
        /// </summary>
        double DY { get; }

        /// <summary>
        /// i of ref point
        /// </summary>
        double IRef { get; }

        /// <summary>
        /// j of ref point
        /// </summary>
        double JRef { get; }

        /// <summary>
        /// reference latitude
        /// </summary>
        double LatRef { get; }

        /// <summary>
        /// reference longitude (in degrees, E is positive, W is negative)
        /// </summary>
        double LonRef { get; }

        /// <summary>
        /// N true lat
        /// </summary>
        double Ntruelat { get; }

        /// <summary>
        /// standard longitude
        /// </summary>
        double SLon { get; }

        /// <summary>
        /// S true lat
        /// </summary>
        double Struelat { get; }

        /// <summary>
        /// 单层数据大小（X*Y*4 Byte）
        /// </summary>
        long BlockSize { get; }

    }
}
