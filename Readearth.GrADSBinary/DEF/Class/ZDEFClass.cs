using System;
using System.Collections.Generic;
using System.Text;

namespace Readearth.GrADSBinary.DEF
{
    /// <summary>
    /// 定义格点数据的z维格点值。
    /// </summary>
    public class ZDEFClass : Object,IZDEF
    {
        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="strZDEF">文本描述</param>
        /// <exception cref="ArgumentNullException">参数为空异常。</exception>
        /// <exception cref="ArgumentException">参数不是VARS描述。</exception>
        /// <exception cref="ArgumentException"></exception>
        public ZDEFClass(string strZDEF)
        {
            if (string.IsNullOrEmpty(strZDEF))
                throw new ArgumentNullException("strZDEF", "参数错误：该参数不能为空。");
            if (!strZDEF.ToUpper().Contains("ZDEF"))
                throw new ArgumentException("参数错误：该参数不是ZDEF描述。", "strZDEF", new Exception(strZDEF));

            string[] paras = strZDEF.ToUpper().Split(' ');

            _xnum = int.Parse(paras[1]);
            _ZDEF_Type = (XYZDEF_Type)Enum.Parse(typeof(XYZDEF_Type), paras[2].ToUpper());

            switch (_ZDEF_Type)
            {
                case XYZDEF_Type.LINEAR:
                    if (paras.Length == 5)
                    {
                        _start = double.Parse(paras[3]);
                        _increment = double.Parse(paras[4]);
                    }
                    else
                        throw new ArgumentException(strZDEF);
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
                        throw new ArgumentException(strZDEF);
                    break;
            }

        }
        #endregion

        #region 成员变量
        int _xnum;
        XYZDEF_Type _ZDEF_Type;
        double _start = double.NaN, _increment = double.NaN;
        List<double> _LEVELS = null;
        #endregion

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
                    throw new ArgumentOutOfRangeException();
            }
        }
        /// <summary>
        /// ZDEF类型
        /// </summary>
        public XYZDEF_Type ZDEFType
        {
            get
            {
                return _ZDEF_Type;
            }
        }
        /// <summary>
        /// levels列表
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
        /// 起始值
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
        /// 增长量
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

