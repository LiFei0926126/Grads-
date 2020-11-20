using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Readearth.GrADSBinary.DEF
{
    /// <summary>
    /// 变量集合类
    /// </summary>
    public class VarsClass : object, IVars
    {
        /// <summary>
        /// 开始标记的正则表达式
        /// </summary>
        public static string StrRegexBegin = "^VARS";

        /// <summary>
        /// 结束标记的正则表达式
        /// </summary>
        public static string StrRegexEnd = "^ENDVARS";

        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="strVARS">变量描述文本</param>
        /// <exception cref="ArgumentNullException">参数为空异常。</exception>
        /// <exception cref="ArgumentException">参数不是VARS描述。</exception>
        /// <exception cref="ArgumentUndealException">未能正确解析变量。</exception>
        public VarsClass(string strVARS)
        {
            if (string.IsNullOrEmpty ( strVARS ))
                throw new ArgumentNullException ( "strVARS" , "参数错误：该参数不能为空。" );
            if (!strVARS.ToUpper ( ).Contains ( "VARS" ))
                throw new ArgumentException ( "参数错误：该参数不是VARS描述。" , "strVARS" , new Exception ( strVARS ) );

            string[] paras = strVARS.Split ( new string[] { "\r\n" , "\n" } , StringSplitOptions.None );
            List<string> listParas = new List<string> ( paras );

            while (string.IsNullOrEmpty ( listParas[listParas.Count - 1] ))
                listParas.RemoveAt ( listParas.Count - 1 );

            _vnum = int.Parse ( Regex.Split ( listParas[0] , "\\s+" )[1] );

            _vars = new List<VariableClass> ( );
            for (int i = 1 ; i < listParas.Count - 1 ; i++)
            {
                _vars.Add ( new VariableClass ( listParas[i] , i - 1 ) );
            }

            if (_vnum != _vars.Count)
                throw new ArgumentUndealException ( "参数错误：未能正确解析变量。" );

        }
        #endregion

        #region 成员变量

        private int _vnum;
        private List<VariableClass> _vars = null;
        #endregion

        #region 属性
        /// <summary>
        /// 变量集合。
        /// </summary>
        public List<VariableClass> VARS
        {
            get
            {
                if (_vars == null)
                    throw new ArgumentNullException ( );
                else
                    return _vars;
            }
        }

        /// <summary>
        /// 变量总数。
        /// </summary>
        public int VarCount
        {
            get
            {
                if (_vnum == int.MinValue)
                    throw new ArgumentException ( );
                else
                    return _vnum;
            }
        }

        /// <summary>
        /// 时次数据内含有的基础数据块数量
        /// </summary>
        public int BlocksCount
        {
            get
            {
                if (_vars == null)
                    throw new Exception ( "请加载正确的CTL文件。" );
                else
                {
                    int _count = 0;
                    for (int i = 0 ; i < _vars.Count ; i++)
                    {
                        VariableClass var = _vars[i];
                        _count += var.LevelCount;
                    }
                    return _count;
                }
            }
        }
        #endregion

        #region 公有函数
        /// <summary>
        /// 根据变量名获取变量。
        /// </summary>
        /// <param name="strVarName">变量名。</param>
        /// <param name="IgnoreCase">是否忽略大小写。</param>
        /// <returns>返回变量。</returns>
        /// <exception cref="ArgumentException">错误的变量名。</exception>
        public VariableClass GetVariableByName(string strVarName , bool IgnoreCase)
        {
            VariableClass pVariableClass = null;
            if (IgnoreCase)
            {
                for (int i = 0 ; i < _vars.Count ; i++)
                {
                    pVariableClass = _vars[i];
                    if (pVariableClass.VarName.ToUpper ( ) == strVarName.ToUpper ( ))
                        break;
                }
            }
            else
            {
                for (int i = 0 ; i < _vars.Count ; i++)
                {
                    pVariableClass = _vars[i];
                    if (pVariableClass.VarName == strVarName)
                        break;
                }
            }
            if (pVariableClass == null)
                throw new ArgumentException ( "参数错误：错误的变量名。" );
            else
                return pVariableClass;
        }
        /// <summary>
        /// 根据变量索引获取变量。
        /// </summary>
        /// <param name="varIndex">变量索引。</param>
        /// <returns></returns>
        public VariableClass GetVariableByIndex(int varIndex)
        {
            if (_vars.Count <= 0)
                throw new ArgumentException ( "参数错误：错误的变量名。" );
            else
                return _vars[varIndex];
        }
        /// <summary>
        /// 获取目标变量的开始位置。
        /// </summary>
        /// <param name="strVarName">变量名</param>
        /// <param name="BlockSize">基础数据块大小</param>
        /// <returns></returns>
        public int GetBinaryBlockIndex(string strVarName , int BlockSize)
        {
            int pBinaryBlockIndex = 0;
            for (int i = 0 ; i < _vars.Count ; i++)
            {
                VariableClass pVariableClass = _vars[i];
                if (pVariableClass.VarName == strVarName)
                    break;
                else
                    pBinaryBlockIndex += pVariableClass.LevelCount * BlockSize;
            }
            return pBinaryBlockIndex;
        }
        /// <summary>
        /// 获取目标变量的开始位置。
        /// </summary>
        /// <param name="varIndex"></param>
        /// <param name="BlockSize"></param>
        /// <returns></returns>
        public int GetBinaryBlockIndex(int varIndex , int BlockSize)
        {
            int pBinaryBlockIndex = 0;
            VariableClass pVariableClass = null;
            for (int i = 0 ; i < varIndex ; i++)
            {
                pVariableClass = _vars[i];
                pBinaryBlockIndex += pVariableClass.LevelCount * BlockSize;
            }
            pVariableClass = _vars[varIndex];
            return pBinaryBlockIndex;
        }
        /// <summary>
        /// 获取目标变量的开始位置。
        /// </summary>
        /// <param name="strVarName"></param>
        /// <param name="BlockSize"></param>
        /// <param name="level"></param>
        /// <returns></returns>
        public int GetBinaryBlockIndex(string strVarName , int BlockSize , int level)
        {
            int pBinaryBlockIndex = 0;
            VariableClass pVariableClass = null;
            for (int i = 0 ; i < _vars.Count ; i++)
            {
                pVariableClass = _vars[i];
                if (pVariableClass.VarName == strVarName)
                    break;
                else
                    pBinaryBlockIndex += pVariableClass.LevelCount * BlockSize;
            }
            pBinaryBlockIndex += level * BlockSize;
            return pBinaryBlockIndex;
        }
        /// <summary>
        /// 获取目标变量的开始位置。
        /// </summary>
        /// <param name="varIndex"></param>
        /// <param name="BlockSize"></param>
        /// <param name="levelIndex"></param>
        /// <returns></returns>
        public int GetBinaryBlockIndex(int varIndex , int BlockSize , int levelIndex)
        {

            int pBinaryBlockIndex = 0;
            VariableClass pVariableClass = null;
            for (int i = 0 ; i < varIndex ; i++)
            {
                pVariableClass = _vars[i];
                pBinaryBlockIndex += pVariableClass.LevelCount * BlockSize;
            }
            pVariableClass = _vars[varIndex];
            if (levelIndex >= pVariableClass.LevelCount)
                throw new ArgumentOutOfRangeException ( "levelIndex" , levelIndex , "参数错误：levelIndex超出当前变量的LEVELS。" );
            pBinaryBlockIndex += levelIndex * BlockSize;
            return pBinaryBlockIndex;
        }
        #endregion
    }
}
