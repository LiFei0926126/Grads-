/**************版本信息**************
 * VerSion :   %clrversion%
 * 文 件 名:   ICTLInfo.cs
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
using Readearth.GrADSBinary.DEF;
namespace Readearth.GrADSBinary
{
    /// <summary>
    /// CTL文件解析类
    /// </summary>
    public interface ICTLInfo
    {
        #region 属性

        /// <summary>
        /// 标识对应数据文件的描述。
        /// </summary>
        string Dset { get; }

        /// <summary>
        /// 控制处理数据文件方式的变量。替换了旧版的FORMAT记录。
        /// </summary>
        IOptions Options { get; }

        /// <summary>
        /// 定义格点数据显示到地图上的投影方式。
        /// </summary>
        IPDef PDef { get; }

        /// <summary>
        /// 定义格点数据的T维格点值。
        /// </summary>
        ITDef TDef { get; }

        /// <summary>
        /// 对数据集的相关描述
        /// </summary>
        string Title { get; }

        /// <summary>
        /// 标识未定义或者缺失数据值。
        /// </summary>
        float UNDef { get; }


        /// <summary>
        /// 变量集合。
        /// </summary>
        IVars Vars { get; }

        /// <summary>
        /// 定义格点数据的x维或者经向格点值。
        /// </summary>
        IXDef XDef { get; }

        /// <summary>
        /// 定义格点数据的y维或者纬向格点值。
        /// </summary>
        IYDef YDef { get; }

        /// <summary>
        /// 定义格点数据的z维格点值。
        /// </summary>
        IZDef ZDef { get; }

        /// <summary>
        /// 定义格点数据的E维格点值。
        /// </summary>
        IEDef EDef { get; }

        /// <summary>
        /// 列数（纬向）
        /// </summary>
        int XSize { get; }

        /// <summary>
        /// 行数（经向）
        /// </summary>
        int YSize { get; }

        /// <summary>
        /// 基础数据块大小（bytes）
        /// </summary>
        int BaseArraySize { get; }

        /// <summary>
        /// CTL文件是否已加载
        /// </summary>
        bool IsCTLFileLoaded { get; }

        /// <summary>
        /// 原点纬度
        /// </summary>
        double Orgin_Lat { get; set; }

        /// <summary>
        /// 时次数据块大小（bytes）
        /// </summary>
        int TimePageSize { get; }

        #endregion

        #region 方法

        /// <summary>
        /// 加载CTL文件。
        /// </summary>
        /// <returns>是否加载成功。</returns>
        bool LoadCTL();

        /// <summary>
        /// 加载CTL文件。
        /// </summary>
        /// <param name="strCTLPath">CTL文件路径</param>
        /// <returns>是否加载成功。</returns>
        bool LoadCTL(string strCTLPath);

        /// <summary>
        /// 根据经纬度计算行列索引号。
        /// </summary>
        /// <param name="lon">经度</param>
        /// <param name="lat">纬度</param>
        /// <returns></returns>
        XYPoint GetXYIndex(double lon , double lat);

        /// <summary>
        /// 获取目标变量的开始位置。
        /// </summary>
        /// <param name="strVarName">变量名</param>
        /// <param name="timeIndex">时间维索引</param>
        /// <param name="level">z维索引</param>
        /// <returns>开始位置</returns>       
        int GetBinaryBlockIndex(string strVarName , int timeIndex , int level);

        /// <summary>
        /// 获取目标变量的开始位置。
        /// </summary>
        /// <param name="varIndex">变量索引</param>
        /// <param name="timeIndex">时间维索引</param>
        /// <param name="level">z维索引</param>
        /// <returns>开始位置</returns>
        int GetBinaryBlockIndex(int varIndex , int timeIndex , int level);
        #endregion

    }
}
