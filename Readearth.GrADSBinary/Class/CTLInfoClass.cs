using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;
using Readearth.GrADSBinary.DEF;
using Projection;

namespace Readearth.GrADSBinary
{
    /// <summary>
    /// CTL文件解析类
    /// </summary>
    public class CTLInfoClass : Object, Readearth.GrADSBinary.ICTLInfo
    {
        #region 常量
        const string empty = "";
        #endregion

        #region 成员变量
        string _ctlFielPath;
        string _DSET = string.Empty, _Title = string.Empty;
        float _Undef = float.NaN;
        //PDEFClass _PDEF = null;
        //XDEFClass _XDEF = null;
        //YDEFClass _YDEF = null;
        //ZDEFClass _ZDEF = null;
        //TDEFClass _TDEF = null;
        //VARSClass _VARS = null;
        IOptions _Options = null;
        IPDEF _PDEF = null;
        IXDEF _XDEF = null;
        IYDEF _YDEF = null;
        IZDEF _ZDEF = null;
        ITDEF _TDEF = null;
        IVARS _VARS = null;

        LamProjection lp;
        bool _IsCTLFileLoaded = false;

        double _Orgin_Lat = double.NaN;
        #endregion

        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="strCTLPath"></param>
        public CTLInfoClass(string strCTLPath = empty)
        {
            if (!string.IsNullOrEmpty(strCTLPath))
            {
                _ctlFielPath = strCTLPath;
                LoadCTL(_ctlFielPath);
            }
        }
        #endregion

        #region 私有函数
        void Prase(string strPara)
        {

            string[] paras = Regex.Split(strPara.Trim(), "\\s+");

            switch (paras[0].ToLower())
            {
                case "dset":
                    _DSET = paras[1];
                    break;
                case "title":
                    List<string> tmp = new List<string>(paras);
                    tmp.RemoveAt(0);
                    _Title = string.Join(" ", tmp.ToArray());
                    break;
                case "options":
                    tmp = new List<string>(paras);
                    tmp.RemoveAt(0);
                    _Options = new Options(string.Join(" ", tmp.ToArray()));
                    break;
                case "undef":
                    _Undef = float.Parse(paras[1]);
                    break;
                case "pdef":
                    _PDEF = new PDEFClass(string.Join(" ", paras));
                    switch (_PDEF.Pro_Type)
                    {
                        case Pro_Type.LCC:
                            _PDEF = new LCC_PDEFClass(string.Join(" ", paras));
                            break;
                        case Pro_Type.LCCR:
                            _PDEF = new LCCR_PDEFClass(string.Join(" ", paras));
                            break;

                    }

                    break;
                case "xdef":
                    _XDEF = new XDEFClass(string.Join(" ", paras));
                    break;
                case "ydef":
                    _YDEF = new YDEFClass(string.Join(" ", paras));
                    break;
                case "zdef":
                    _ZDEF = new ZDEFClass(string.Join(" ", paras));
                    break;
                case "tdef":
                    _TDEF = new TDEFClass(string.Join(" ", paras));
                    break;
                case "vars":
                    _VARS = new VARSClass(strPara);
                    break;
            }

        }
        #endregion

