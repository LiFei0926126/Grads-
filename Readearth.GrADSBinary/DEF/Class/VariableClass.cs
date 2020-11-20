using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Readearth.GrADSBinary.DEF
{
    /// <summary>
    /// 变量类
    /// </summary>
    public class VariableClass
    {
        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="strInit"></param>
        /// <param name="varIndex"></param>
        public VariableClass(string strInit , int varIndex)
        {
            _varIndex = varIndex;

            List<string> additional = new List<string> ( );
            List<string> listParas = new List<string> ( Regex.Split ( strInit , "\\s+" ) );
            for (int i = 0 ; i < listParas.Count ; i++)
            {
                if (i == 0)
                    _varName = listParas[i];
                else if (i == 1)
                    _levels = int.Parse ( listParas[i] );
                else if (i == (listParas.Count - 2))
                    _description = listParas[i];
                else if (i == (listParas.Count - 1))
                    _uint = listParas[i];
                else
                    additional.Add ( listParas[i] );
            }
            _additional_codes = string.Join ( "," , additional.ToArray ( ) );
        }

        #endregion 构造函数

        #region 成员变量

        private string _varName = string.Empty;
        private int _levels = int.MinValue;
        private int _varIndex = int.MinValue;
        private string _description = string.Empty;
        private string _uint = string.Empty;
        private string _additional_codes = string.Empty;

        #endregion 成员变量

        #region 属性

        /// <summary>
        /// 变量名
        /// </summary>
        public string VarName
        {
            get
            {
                if (string.IsNullOrEmpty ( _varName ))
                    throw new ArgumentNullException ( );
                else
                    return _varName;
            }
        }

        /// <summary>
        /// z维长度
        /// </summary>
        public int LevelCount
        {
            get
            {
                if (_levels == int.MinValue)
                    throw new ArgumentNullException ( );
                else
                    return _levels;
            }
        }

        /// <summary>
        /// 变量描述
        /// </summary>
        public string Description
        {
            get
            {
                if (string.IsNullOrEmpty ( _description ))
                    throw new ArgumentNullException ( );
                else
                    return _description;
            }
        }

        /// <summary>
        /// 变量单位
        /// </summary>
        public string StrUint
        {
            get
            {
                if (string.IsNullOrEmpty ( _uint ))
                    throw new ArgumentNullException ( );
                else
                    return _uint;
            }
        }

        /// <summary>
        /// 可选条件
        /// </summary>
        public string Additional_Codes
        {
            get
            {
                if (string.IsNullOrEmpty ( _additional_codes ))
                    throw new ArgumentNullException ( );
                else
                    return _additional_codes;
            }
        }

        /// <summary>
        /// 变量索引
        /// </summary>
        public int VarIndex
        {
            get
            {
                if (_varIndex == int.MinValue)
                    throw new ArgumentNullException ( );
                else
                    return _varIndex;
            }
        }

        #endregion 属性
    }
}