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
