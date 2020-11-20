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