        #region 公有函数
        /// <summary>
        /// 加载CTL文件。
        /// </summary>
        /// <returns>返回加载结果</returns>
        public bool LoadCTL()
        {
            _IsCTLFileLoaded = false;
            StreamReader sr = new StreamReader(_ctlFielPath);
            string strLine = "";
            while ((strLine = sr.ReadLine()) != null)
            {
                string strPattern = "^vars\\s+[1-9]{1}[0-9]{0,}$";
                string strPattern1 = "^zdef\\s+[1-9]{1}[0-9]{0,}";
                //^endvars$
                if (Regex.IsMatch(strLine, strPattern, RegexOptions.IgnoreCase))
                {
                    StringBuilder sb = new StringBuilder();
                    string strPatternEnd = "^endvars$";

                    do
                    {
                        sb.AppendLine(strLine.Trim(' '));
                    }
                    while (!Regex.IsMatch((strLine = sr.ReadLine()), strPatternEnd, RegexOptions.IgnoreCase));
                    sb.AppendLine(strLine.Trim(' '));

                    Prase(sb.ToString());
                }
                else if (Regex.IsMatch(strLine, strPattern1, RegexOptions.IgnoreCase))
                {
                    if (Regex.IsMatch(strLine, "levels", RegexOptions.IgnoreCase))
                    {
                        StringBuilder sb = new StringBuilder();
                        //string strPatternEnd = "^endvars$";
                        string[] des = Regex.Split(strLine.Trim(), "\\s+");
                        //do
                        sb.Append(strLine);
                        while (Regex.Split(sb.ToString(), "\\s+").Length < des.Length + int.Parse(des[1]))
                        {
                            strLine = sr.ReadLine();

                            sb.Append(" " + strLine.Trim());
                        }
                        //sb.Append(" " + strLine.Trim(' '));

                        Prase(sb.ToString());
                    }
                    else
                        Prase(strLine.Trim());
                }
                else
                    Prase(strLine.Trim(' '));
            }

            sr.Close();
            sr.Dispose();
            _IsCTLFileLoaded = true;

            return _IsCTLFileLoaded;
        }
        /// <summary>
        /// 加载CTL文件。
        /// </summary>
        /// <param name="strCTLPath"></param>
        /// <returns>返回加载结果</returns>
        public bool LoadCTL(string strCTLPath)
        {
            _IsCTLFileLoaded = false;

            if (File.Exists(strCTLPath))
            {
                _ctlFielPath = strCTLPath;
                StreamReader sr = new StreamReader(strCTLPath);
                string strLine = "";
                while ((strLine = sr.ReadLine()) != null)
                {
                    string strPattern = "^vars\\s+[1-9]{1}[0-9]{0,}$";
                    string strPattern1 = "^zdef\\s+[1-9]{1}[0-9]{0,}";
                    //^endvars$
                    if (Regex.IsMatch(strLine, strPattern, RegexOptions.IgnoreCase))
                    {
                        StringBuilder sb = new StringBuilder();
                        string strPatternEnd = "^endvars$";

                        do
                        {
                            sb.AppendLine(strLine.Trim(' '));
                        }
                        while (!Regex.IsMatch((strLine = sr.ReadLine()), strPatternEnd, RegexOptions.IgnoreCase));
                        sb.AppendLine(strLine.Trim(' '));

                        Prase(sb.ToString());
                    }
                    else if (Regex.IsMatch(strLine, strPattern1, RegexOptions.IgnoreCase))
                    {
                        if (Regex.IsMatch(strLine, "levels", RegexOptions.IgnoreCase))
                        {
                            StringBuilder sb = new StringBuilder();
                            //string strPatternEnd = "^endvars$";
                            string[] des = Regex.Split(strLine.Trim(), "\\s+");
                            //do
                            sb.Append(strLine);
                            while (Regex.Split(sb.ToString(), "\\s+").Length < des.Length + int.Parse(des[1]))
                            {
                                strLine = sr.ReadLine();

                                sb.Append(" " + strLine.Trim());
                            }
                            //sb.Append(" " + strLine.Trim(' '));

                            Prase(sb.ToString());
                        }
                        else
                            Prase(strLine.Trim());
                    }
                    else
                        Prase(strLine.Trim(' '));
                }

                sr.Close();
                sr.Dispose();
                _IsCTLFileLoaded = true;

                return _IsCTLFileLoaded;
            }
            else
                throw new Exception("CTL文件不存在。");
        }
        /// <summary>
        /// 根据经纬度计算行列索引号。
        /// </summary>
        /// <param name="lon">经度</param>
        /// <param name="lat">纬度</param>
        /// <returns></returns>
        public XYPoint GetXYIndex(double lon, double lat)
        {
            XYPoint pXYIndex = new XYPoint();

            pXYIndex.Long = (float)lon;
            pXYIndex.Lat = (float)lat;

            IPDEF pPDEF = this.PDEF;
            switch (pPDEF.Pro_Type)
            {
                case Pro_Type.LCC:
                    ILCC_PDEF pLCC_PDEF = pPDEF as ILCC_PDEF;
                    lp = new LamProjection(new VectorPointXY(pLCC_PDEF.SLon, Orgin_Lat), pLCC_PDEF.Struelat, pLCC_PDEF.Ntruelat, GeoRefEllipsoid.WGS_84);
                    VectorPointXY xyRef = lp.GeoToProj(new VectorPointXY(pLCC_PDEF.LonRef, pLCC_PDEF.LatRef));
                    double xstart = xyRef.X + (0 - pLCC_PDEF.IRef) * pLCC_PDEF.DX;
                    double ystart = xyRef.Y + (0 - pLCC_PDEF.JRef) * pLCC_PDEF.DY;

                    VectorPointXY xy = lp.GeoToProj(new VectorPointXY(lon, lat));
                    double x = xy.X;
                    double y = xy.Y;

                    pXYIndex.ColIndex = (int)((x - xstart) / pLCC_PDEF.DX);
                    pXYIndex.RowIndex = (int)((y - ystart) / pLCC_PDEF.DY);
                    break;
                case Pro_Type.LCCR:
                    ILCCR_PDEF pLCCR_PDEF = pPDEF as ILCCR_PDEF;
                    lp = new LamProjection(new VectorPointXY(pLCCR_PDEF.SLon, 0), pLCCR_PDEF.Struelat, pLCCR_PDEF.Ntruelat, GeoRefEllipsoid.WGS_84);
                    xyRef = lp.GeoToProj(new VectorPointXY(pLCCR_PDEF.LonRef, pLCCR_PDEF.LatRef));
                    xstart = (0 - pLCCR_PDEF.IRef) * pLCCR_PDEF.DX + xyRef.X;
                    ystart = (0 - pLCCR_PDEF.JRef) * pLCCR_PDEF.DY + xyRef.Y;

                    xy = lp.GeoToProj(new VectorPointXY(lon, lat));
                    x = xy.X;
                    y = xy.Y;
                    pXYIndex.ColIndex = (int)((x - xstart) / pLCCR_PDEF.DX);
                    pXYIndex.RowIndex = (int)((y - ystart) / pLCCR_PDEF.DY);
                    break;
            }

            return pXYIndex;
        }
        /// <summary>
        /// 获取目标变量的开始位置。
        /// </summary>
        /// <param name="strVarName">变量名</param>
        /// <param name="level">z维索引</param>
        /// <returns>开始位置</returns>       
        public int GetBinaryBlockIndex(string strVarName, int level)
        {
            int pBinaryBlockIndex = 0;
            VariableClass pVariableClass = null;
            for (int i = 0; i < _VARS.VarCount; i++)
            {
                pVariableClass = _VARS.VARS[i];
                if (pVariableClass.VarName == strVarName)
                {
                    if (level >= pVariableClass.LevelCount)
                        throw new ArgumentOutOfRangeException("level", level, "参数错误：level超出当前变量的LEVELS。");
                    pBinaryBlockIndex += level * BaseArraySize;
                    break;
                }
                else
                    pBinaryBlockIndex += pVariableClass.LevelCount * BaseArraySize;
            }

            return pBinaryBlockIndex;
        }
        /// <summary>
        /// 获取目标变量的开始位置。
        /// </summary>
        /// <param name="varIndex">变量索引</param>
        /// <param name="level">z维索引</param>
        /// <returns>开始位置</returns>
        public int GetBinaryBlockIndex(int varIndex, int level)
        {

            int pBinaryBlockIndex = 0;
            VariableClass pVariableClass = null;
            for (int i = 0; i < varIndex; i++)
            {
                pVariableClass = _VARS.VARS[i];
                pBinaryBlockIndex += pVariableClass.LevelCount * BaseArraySize;
            }
            pVariableClass = _VARS.VARS[varIndex];
            if (level >= pVariableClass.LevelCount)
                throw new ArgumentOutOfRangeException("level", level, "参数错误：level超出当前变量的LEVELS。");
            pBinaryBlockIndex += level * BaseArraySize;
            return pBinaryBlockIndex;
        }

