using System;
using System.Collections.Generic;
using System.Text;

namespace Readearth.GrADSBinary.DEF
{
    /// <summary>
    /// 
    /// </summary>
    public class YDEFClass : Object,IYDEF
    {
        #region 构造函数
        /// <summary>
        /// 
        /// </summary>
        /// <param name="strYDEF"></param>
        public YDEFClass(string strYDEF)
        {
            if (string.IsNullOrEmpty(strYDEF))
                throw new ArgumentNullException("strYDEF", "参数错误：该参数不能为空。");
            if (!strYDEF.ToUpper().Contains("YDEF"))
                throw new ArgumentException("参数错误：该参数不是YDEF描述。", "strYDEF", new Exception(strYDEF));

            string[] paras = strYDEF.ToUpper().Split(' ');

            _ynum = int.Parse(paras[1]);
            _YDEF_Type = (XYZDEF_Type)Enum.Parse(typeof(XYZDEF_Type), paras[2].ToUpper());

            switch (_YDEF_Type)
            {
                case XYZDEF_Type.LINEAR:
                    if (paras.Length == 5)
                    {
                        _start = double.Parse(paras[3]);
                        _increment = double.Parse(paras[4]);
                    }
                    else
                        throw new ArgumentException();
                    break;
                case XYZDEF_Type.LEVELS:
                    if (paras.Length >= 4)
                    {
                        _LEVELS = new List<double>();
                        for (int i = 3; i < paras.Length; i++)
                        {
                            _LEVELS.Add(double.Parse(paras[i]));
                        }
                    }
                    else
                        throw new ArgumentException();
                    break;
                default:
                    throw new ArgumentUndealException("参数错误：没有对该参数进行处理。", "YDEFType",new Exception(YDEFType.ToString()));
            }
        }
        #endregion

        #region 成员变量
        int _ynum;
        XYZDEF_Type _YDEF_Type;
        double _start = double.NaN, _increment = double.NaN;
        List<double> _LEVELS = null;
        #endregion

        #region 属性
        /// <summary>
        /// 
        /// </summary>
        public int YSize
        {
            get
            {
                if (_ynum >= 1)
                    return _ynum;
                else
                    throw new ArgumentOutOfRangeException();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public XYZDEF_Type YDEFType
        {
            get
            {
                return _YDEF_Type;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public List<double> LEVELS
        {
            get
            {
                if (_LEVELS == null)
                    throw new ArgumentUndealException();
                else
                    return _LEVELS;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public double STRAT
        {
            get
            {
                if (_start == double.NaN)
                    throw new ArgumentUndealException();
                else
                    return _start;
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        public double INCREMENT
        {
            get
            {
                if (_increment == double.NaN)
                    throw new ArgumentUndealException();
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
