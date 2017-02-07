using System;
using System.Collections.Generic;
using System.Text;

namespace Readearth.GrADSBinary.DEF
{
    /// <summary>
    /// 
    /// </summary>
    public class LCC_PDEFClass : PDEFClass, IPDEF, ILCC_PDEF
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="strPdef"></param>
        public LCC_PDEFClass(string strPdef)
            : base(strPdef)
        {
            string[] paras = strPdef.Split(' ');
            _iSize = int.Parse(paras[1]);
            _jSize = int.Parse(paras[2]);
            _latref = double.Parse(paras[4]);
            _lonref = double.Parse(paras[5]);
            _iref = double.Parse(paras[6]);
            _jref = double.Parse(paras[7]);
            _Struelat = double.Parse(paras[8]);
            _Ntruelat = double.Parse(paras[9]);
            _slon = double.Parse(paras[10]);
            _dx = double.Parse(paras[11]);
            _dy = double.Parse(paras[12]);
        }

        #region 属性

        /// <summary>
        /// The size of the native grid in the x direction
        /// </summary>
        int ILCC_PDEF.ISize
        {
            get
            {
                return _iSize;
            }
        }

        /// <summary>
        /// The size of the native grid in the y direction
        /// </summary>
        int ILCC_PDEF.JSize
        {
            get
            {
                return _jSize;
            }
        }

        /// <summary>
        /// reference latitude
        /// </summary>
        double ILCC_PDEF.LatRef
        {
            get
            {
                return _latref;
            }
        }

        /// <summary>
        /// reference longitude (in degrees, E is positive, W is negative)
        /// </summary>
        double ILCC_PDEF.LonRef
        {
            get
            {
                return _lonref;
            }
        }

        /// <summary>
        /// i of ref point
        /// </summary>
        double ILCC_PDEF.IRef
        {
            get
            {
                return _iref;
            }
        }

        /// <summary>
        /// j of ref point
        /// </summary>
        double ILCC_PDEF.JRef
        {
            get
            {
                return _jref;
            }
        }

        /// <summary>
        /// S true lat
        /// </summary>
        double ILCC_PDEF.Struelat
        {
            get
            {
                return _Struelat;
            }
        }

        /// <summary>
        /// N true lat
        /// </summary>
        double ILCC_PDEF.Ntruelat
        {
            get
            {
                return _Ntruelat;
            }
        }

        /// <summary>
        /// standard longitude
        /// </summary>
        double ILCC_PDEF.SLon
        {
            get
            {
                return _slon;
            }
        }

        /// <summary>
        /// grid X increment in meters
        /// </summary>
        double ILCC_PDEF.DX
        {
            get
            {
                return _dx;
            }
        }

        /// <summary>
        /// grid Y increment in meters
        /// </summary>
        double ILCC_PDEF.DY
        {
            get
            {
                return _dy;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        long ILCC_PDEF.BlockSize
        {
            get
            {
                return _iSize * _jSize * 4;
            }
        }

        
        #endregion
    }
}