        /// <summary>
        /// 获取目标变量的开始位置。
        /// </summary>
        /// <param name="strVarName">变量名</param>
        /// <param name="timeIndex">时间维索引</param>
        /// <param name="level">z维索引</param>
        /// <returns>开始位置</returns>       
        public int GetBinaryBlockIndex(string strVarName, int timeIndex, int level)
        {
            int pBinaryBlockIndex = 0;
            VariableClass pVariableClass = null;
            for (int i = 0; i < _VARS.VarCount; i++)
            {
                pVariableClass = _VARS.VARS[i];
                if (pVariableClass.VarName == strVarName)
                {
                    if (level >= pVariableClass.LevelCount)
                        throw new ArgumentOutOfRangeException("level", level, "参数错误：level超出当前变量的LEVELS。");
                    pBinaryBlockIndex += level * BaseArraySize;
                    break;
                }
                else
                    pBinaryBlockIndex += pVariableClass.LevelCount * BaseArraySize;
            }

            return pBinaryBlockIndex + TimePageSize * timeIndex;
        }
        /// <summary>
        /// 获取目标变量的开始位置。
        /// </summary>
        /// <param name="varIndex">变量索引</param>
        /// <param name="timeIndex">时间维索引</param>
        /// <param name="level">z维索引</param>
        /// <returns>开始位置</returns>
        public int GetBinaryBlockIndex(int varIndex, int timeIndex, int level)
        {

            int pBinaryBlockIndex = 0;
            VariableClass pVariableClass = null;
            for (int i = 0; i < varIndex; i++)
            {
                pVariableClass = _VARS.VARS[i];
                pBinaryBlockIndex += pVariableClass.LevelCount * BaseArraySize;
            }
            pVariableClass = _VARS.VARS[varIndex];
            if (level >= pVariableClass.LevelCount)
                throw new ArgumentOutOfRangeException("level", level, "参数错误：level超出当前变量的LEVELS。");
            pBinaryBlockIndex += level * BaseArraySize;
            return pBinaryBlockIndex + timeIndex * TimePageSize;
        }
        #endregion

