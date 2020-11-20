using System;
using System.Collections.Generic;

namespace Readearth.GrADSBinary.DEF
{
    /// <summary>
    /// This entry defines the grid point values for the Y dimension, or latitude.
    /// </summary>
    public class YDefClass : object, IYDef
    {
        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="strYDEF"></param>
        public YDefClass(string strYDEF)
        {
            if (string.IsNullOrEmpty ( strYDEF ))
                throw new ArgumentNullException ( "strYDEF" , "参数错误：该参数不能为空。" );
            if (!strYDEF.ToUpper ( ).Contains ( "YDEF" ))
                throw new ArgumentException ( "参数错误：该参数不是YDEF描述。" , "strYDEF" , new Exception ( strYDEF ) );

            string[] paras = strYDEF.ToUpper ( ).Split ( ' ' );

            _ynum = int.Parse ( paras[1] );
            _YDEF_Type = (Mapping_Type)Enum.Parse ( typeof ( Mapping_Type ) , paras[2].ToUpper ( ) );

            switch (_YDEF_Type)
            {
                case Mapping_Type.Linear:
                    if (paras.Length == 5)
                    {
                        _start = double.Parse ( paras[3] );
                        _increment = double.Parse ( paras[4] );
                    }
                    else
                        throw new ArgumentException ( );
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
                        throw new ArgumentException ( );
                    break;
                default:
                    throw new ArgumentUndealException ( "参数错误：没有对该参数进行处理。" , "YDEFType" , new Exception ( MappingType.ToString ( ) ) );
            }
        }
        #endregion

        #region 成员变量
        private int _ynum;
        private Mapping_Type _YDEF_Type;
        private double _start = double.NaN, _increment = double.NaN;
        private List<double> _LEVELS = null;
        #endregion

        #region 属性
        /// <summary>
        /// YSize
        /// </summary>
        public int YSize
        {
            get
            {
                if (_ynum >= 1)
                    return _ynum;
                else
                    throw new ArgumentOutOfRangeException ( );
            }
        }

        /// <summary>
        /// YDEFType
        /// </summary>
        public Mapping_Type MappingType => _YDEF_Type;
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
