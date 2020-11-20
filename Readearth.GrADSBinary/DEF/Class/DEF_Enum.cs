namespace Readearth.GrADSBinary.DEF
{
    /// <summary>
    ///投影类型 
    /// </summary>
    public enum Pro_Type
    {
        /// <summary>
        /// 北半球极坐标投影
        /// (该版本类库暂不支持！)
        /// </summary>
        NPS = 1,
        /// <summary>
        /// 南半球极坐标投影
        /// (该版本类库暂不支持！)
        /// </summary>
        SPS = 2,
        /// <summary>
        /// 兰伯特投影（风矢量的方向以格点相关方向）
        /// </summary>
        LCCR = 4,
        /// <summary>
        /// 兰伯特投影（风矢量的方向以地理相关方向）
        /// </summary>
        LCC = 8,
        /// <summary>
        /// 埃塔模型本地网格
        /// (该版本类库暂不支持！)
        /// </summary>
        ETA_U = 16,
        /// <summary>
        /// 极地立体投影。风旋转并没有实现! ! !只使用标量字段。
        /// </summary>
        PSE = 32,
        /// <summary>
        /// CSU公羊模型使用一个斜极立体投影。风旋转并没有实现! ! !只使用标量字段。
        /// (该版本类库暂不支持！)
        /// </summary>
        OPS = 64,
        /// <summary>
        /// (该版本类库暂不支持！)
        /// </summary>
        ROTLL = 128
    }
    /// <summary>
    /// 映射类型
    /// </summary>
    public enum Mapping_Type
    {
        /// <summary>
        /// 线性映射
        /// </summary>
        Linear = 1,
        /// <summary>
        /// 分别指定
        /// </summary>
        Levels = 2
    }
    /// <summary>
    /// 时间单位
    /// </summary>
    public enum TDef_Unit
    {
        /// <summary>
        /// 分钟
        /// </summary>
        MN,
        /// <summary>
        /// 小时
        /// </summary>
        HR,
        /// <summary>
        /// 天
        /// </summary>
        DY,
        /// <summary>
        /// 月
        /// </summary>
        MO,
        /// <summary>
        /// 年
        /// </summary>
        YR
    }

    /// <summary>
    /// EDef集合成员定义类型
    /// </summary>
    public enum EDef_Type
    {
        /// <summary>
        /// 枚举名字
        /// </summary>
        Names = 1,

        /// <summary>
        /// 扩展形式
        /// </summary>
        Extend = 2
    }
}