        #region 属性

        /// <summary>
        /// 
        /// </summary>
        public bool IsCTLFileLoaded
        {
            get
            {
                return _IsCTLFileLoaded;
            }
        }
        /// <summary>
        /// 变量集合。
        /// </summary>
        public IVARS VARS
        {
            get
            {
                if (_VARS == null)
                    throw new ArgumentNullException("VARS", "VARS参数为空。");
                else
                    return _VARS;
            }
        }
        /// <summary>
        /// 定义格点数据的x维或者经向格点值。
        /// </summary>
        public IXDEF XDEF
        {
            get
            {
                if (_XDEF == null)
                    throw new ArgumentNullException("XDEF", "XDEF参数为空。");
                else
                    return _XDEF;
            }
        }
        /// <summary>
        /// 定义格点数据的y维或者纬向格点值。
        /// </summary>
        public IYDEF YDEF
        {
            get
            {
                if (_YDEF == null)
                    throw new ArgumentNullException("YDEF", "YDEF参数为空。");
                else
                    return _YDEF;
            }
        }
        /// <summary>
        /// 定义格点数据的z维格点值。
        /// </summary>
        public IZDEF ZDEF
        {
            get
            {
                if (_ZDEF == null)
                    throw new ArgumentNullException("ZDEF", "ZDEF参数为空。");
                else
                    return _ZDEF;
            }
        }
        /// <summary>
        /// 定义格点数据显示到地图上的投影方式。
        /// </summary>
        public IPDEF PDEF
        {
            get
            {
                if (_PDEF == null)
                    throw new ArgumentNullException("PDEF", "PDEF参数为空。");
                else
                    return _PDEF;
            }
        }
        /// <summary>
        /// 定义格点数据的T维格点值。
        /// </summary>
        public ITDEF TDEF
        {
            get
            {
                if (_TDEF == null)
                    throw new ArgumentNullException("TDEF", "TDEF参数为空。");
                else
                    return _TDEF;
            }
        }
        /// <summary>
        /// 标识未定义或者缺失数据值。
        /// </summary>
        public float UNDEF
        {
            get
            {
                if (_Undef == float.NaN)
                    throw new ArgumentNullException("UNDEF", "UNDEF参数为空。");
                else
                    return _Undef;
            }
        }
        /// <summary>
        /// 标识对应数据文件的描述。
        /// </summary>
        public string DSET
        {
            get
            {
                if (string.IsNullOrEmpty(_DSET))
                    throw new ArgumentNullException("DEST", "DEST参数为空。");
                else
                    return _DSET;
            }
        }
        /// <summary>
        /// 对数据集的相关描述
        /// </summary>
        public string Title
        {
            get
            {
                if (string.IsNullOrEmpty(_Title))
                    throw new ArgumentNullException("Title", "Title参数为空。");
                else
                    return _Title;
            }
        }
        /// <summary>
        /// 控制处理数据文件方式的变量。替换了旧版的FORMAT记录。
        /// </summary>
        public IOptions Options
        {
            get
            {
                if (_Options == null)
                    throw new ArgumentNullException("Options", "Options参数为空。");
                else
                    return _Options;
            }
        }
        /// <summary>
        /// 列数（纬向）
        /// </summary>
        public int XSize
        {
            get
            {
                if (_PDEF != null)
                    switch (this._PDEF.Pro_Type)
                    {
                        case Pro_Type.LCC:
                            return ((ILCC_PDEF)_PDEF).ISize;
                        case Pro_Type.LCCR:
                            return ((ILCCR_PDEF)_PDEF).ISize;
                        default:
                            throw new ArgumentUndealException("参数错误：未对该投影类型进行处理。", new ArgumentException("参数错误：未处理该参数。", "this._PDEF.Pro_Type"));
                    }
                else
                {
                    return _XDEF.XSize;
                }
            }
        }
        /// <summary>
        /// 行数（经向）
        /// </summary>
        public int YSize
        {
            get
            {
                if (_PDEF != null)
                    switch (this._PDEF.Pro_Type)
                    {
                        case Pro_Type.LCC:
                            return ((ILCC_PDEF)_PDEF).JSize;
                        case Pro_Type.LCCR:
                            return ((ILCCR_PDEF)_PDEF).JSize;
                        default:
                            throw new ArgumentUndealException("参数错误：未对该投影类型进行处理。", new ArgumentException("参数错误：未处理该参数。", "this._PDEF.Pro_Type"));
                    }
                else
                    return _YDEF.YSize;
            }
        }
        /// <summary>
        /// 基础数据块大小（bytes）
        /// </summary>
        public int BaseArraySize
        {
            get
            {
                return XSize * YSize * 4;
            }
        }


        /// <summary>
        /// 时次基础数据块大小（bytes）
        /// </summary>
        public int TimePageSize
        {
            get
            {
                return _VARS.BlocksCount * BaseArraySize;
            }
        }

        /// <summary>
        /// 原点纬度
        /// </summary>
        public double Orgin_Lat
        {
            set
            {
                _Orgin_Lat = value;
            }
            get
            {
                if (Double.IsNaN(_Orgin_Lat))
                {
                    switch (this.PDEF.Pro_Type)
                    {
                        case Pro_Type.LCC:
                            return ((ILCC_PDEF)PDEF).LatRef;
                        case Pro_Type.LCCR:
                            return ((ILCCR_PDEF)PDEF).LatRef;
                        default:
                            throw new ArgumentUndealException("参数错误：未对该投影类型进行处理。", new ArgumentException("参数错误：未处理该参数。", "this._PDEF.Pro_Type"));
                    }
                }
                    
                else
                    return _Orgin_Lat;
            }
        }
        #endregion
    }

}
