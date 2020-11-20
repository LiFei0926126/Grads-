using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;
namespace Readearth.GrADSBinary.DEF
{
    /// <summary>
    /// 时间维定义
    /// </summary>
    public class TDefClass : object, ITDef
    {
        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="strTDEF"></param>
        public TDefClass(string strTDEF)
        {
            if (string.IsNullOrEmpty ( strTDEF ))
                throw new ArgumentNullException ( "strTDEF" , "参数错误：该参数不能为空。" );
            if (!strTDEF.ToUpper ( ).Contains ( "TDEF" ))
                throw new ArgumentException ( "参数错误：该参数不是TDEF描述。" , "strTDEF" , new Exception ( strTDEF ) );

            string[] paras = strTDEF.ToUpper ( ).Split ( ' ' );

            if (paras.Length == 5)
            {
                m_Tnum = int.Parse ( paras[1] );

                string ss = DateTime.Now.ToString ( "HH:ssZddMMMyyyy" );

                //IFormatProvider ifp = new CultureInfo("zh-CN", true);
                //DateTime dtTest = DateTime.ParseExact(ss, "HH:ssZddMMMyyyy", ifp);

                IFormatProvider ifp = new CultureInfo ( "en-US" , true );
                if (paras[3].Length == 9)
                    _startTime = DateTime.ParseExact ( paras[3] , "ddMMMyyyy" , ifp );
                else if (paras[3].Length == 12)
                    _startTime = DateTime.ParseExact ( paras[3] , "HHZddMMMyyyy" , ifp );
                else if (paras[3].Length == 15)
                    _startTime = DateTime.ParseExact ( paras[3] , "HH:mmZddMMMyyyy" , ifp );
                else
                    throw new ArgumentUndealException ( "未被识别为有效的时间字符串。" );

                string strUnit = new Regex ( "[A-z]{2}" ).Match ( paras[4] ).ToString ( );
                string strIncrement = new Regex ( "[0-9]{1,2}" ).Match ( paras[4] ).ToString ( );

                int _increment = int.MinValue;
                int.TryParse ( strIncrement , out _increment );

                TDef_Unit pTDEF_Unit = (TDef_Unit)Enum.Parse ( typeof ( TDef_Unit ) , strUnit , true );

                for (int i = 0 ; i < m_Tnum ; i++)
                {
                    switch (pTDEF_Unit)
                    {
                        case TDef_Unit.MN:
                            _UTC_ForecastTimes.Add ( UTC_StratTime.AddMinutes ( i * _increment ) );
                            break;
                        case TDef_Unit.HR:
                            _UTC_ForecastTimes.Add ( UTC_StratTime.AddHours ( i * _increment ) );
                            break;
                        case TDef_Unit.DY:
                            _UTC_ForecastTimes.Add ( UTC_StratTime.AddDays ( i * _increment ) );
                            break;
                        case TDef_Unit.MO:
                            _UTC_ForecastTimes.Add ( UTC_StratTime.AddMonths ( i * _increment ) );
                            break;
                        case TDef_Unit.YR:
                            _UTC_ForecastTimes.Add ( UTC_StratTime.AddYears ( i * _increment ) );
                            break;
                    }
                }
                for (int i = 0 ; i < m_Tnum ; i++)
                {
                    switch (pTDEF_Unit)
                    {
                        case TDef_Unit.MN:
                            _BJ_ForecastTimes.Add ( BJT_StratTime.AddMinutes ( i * _increment ) );
                            break;
                        case TDef_Unit.HR:
                            _BJ_ForecastTimes.Add ( BJT_StratTime.AddHours ( i * _increment ) );
                            break;
                        case TDef_Unit.DY:
                            _BJ_ForecastTimes.Add ( BJT_StratTime.AddDays ( i * _increment ) );
                            break;
                        case TDef_Unit.MO:
                            _BJ_ForecastTimes.Add ( BJT_StratTime.AddMonths ( i * _increment ) );
                            break;
                        case TDef_Unit.YR:
                            _BJ_ForecastTimes.Add ( BJT_StratTime.AddYears ( i * _increment ) );
                            break;
                    }
                }
            }
            else
                throw new ArgumentException ( strTDEF );
        }
        #endregion

        #region 成员变量
        private int m_Tnum;
        private DateTime _startTime = DateTime.MinValue;
        private int _increment = int.MinValue;
        private List<DateTime> _UTC_ForecastTimes = new List<DateTime> ( );
        private List<DateTime> _BJ_ForecastTimes = new List<DateTime> ( );
        #endregion

        #region 属性
        /// <summary>
        /// 预报时间列表（世界时）
        /// </summary>
        public List<DateTime> UTC_ForecastTimes => _UTC_ForecastTimes;
        /// <summary>
        /// 预报时间列表（北京时）
        /// </summary>
        public List<DateTime> BJT_ForecastTimes => _BJ_ForecastTimes;
        /// <summary>
        /// 时间维长度
        /// </summary>
        public int TSize
        {
            get
            {
                if (m_Tnum >= 1)
                    return m_Tnum;
                else
                    throw new ArgumentOutOfRangeException ( );
            }
        }
        /// <summary>
        /// 时间维起始时间（世界时）
        /// </summary>
        public DateTime UTC_StratTime
        {
            get
            {
                if (_startTime == DateTime.MinValue)
                    throw new ArgumentUndealException ( );
                else
                    return _startTime.AddHours ( -8 );
            }
        }
        /// <summary>
        /// 时间维起始时间（北京时）
        /// </summary>
        public DateTime BJT_StratTime
        {
            get
            {
                if (_startTime == DateTime.MinValue)
                    throw new ArgumentUndealException ( );
                else
                    return _startTime;
            }
        }
        /// <summary>
        /// 时间维增量
        /// </summary>
        public int Increment
        {
            get
            {
                if (_increment == int.MinValue)
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
