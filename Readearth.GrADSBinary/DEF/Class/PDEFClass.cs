using System;
using System.Collections.Generic;
using System.Text;

namespace Readearth.GrADSBinary.DEF
{
    /// <summary>
    /// 
    /// </summary>
    public class PDEFClass : Object, IPDEF//, ILCC_PDEF, ILCCR_PDEF
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="strPdef"></param>
        public PDEFClass(string strPdef)
        {
            if (string.IsNullOrEmpty(strPdef))
                throw new ArgumentNullException("参数错误：该参数不能为空。");
            if (!strPdef.ToUpper().Contains("PDEF"))
                throw new ArgumentException("参数错误：该参数不是PDEF描述。");

            string[] paras = strPdef.Split(' ');



            string _Pro = paras[3].Replace('.', '_').ToUpper();
            _Pro_Type = (Pro_Type)Enum.Parse(typeof(Pro_Type), _Pro);
            switch (_Pro_Type)
            {
                case Pro_Type.LCC:
                case Pro_Type.LCCR:
                    break;
                default:
                    throw new ArgumentUndealException("暂未处理该投影类型。");
            }
        }
        #region 成员变量
        /// <summary>
        /// PDEF 类型
        /// </summary>
       protected Pro_Type _Pro_Type;
        /// <summary>
        /// 
        /// </summary>
        protected int _iSize, _jSize;
        /// <summary>
        /// 
        /// </summary>
        protected double _latref, _lonref, _iref, _jref, _Struelat, _Ntruelat, _slon, _dx, _dy;
        #endregion

        #region 属性
        /// <summary>
        /// 投影类型
        /// </summary>
        public Pro_Type Pro_Type
        {
            get
            {
                return _Pro_Type;
            }
        }

        ///// <summary>
        ///// The size of the native grid in the x direction
        ///// </summary>
        //public int ISize
        //{
        //    get
        //    {
        //        return _iSize;
        //    }
        //}

        ///// <summary>
        ///// The size of the native grid in the y direction
        ///// </summary>
        //public int JSize
        //{
        //    get
        //    {
        //        return _jSize;
        //    }
        //}

        ///// <summary>
        ///// reference latitude
        ///// </summary>
        //public double LatRef
        //{
        //    get
        //    {
        //        return _latref;
        //    }
        //}

        ///// <summary>
        ///// reference longitude (in degrees, E is positive, W is negative)
        ///// </summary>
        //public double LonRef
        //{
        //    get
        //    {
        //        return _lonref;
        //    }
        //}

        ///// <summary>
        ///// i of ref point
        ///// </summary>
        //public double IRef
        //{
        //    get
        //    {
        //        return _iref;
        //    }
        //}

        ///// <summary>
        ///// j of ref point
        ///// </summary>
        //public double JRef
        //{
        //    get
        //    {
        //        return _jref;
        //    }
        //}

        ///// <summary>
        ///// S true lat
        ///// </summary>
        //public double Struelat
        //{
        //    get
        //    {
        //        return _Struelat;
        //    }
        //}

        ///// <summary>
        ///// N true lat
        ///// </summary>
        //public double Ntruelat
        //{
        //    get
        //    {
        //        return _Ntruelat;
        //    }
        //}

        ///// <summary>
        ///// standard longitude
        ///// </summary>
        //public double SLon
        //{
        //    get
        //    {
        //        return _slon;
        //    }
        //}

        ///// <summary>
        ///// grid X increment in meters
        ///// </summary>
        //public double DX
        //{
        //    get
        //    {
        //        return _dx;
        //    }
        //}

        ///// <summary>
        ///// grid Y increment in meters
        ///// </summary>
        //public double DY
        //{
        //    get
        //    {
        //        return _dy;
        //    }
        //}
        ///// <summary>
        ///// 单层数据的字节数
        ///// </summary>
        //public long BlockSize
        //{
        //    get
        //    {
        //        return _iSize * _jSize * 4;
        //    }
        //}
        #endregion

    }
}
