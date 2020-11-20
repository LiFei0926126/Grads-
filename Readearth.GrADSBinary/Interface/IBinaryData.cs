/**************版本信息**************
 * 文 件 名:   IBinaryData.cs
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
using System;

namespace Readearth.GrADSBinary
{
    /// <summary>
    /// 二进制文件操作类接口
    /// </summary>
    public interface IBinaryData
    {
        /// <summary>
        /// CTL文件描述信息
        /// </summary>
        ICTLInfo CTLInfo { get; set; }
        /// <summary>
        /// 数据验证结果
        /// </summary>
        bool VerifyDataResult { get; }
        /// <summary>
        /// 输出到ASC文件。
        /// </summary>
        /// <param name="data">变量数组。</param>
        /// <param name="outputPath">输出文件完整路径。</param>
        /// <param name="isPrjWith">是否输出PRJ文件。</param>
        /// <returns>返回输出是否成功。</returns>
        /// <exception cref="Exception">异常</exception>
        bool Export2ASC(float[,] data , string outputPath , bool isPrjWith);

        /// <summary>
        /// 输出到ASC文件。
        /// </summary>
        /// <param name="VarName">包含变量名的表达式语句。</param>
        /// <param name="timeIndex">时间维索引</param>
        /// <param name="Level">Z维索引。</param>
        /// <param name="outputPath">输出文件完整路径。</param>
        /// <param name="isPrjWith">是否输出PRJ文件。</param>
        /// <returns>返回输出是否成功。</returns>
        /// <exception cref="Exception">异常</exception>
        bool Export2ASC(string VarName , int timeIndex , int Level , string outputPath , bool isPrjWith);
        /// <summary>
        /// 读取数据
        /// </summary>
        /// <param name="VarName">要素</param>
        /// <returns>返回数据数组</returns>
        float[,,,] ReadData(string VarName);
        /// <summary>
        /// 读取数据
        /// </summary>
        /// <param name="VarName">要素</param>
        ///  <param name="timeIndex">时间维索引</param>
        /// <returns>返回数据数组</returns>
        float[,,] ReadData(string VarName , int timeIndex);
        /// <summary>
        /// 读取数据
        /// </summary>
        /// <param name="VarName">要素</param>
        /// <param name="timeIndex">时间维索引</param>
        /// <param name="Level">层次</param>
        /// <returns>返回数据数组</returns>
        float[,] ReadData(string VarName , int timeIndex , int Level);
        /// <summary>
        /// 读取数据
        /// </summary>
        /// <param name="VarName">要素</param>
        /// <param name="timeIndex">时间维索引</param>
        /// <param name="Level">层次</param>
        /// <param name="iIndex">列索引</param>
        /// <param name="jIndex">行索引</param>
        /// <returns>返回数据</returns>
        float ReadData(string VarName , int timeIndex , int Level , int iIndex , int jIndex);
        /// <summary>
        /// 读取数据
        /// </summary>
        /// <param name="VarName">要素</param>
        /// <param name="timeIndex">时间维索引</param>
        /// <param name="Level">层次</param>
        /// <param name="lon">经度</param>
        /// <param name="lat">纬度</param>
        /// <returns>返回数据</returns>
        float ReadData(string VarName , int timeIndex , int Level , double lon , double lat);
        /// <summary>
        /// 验证数据
        /// </summary>
        /// <returns>返回验证结果</returns>
        bool VerifyData();
    }
}