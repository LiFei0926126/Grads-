using System;
using System.Collections.Generic;

namespace Readearth.GrADSBinary.DEF
{
    /// <summary>
    /// 定义格点数据的z维格点值。
    /// </summary>
    public class ZDefClass : object, IZDef
    {
        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="strZDEF">文本描述</param>
        /// <exception cref="ArgumentNullException">参数为空异常。</exception>
        /// <exception cref="ArgumentException">参数不是VARS描述。</exception>
        /// <exception cref="ArgumentException"></exception>
        public ZDefClass(string strZDEF)
        {
            if (string.IsNullOrEmpty ( strZDEF ))
                throw new ArgumentNullException ( "strZDEF" , "参数错误：该参数不能为空。" );
            if (!strZDEF.ToUpper ( ).Contains ( "ZDEF" ))
                throw new ArgumentException ( "参数错误：该参数不是ZDEF描述。" , "strZDEF" , new Exception ( strZDEF ) );

            string[] paras = strZDEF.ToUpper ( ).Split ( ' ' );

            _xnum = int.Parse ( paras[1] );
            _ZDEF_Type = (Mapping_Type)Enum.Parse ( typeof ( Mapping_Type ) , paras[2].ToUpper ( ) );

            switch (_ZDEF_Type)
            {
                case Mapping_Type.Linear:
                    if (paras.Length == 5)
                    {
                        _start = double.Parse ( paras[3] );
                        _increment = double.Parse ( paras[4] );
                    }
                    else
                        throw new ArgumentException ( strZDEF );
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
                        throw new ArgumentException ( strZDEF );
                    break;
            }
        }

        #endregion 构造函数

        #region 成员变量

        private int _xnum;
        private Mapping_Type _ZDEF_Type;
        private double _start = double.NaN, _increment = double.NaN;
        private List<double> _LEVELS = null;

        #endregion 成员变量

        #region 属性

        /// <summary>
        /// Z维长度
        /// </summary>
        public int ZSize
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
        /// ZDEF类型
        /// </summary>
        public Mapping_Type MappingType => _ZDEF_Type;

        /// <summary>
        /// levels列表
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
        /// 起始值
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
        /// 增长量
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

        #endregion 属性
    }
}