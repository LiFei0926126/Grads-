using System;
namespace Readearth.GrADSBinary.DEF
{
    /// <summary>
    /// 
    /// </summary>
    interface ILCC_PDEF
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
        /// The size of the native grid in the x direction
        /// </summary>
        int ISize { get; }
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
        /// The size of the native grid in the y direction
        /// </summary>
        int JSize { get; }
        /// <summary>
        /// 单层数据大小（X*Y*4 Byte）
        /// </summary>
        long BlockSize { get; }

    }
}
