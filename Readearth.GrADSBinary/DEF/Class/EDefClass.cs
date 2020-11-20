using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Readearth.GrADSBinary.DEF
{
    /// <summary>
    /// EDefClass
    /// ----------------
    /// 创 建 者：LiFei
    /// 创建时间：2020/11/20 14:00:06
    /// ----------------
    /// </summary>
    public class EDefClass : IEDef
    {
        /// <summary>
        /// 开始标记的正则表达式
        /// </summary>
        public static string StrRegexBegin = "^EDEF";

        /// <summary>
        /// 结束标记的正则表达式
        /// </summary>
        public static string StrRegexEnd = "^ENDEDEF";

        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="strEDef"></param>
        public EDefClass(string strEDef)
        {
            if (string.IsNullOrEmpty ( strEDef ))
                throw new ArgumentNullException ( "strEDef" , "参数错误：该参数不能为空。" );
            if (!strEDef.ToUpper ( ).Contains ( "EDEF" ))
                throw new ArgumentException ( "参数错误：该参数不是XDEF描述。" , "strEDef" , new Exception ( strEDef ) );

            string[] paras = strEDef.Split ( ' ' );

            EDefNum = int.Parse ( paras[1] );
            EDef_Type = paras[2].ToUpper ( ) == "NAMES" ? EDef_Type.Names : EDef_Type.Extend;

            switch (EDef_Type)
            {
                case EDef_Type.Names:
                    if (Regex.Split ( strEDef , "\\s+" ).Length == EDefNum + 3)
                    {
                        Names = new List<string> ( );
                        for (int i = 3 ; i < paras.Length ; i++)
                        {
                            Names.Add ( paras[i] );
                        }
                    }
                    else
                    {

                        throw new ArgumentException ( "参数错误：该参数不是支持的EDEF描述。" , "strEDef" , new Exception ( strEDef ) );
                    }
                    break;
                case EDef_Type.Extend:
                    throw new Exception ( "暂未支持该类型。" );
            }
        }
        #endregion 构造函数

        #region 成员变量
        private int _enum;
        private EDef_Type _EDef_Type;
        private List<string> _Names = null;
        #endregion 成员变量

        #region 属性
        /// <summary>
        /// 名称列表
        /// </summary>
        public List<string> Names { get => _Names; private set => _Names = value; }

        /// <summary>
        /// 数量
        /// </summary>
        public int EDefNum { get => _enum; private set => _enum = value; }

        /// <summary>
        /// EDef集合成员定义类型
        /// </summary>
        public EDef_Type EDef_Type { get => _EDef_Type; private set => _EDef_Type = value; }
        #endregion 属性

        #region 公有方法
        #endregion 公有方法

        #region 私有方法
        #endregion 私有方法

    }
}
