using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;
using System.Text.RegularExpressions;
namespace Readearth.GrADSBinary.DEF
{
    /// <summary>
    /// 时间维定义
    /// </summary>
    public class TDEFClass : Object,ITDEF
    {
        #region 构造函数
        /// <summary>
        /// 
        /// </summary>
        /// <param name="strTDEF"></param>
        public TDEFClass(string strTDEF)
        {
            if (string.IsNullOrEmpty(strTDEF))
                throw new ArgumentNullException("strTDEF", "参数错误：该参数不能为空。");
            if (!strTDEF.ToUpper().Contains("TDEF"))
                throw new ArgumentException("参数错误：该参数不是TDEF描述。", "strTDEF", new Exception(strTDEF));

            string[] paras = strTDEF.ToUpper().Split(' ');

            if (paras.Length == 5)
            {
                _tnum = int.Parse(paras[1]);

                string ss = DateTime.Now.ToString("HH:ssZddMMMyyyy");

                //IFormatProvider ifp = new CultureInfo("zh-CN", true);
                //DateTime dtTest = DateTime.ParseExact(ss, "HH:ssZddMMMyyyy", ifp);
                
                IFormatProvider ifp = new CultureInfo("en-US", true);
                if (paras[3].Length == 9)
                    _startTime = DateTime.ParseExact(paras[3], "ddMMMyyyy", ifp);
                else if (paras[3].Length == 12)
                    _startTime = DateTime.ParseExact(paras[3], "HHZddMMMyyyy", ifp);
                else if (paras[3].Length == 15)
                    _startTime = DateTime.ParseExact(paras[3], "HH:mmZddMMMyyyy", ifp);
                else
                    throw new ArgumentUndealException("未被识别为有效的时间字符串。");
                int _increment = int.Parse(new Regex("[0-9]{1,2}").Match(paras[4]).ToString());

                TDEF_Unit pTDEF_Unit = (TDEF_Unit)Enum.Parse(typeof(TDEF_Unit),new Regex("[A-z]{2}").Match(paras[4]).ToString().ToUpper());

                for (int i = 0; i < _tnum; i++)
                {
                    switch (pTDEF_Unit)
                    {
                        case TDEF_Unit.MN:
                            _UTC_ForecastTimes.Add(UTC_STRATTIME.AddMinutes(i));
                            break;
                        case TDEF_Unit.HR:
                            _UTC_ForecastTimes.Add(UTC_STRATTIME.AddHours(i));
                            break;
                        case TDEF_Unit.DY:
                            _UTC_ForecastTimes.Add(UTC_STRATTIME.AddDays(i));
                            break;
                        case TDEF_Unit.MO:
                            _UTC_ForecastTimes.Add(UTC_STRATTIME.AddMonths(i));
                            break;
                        case TDEF_Unit.YR:
                            _UTC_ForecastTimes.Add(UTC_STRATTIME.AddYears(i));
                            break;
                    }
                }
                for (int i = 0; i < _tnum; i++)
                {
                    switch (pTDEF_Unit)
                    {
                        case TDEF_Unit.MN:
                            _BJ_ForecastTimes.Add(BJT_STRATTIME.AddMinutes(i));
                            break;
                        case TDEF_Unit.HR:
                            _BJ_ForecastTimes.Add(BJT_STRATTIME.AddHours(i));
                            break;
                        case TDEF_Unit.DY:
                            _BJ_ForecastTimes.Add(BJT_STRATTIME.AddDays(i));
                            break;
                        case TDEF_Unit.MO:
                            _BJ_ForecastTimes.Add(BJT_STRATTIME.AddMonths(i));
                            break;
                        case TDEF_Unit.YR:
                            _BJ_ForecastTimes.Add(BJT_STRATTIME.AddYears(i));
                            break;
                    }
                }
            }
            else
                throw new ArgumentException(strTDEF);
        }
        #endregion

        #region 成员变量
        int _tnum;
        DateTime _startTime =DateTime.MinValue;
        int _increment = int.MinValue;
        List<DateTime> _UTC_ForecastTimes = new List<DateTime>();
        List<DateTime> _BJ_ForecastTimes = new List<DateTime>();
        #endregion

        #region 属性
        /// <summary>
        /// 预报时间列表（世界时）
        /// </summary>
        public List<DateTime> UTC_ForecastTimes
        {
            get
            {
                return _UTC_ForecastTimes;
            }
        }
        /// <summary>
        /// 预报时间列表（北京时）
        /// </summary>
        public List<DateTime> BJT_ForecastTimes
        {
            get
            {
                return _BJ_ForecastTimes;
            }
        }
        /// <summary>
        /// 时间维长度
        /// </summary>
        public int TSize
        {
            get
            {
                if (_tnum >= 1)
                    return _tnum;
                else
                    throw new ArgumentOutOfRangeException();
            }
        }
        /// <summary>
        /// 时间维起始时间（世界时）
        /// </summary>
        public DateTime UTC_STRATTIME
        {
            get
            {
                if (_startTime == DateTime.MinValue)
                    throw new ArgumentUndealException();
                else
                    return _startTime.AddHours(-8);
            }
        }
        /// <summary>
        /// 时间维起始时间（北京时）
        /// </summary>
        public DateTime BJT_STRATTIME
        {
            get
            {
                if (_startTime == DateTime.MinValue)
                    throw new ArgumentUndealException();
                else
                    return _startTime;
            }
        }
        /// <summary>
        /// 时间维增量
        /// </summary>
        public int INCREMENT
        {
            get
            {
                if (_increment == int.MinValue)
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
