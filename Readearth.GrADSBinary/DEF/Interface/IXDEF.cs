namespace Readearth.GrADSBinary.DEF
{
    /// <summary>
    ///XDEF xnum mapping
    /// </summary>
    public interface IXDef:ILinear,ILevels, IMapping
    {
        
        /// <summary>
        /// X维长度
        /// </summary>
        int XSize { get; }
    }
}
