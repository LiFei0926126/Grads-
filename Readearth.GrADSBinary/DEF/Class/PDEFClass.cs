using System;

namespace Readearth.GrADSBinary.DEF
{
    /// <summary>
    /// 
    /// </summary>
    public class PDefClass : object, IPDef, ILcc_PDef, ILccr_PDef
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="strPdef"></param>
        public PDefClass(string strPdef)
        {
            if (string.IsNullOrEmpty ( strPdef ))
                throw new ArgumentNullException ( "参数错误：该参数不能为空。" );


            string[] paras = strPdef.Split ( ' ' );

            if (paras[0].ToUpper ( ) != "PDEF")
                throw new ArgumentException ( "参数错误：该参数不是PDEF描述。" );

            string _Pro = paras[3].Replace ( '.' , '_' ).ToUpper ( );
            _Pro_Type = (Pro_Type)Enum.Parse ( typeof ( Pro_Type ) , _Pro );

            switch (_Pro_Type)
            {
                case Pro_Type.LCC:
                case Pro_Type.LCCR:

                    #region Decode
                    _iSize = int.Parse ( paras[1] );
                    _jSize = int.Parse ( paras[2] );
                    _latref = double.Parse ( paras[4] );
                    _lonref = double.Parse ( paras[5] );
                    _iref = double.Parse ( paras[6] );
                    _jref = double.Parse ( paras[7] );
                    _Struelat = double.Parse ( paras[8] );
                    _Ntruelat = double.Parse ( paras[9] );
                    _slon = double.Parse ( paras[10] );
                    _dx = double.Parse ( paras[11] );
                    _dy = double.Parse ( paras[12] );
                    #endregion

                    break;
                default:
                    throw new ArgumentUndealException ( "暂未处理该投影类型。" );
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
        public Pro_Type Pro_Type => _Pro_Type;

        /// <summary>
        /// The size of the native grid in the x direction
        /// </summary>
        public int ISize => _iSize;

        /// <summary>
        /// The size of the native grid in the y direction
        /// </summary>
        public int JSize => _jSize;

        /// <summary>
        /// reference latitude
        /// </summary>
        public double LatRef => _latref;

        /// <summary>
        /// reference longitude (in degrees, E is positive, W is negative)
        /// </summary>
        public double LonRef => _lonref;

        /// <summary>
        /// i of ref point
        /// </summary>
        public double IRef => _iref;

        /// <summary>
        /// j of ref point
        /// </summary>
        public double JRef => _jref;

        /// <summary>
        /// S true lat
        /// </summary>
        public double Struelat => _Struelat;

        /// <summary>
        /// N true lat
        /// </summary>
        public double Ntruelat => _Ntruelat;

        /// <summary>
        /// standard longitude
        /// </summary>
        public double SLon => _slon;

        /// <summary>
        /// grid X increment in meters
        /// </summary>
        public double DX => _dx;

        /// <summary>
        /// grid Y increment in meters
        /// </summary>
        public double DY => _dy;
        /// <summary>
        /// 单层数据的字节数
        /// </summary>
        public long BlockSize => _iSize * _jSize * 4;
        #endregion

    }
}
