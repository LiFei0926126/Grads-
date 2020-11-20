using System;
using System.Collections.Generic;

namespace Readearth.GrADSBinary.DEF
{
    /// <summary>
    /// This entry defines the grid point values for the X dimension, or longitude.
    /// </summary>
    public class XDefClass : Object, IXDef
    {
        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="strXDEF"></param>
        /// <exception cref="ArgumentNullException">参数错误：该参数不能为空。</exception>
        /// <exception cref="ArgumentNullException">参数错误：该参数不是XDEF描述。</exception>
        /// <exception cref="ArgumentNullException">参数错误：该参数不是 LINEAR XDEF描述。</exception>
        /// <exception cref="ArgumentNullException">参数错误：该参数不是 LEVELS XDEF描述。</exception>
        public XDefClass(string strXDEF)
        {
            if (string.IsNullOrEmpty ( strXDEF ))
                throw new ArgumentNullException ( "strXDEF" , "参数错误：该参数不能为空。" );
            if (!strXDEF.ToUpper ( ).Contains ( "XDEF" ))
                throw new ArgumentException ( "参数错误：该参数不是XDEF描述。" , "strXDEF" , new Exception ( strXDEF ) );

            string[] paras = strXDEF.ToUpper ( ).Split ( ' ' );

            _xnum = int.Parse ( paras[1] );
            _XDEF_Type = (Mapping_Type)Enum.Parse ( typeof ( Mapping_Type ) , paras[2].ToUpper ( ) );

            switch (_XDEF_Type)
            {
                case Mapping_Type.Linear:
                    if (paras.Length == 5)
                    {
                        _start = double.Parse ( paras[3] );
                        _increment = double.Parse ( paras[4] );
                    }
                    else
                        throw new ArgumentException ( "参数错误：该参数不是 LINEAR XDEF描述。" , "strXDEF" , new Exception ( strXDEF ) );
                    break;
                case Mapping_Type.Levels:
                    if (paras.Length >= 4)
                    {
                        _LEVELS = new List<double> ( );
                        for (int i = 3 ; i < paras.Length ; i++)
                        {
                            _LEVELS.Add ( double.Parse ( paras[i] ) );
                        }
                    }
                    else
                        throw new ArgumentException ( "参数错误：该参数不是 LEVELS XDEF描述。" , "strXDEF" , new Exception ( strXDEF ) );
                    break;
            }

        }
        #endregion

        #region 成员变量
        private int _xnum;
        private Mapping_Type _XDEF_Type;
        private double _start = double.NaN, _increment = double.NaN;
        private List<double> _LEVELS = null;
        #endregion

        #region 属性
        /// <summary>
        /// XSize
        /// </summary>
        public int XSize
        {
            get
            {
                if (_xnum >= 1)
                    return _xnum;
                else
                    throw new ArgumentOutOfRangeException ( );
            }
        }
        /// <summary>
        /// XDEFType
        /// </summary>
        public Mapping_Type MappingType => _XDEF_Type;
        /// <summary>
        /// LEVELS
        /// </summary>
        public List<double> Levels
        {
            get
            {
                if (_LEVELS == null)
                    throw new ArgumentUndealException ( );
                else
                    return _LEVELS;
            }
        }
        /// <summary>
        /// STRAT
        /// </summary>
        public double Strat
        {
            get
            {
                if (_start == double.NaN)
                    throw new ArgumentUndealException ( );
                else
                    return _start;
            }
        }

        /// <summary>
        /// INCREMENT
        /// </summary>
        public double Increment
        {
            get
            {
                if (_increment == double.NaN)
                    throw new ArgumentUndealException ( );
                else
                    return _increment;
            }
        }
        #endregion

        #region 公有函数

        #endregion

        #region 私有函数

        #endregion
    }
}
