using System;
namespace Readearth.GrADSBinary.DEF
{
    /// <summary>
    /// 变量集合类
    /// </summary>
    public interface IVars
    {

        /// <summary>
        /// 根据变量索引
        /// </summary>
        /// <param name="varIndex"></param>
        /// <param name="BlockSize"></param>
        /// <returns></returns>
        int GetBinaryBlockIndex(int varIndex , int BlockSize);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="varIndex"></param>
        /// <param name="BlockSize"></param>
        /// <param name="levelIndex"></param>
        /// <returns></returns>
        int GetBinaryBlockIndex(int varIndex , int BlockSize , int levelIndex);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="strVarName"></param>
        /// <param name="BlockSize"></param>
        /// <returns></returns>
        int GetBinaryBlockIndex(string strVarName , int BlockSize);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="strVarName"></param>
        /// <param name="BlockSize"></param>
        /// <param name="level"></param>
        /// <returns></returns>
        int GetBinaryBlockIndex(string strVarName , int BlockSize , int level);

        /// <summary>
        /// 根据变量索引获取变量。
        /// </summary>
        /// <param name="varIndex">变量索引。</param>
        /// <returns></returns>
        VariableClass GetVariableByIndex(int varIndex);

        /// <summary>
        /// 根据变量名获取变量。
        /// </summary>
        /// <param name="strVarName">变量名。</param>
        /// <param name="IgnoreCase">是否忽略大小写。</param>
        /// <returns>返回变量。</returns>
        /// <exception cref="ArgumentException">错误的变量名。</exception>
        VariableClass GetVariableByName(string strVarName , bool IgnoreCase);

        /// <summary>
        /// 变量总数。
        /// </summary>
        int VarCount { get; }

        /// <summary>
        /// 变量集合。
        /// </summary>
        System.Collections.Generic.List<VariableClass> VARS { get; }

        /// <summary>
        /// 时次数据内含有的基础数据块数量
        /// </summary>
        int BlocksCount { get; }
    }
}
