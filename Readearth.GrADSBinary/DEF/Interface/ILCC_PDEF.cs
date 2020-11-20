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
