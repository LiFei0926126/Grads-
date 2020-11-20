namespace Readearth.GrADSBinary.DEF
{
    /// <summary>
    /// 可选项
    /// </summary>
    public interface IOptions
    {
        /// <summary>
        /// 是否使用模板
        /// </summary>
        bool UseTemplates { get; }
        /// <summary>
        /// 是否进行大小端转换
        /// </summary>
        bool Byteswapped { get; }
    }
}
