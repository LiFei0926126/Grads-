/* XDEF xnum mapping <additional arguments>    
 * This entry defines the grid point values for the X dimension, or longitude. The first argument, xnum, specifies the number of grid points in the X direction.
 * xnum must be an integer >= 1. mapping defines the method by which longitudes are assigned to X grid points. There are two options for mapping:
    
 * LINEAR    Linear mapping 
 * LEVELS    Longitudes specified individually
    
 * The LINEAR mapping method requires two additional arguments: start and increment. start is a floating point value that indicates the longitude at grid point X=1. 
 * Negative values indicate western longitudes. increment is the spacing between grid point values, given as a positive floating point value.

 * The LEVELS mapping method requires one additional argument, value-list, which explicitly specifies the longitude value for each grid point.
 * value-list should contain xnum floating point values. It may continue into the next record in the descriptor file, but note that records may not have more than 255 characters. 
 * There must be at least 2 levels in value-list; otherwise use the LINEAR method.

 * Here are some examples:

 * XDEF	144	LINEAR	0.0 2.5
 * XDEF	72	LINEAR	0.0 5.0
 * XDEF	12	LEVELS	0 30 60 90 120 150 180 210 240 270 300 330
 * XDEF	12	LEVELS	15 45 75 105 135 165 195 225 255 285 315 345
 */

using System;
namespace Readearth.GrADSBinary.DEF
{
    /// <summary>
    ///XDEF xnum mapping
    /// </summary>
    public interface IXDEF
    {
        /// <summary>
        /// LEVELS 模式下的等级
        /// </summary>
        System.Collections.Generic.List<double> LEVELS { get; }
        /// <summary>
        /// LINEAR 模式的起始值
        /// </summary>
        double STRAT { get; }
        /// <summary>
        /// LINEAR 模式的增长值
        /// </summary>
        double INCREMENT { get; }
        /// <summary>
        /// 模式
        /// </summary>
        XYZDEF_Type XDEFType { get; }
        /// <summary>
        /// X维长度
        /// </summary>
        int XSize { get; }
    }
}
