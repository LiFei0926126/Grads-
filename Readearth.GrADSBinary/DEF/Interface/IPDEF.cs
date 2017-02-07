using System;
namespace Readearth.GrADSBinary.DEF
{
    /// <summary>
    /// 
    /// </summary>
    public interface IPDEF
    {
        /// <summary>
        /// x方向格距（单位为米）
        /// </summary>
        double DX { get; }
        /// <summary>
        ///y方向格距（单位为米）
        /// </summary>
        double DY { get; }
        /// <summary>
        ///参考点的x方向索引
        /// </summary>
        double IRef { get; }
        /// <summary>
        /// y方向本地网格尺寸
        /// </summary>
        int ISize { get; }
        /// <summary>
        /// 参考点的y方向索引
        /// </summary>
        double JRef { get; }
        /// <summary>
        /// 参考点纬度 ('°'为单位。 北为正，南为负。)
        /// </summary>
        double LatRef { get; }
        /// <summary>
        /// 参考点经度 ('°'为单位。 东为正，西为负。)
        /// </summary>
        double LonRef { get; }
        /// <summary>
        /// 北参考纬线
        /// </summary>
        double Ntruelat { get; }
        /// <summary>
        /// 中央经线
        /// </summary>
        double SLon { get; }
        /// <summary>
        /// 南参考纬线
        /// </summary>
        double Struelat { get; }
        /// <summary>
        /// 投影类型
        /// </summary>
        Pro_Type Pro_Type { get; }
        /// <summary>
        ///y方向本地网格尺寸
        /// </summary>
        int JSize { get; }
        /// <summary>
        /// 单层数据大小（X*Y*4 Byte）
        /// </summary>
        long BlockSize { get; }
    }
}
