using System;
using System.Collections.Generic;
using System.Text;

namespace Readearth.GrADSBinary.DEF
{
    /// <summary>
    /// This entry defines the grid point values for the X dimension, or longitude.
    /// </summary>
    public class XDEFClass : Object,IXDEF
    {
        #region 构造函数
        /// <summary>
        /// 
        /// </summary>
        /// <param name="strXDEF"></param>
        public XDEFClass(string strXDEF)
        {
            if (string.IsNullOrEmpty(strXDEF))
                throw new ArgumentNullException("strXDEF", "参数错误：该参数不能为空。");
            if (!strXDEF.ToUpper().Contains("XDEF"))
                throw new ArgumentException("参数错误：该参数不是XDEF描述。", "strXDEF",new Exception(strXDEF));

            string[] paras = strXDEF.ToUpper().Split(' ');

            _xnum = int.Parse(paras[1]);
            _XDEF_Type = (XYZDEF_Type)Enum.Parse(typeof(XYZDEF_Type), paras[2].ToUpper());

            switch (_XDEF_Type)
            {
                case XYZDEF_Type.LINEAR:
                    if (paras.Length == 5)
                    {
                        _start = double.Parse(paras[3]);
                        _increment = double.Parse(paras[4]);
                    }
                    else
                        throw new ArgumentException(strXDEF);
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
                        throw new ArgumentException(strXDEF);
                    break;
            }

        }
        #endregion

        #region 成员变量
        int _xnum;
        XYZDEF_Type _XDEF_Type;
        double _start = double.NaN, _increment = double.NaN;
        List<double> _LEVELS = null;
        #endregion

        #region 属性
        /// <summary>
        /// 
        /// </summary>
        public int XSize
        {
            get
            {
                if (_xnum >= 1)
                    return _xnum;
                else
                    throw new ArgumentOutOfRangeException();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public XYZDEF_Type XDEFType
        {
            get
            {
                return _XDEF_Type;
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
