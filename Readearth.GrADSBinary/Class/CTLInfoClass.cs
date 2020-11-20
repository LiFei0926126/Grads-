#define test

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

using Projection;

using Readearth.GrADSBinary.DEF;

namespace Readearth.GrADSBinary
{
    /// <summary>
    /// CTL文件解析类
    /// </summary>
    public class CTLInfoClass : object, ICTLInfo
    {
        #region 常量
        private const string PATTERN_WithNum = "(^[x|y|z]def)\\s+[1-9]{1}[0-9]{0,}";
        #endregion

        #region 成员变量
        private string _ctlFielPath;
        private string _Dset = string.Empty, _Title = string.Empty;
        private float _Undef = float.NaN;
        private IOptions _Options = null;
        private IPDef _PDef = null;
        private IXDef _XDef = null;
        private IYDef _YDef = null;
        private IZDef _ZDef = null;
        private ITDef _TDef = null;
        private IVars _Vars = null;
        private IEDef _EDef = null;
        private LamProjection lp;
        private bool _IsCTLFileLoaded = false;
        private double _Orgin_Lat = double.NaN;
        #endregion

        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="strCTLPath"></param>
        public CTLInfoClass(string strCTLPath = "")
        {
            if (!string.IsNullOrEmpty ( strCTLPath ))
            {
                _ctlFielPath = strCTLPath;
                LoadCTL ( _ctlFielPath );
            }
        }
        #endregion

        #region 私有函数
        private void Prase(string strPara)
        {
            string[] paras = Regex.Split ( strPara.Trim ( ) , "\\s+" );

            bool useTemplates = false;

            switch (paras[0].ToLower ( ))
            {
                case "dset":
                    _Dset = paras[1].Remove ( 0 , 1 );
                    useTemplates = _Dset.Contains ( "%" );
                    break;
                case "title":
                    List<string> tmp = new List<string> ( paras );
                    tmp.RemoveAt ( 0 );
                    _Title = string.Join ( " " , tmp.ToArray ( ) );
                    break;
                case "options":
                    tmp = new List<string> ( paras );
                    _Options = new Options ( string.Join ( " " , tmp.ToArray ( ) ) );
                    if (useTemplates != _Options.UseTemplates)
                        throw new Exception ( "CTL 文件描述错误。" );
                    break;
                case "undef":
                    _Undef = float.Parse ( paras[1] );
                    break;
                case "pdef":
                    _PDef = new PDefClass ( string.Join ( " " , paras ) );

                    break;
                case "xdef":
                    _XDef = new XDefClass ( string.Join ( " " , paras ) );
                    break;
                case "ydef":
                    _YDef = new YDefClass ( string.Join ( " " , paras ) );
                    break;
                case "zdef":
                    _ZDef = new ZDefClass ( string.Join ( " " , paras ) );
                    break;
                case "tdef":
                    _TDef = new TDefClass ( string.Join ( " " , paras ) );
                    break;
                case "vars":
                    _Vars = new VarsClass ( strPara );
                    break;
                case "edef":
                    _EDef = new EDefClass ( strPara );
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
            StreamReader sr = new StreamReader ( _ctlFielPath );
            string[] strLines = sr.ReadToEnd ( ).Split ( new string[] { "\n" } , StringSplitOptions.None );
            sr.Close ( );

            for (int i = 0 ; i < strLines.Length ; i++)
            {

                Match m = Regex.Match ( strLines[i] , EDefClass.StrRegexBegin + "|" + VarsClass.StrRegexBegin , RegexOptions.IgnoreCase );
                //EDEF 和 VARS的特殊处理
                if (m.Success)
                {
                    StringBuilder sb = new StringBuilder ( strLines[i] + "  " );
                    if (!Regex.IsMatch ( strLines[i] , "NAMES" ))
                    {
                        while (!Regex.IsMatch ( strLines[i] , "^END" + m.Value , RegexOptions.IgnoreCase ))
                        {
                            sb.AppendLine ( " " + strLines[i] );
                            i++;
                        }
                        sb.AppendLine ( " " + strLines[i] );
                    }
                    Prase ( sb.ToString ( ) );
                    sb.Length = 0;
                }

                //XDEF、YDEF 和 ZDEF 的处理
                else if (Regex.IsMatch ( strLines[i] , PATTERN_WithNum , RegexOptions.IgnoreCase ))
                {
                    StringBuilder sb = new StringBuilder ( );
                    string[] des = Regex.Split ( strLines[i].Trim ( ) , "\\s+" );
                    int num = int.Parse ( des[1] );
                    while (Regex.Split ( sb.ToString ( ) , "\\s+" ).Length < des.Length + num)
                    {
                        sb.AppendLine ( strLines[i].Trim ( ' ' ) );
                        i++;
                    }
                    Prase ( sb.ToString ( ) );
                    sb.Length = 0;
                }
                
                //其余选项的处理
                else
                    Prase ( strLines[i].Trim ( ' ' ) );
            }

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

            if (File.Exists ( strCTLPath ))
            {
                _ctlFielPath = strCTLPath;
                return LoadCTL ( );
            }
            else
                throw new Exception ( "CTL文件不存在。" );
        }
        /// <summary>
        /// 根据经纬度计算行列索引号。
        /// </summary>
        /// <param name="lon">经度</param>
        /// <param name="lat">纬度</param>
        /// <returns></returns>
        public XYPoint GetXYIndex(double lon , double lat)
        {
            XYPoint pXYIndex = new XYPoint
            {
                Long = (float)lon ,
                Lat = (float)lat
            };

            IPDef pPDef = this.PDef;
            switch (pPDef.Pro_Type)
            {
                case Pro_Type.LCC:
                    ILcc_PDef pLCC_PDEF = pPDef as ILcc_PDef;
                    lp = new LamProjection ( new VectorPointXY ( pLCC_PDEF.SLon , Orgin_Lat ) , pLCC_PDEF.Struelat , pLCC_PDEF.Ntruelat , GeoRefEllipsoid.WGS_84 );
                    VectorPointXY xyRef = lp.GeoToProj ( new VectorPointXY ( pLCC_PDEF.LonRef , pLCC_PDEF.LatRef ) );
                    double xstart = xyRef.X + (0 - pLCC_PDEF.IRef) * pLCC_PDEF.DX;
                    double ystart = xyRef.Y + (0 - pLCC_PDEF.JRef) * pLCC_PDEF.DY;

                    VectorPointXY xy = lp.GeoToProj ( new VectorPointXY ( lon , lat ) );
                    double x = xy.X;
                    double y = xy.Y;

                    pXYIndex.ColIndex = (int)((x - xstart) / pLCC_PDEF.DX);
                    pXYIndex.RowIndex = (int)((y - ystart) / pLCC_PDEF.DY);
                    break;
                case Pro_Type.LCCR:
                    ILccr_PDef pLCCR_PDEF = pPDef as ILccr_PDef;
                    lp = new LamProjection ( new VectorPointXY ( pLCCR_PDEF.SLon , 0 ) , pLCCR_PDEF.Struelat , pLCCR_PDEF.Ntruelat , GeoRefEllipsoid.WGS_84 );
                    xyRef = lp.GeoToProj ( new VectorPointXY ( pLCCR_PDEF.LonRef , pLCCR_PDEF.LatRef ) );
                    xstart = (0 - pLCCR_PDEF.IRef) * pLCCR_PDEF.DX + xyRef.X;
                    ystart = (0 - pLCCR_PDEF.JRef) * pLCCR_PDEF.DY + xyRef.Y;

                    xy = lp.GeoToProj ( new VectorPointXY ( lon , lat ) );
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
        public int GetBinaryBlockIndex(string strVarName , int level)
        {
            int pBinaryBlockIndex = 0;
            VariableClass pVariableClass = null;
            for (int i = 0 ; i < _Vars.VarCount ; i++)
            {
                pVariableClass = _Vars.VARS[i];
                if (pVariableClass.VarName == strVarName)
                {
                    if (level >= pVariableClass.LevelCount)
                        throw new ArgumentOutOfRangeException ( "level" , level , "参数错误：level超出当前变量的LEVELS。" );
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
        public int GetBinaryBlockIndex(int varIndex , int level)
        {

            int pBinaryBlockIndex = 0;
            VariableClass pVariableClass = null;
            for (int i = 0 ; i < varIndex ; i++)
            {
                pVariableClass = _Vars.VARS[i];
                pBinaryBlockIndex += pVariableClass.LevelCount * BaseArraySize;
            }
            pVariableClass = _Vars.VARS[varIndex];
            if (level >= pVariableClass.LevelCount)
                throw new ArgumentOutOfRangeException ( "level" , level , "参数错误：level超出当前变量的LEVELS。" );
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
        public int GetBinaryBlockIndex(string strVarName , int timeIndex , int level)
        {
            int pBinaryBlockIndex = 0;
            VariableClass pVariableClass = null;
            for (int i = 0 ; i < _Vars.VarCount ; i++)
            {
                pVariableClass = _Vars.VARS[i];
                if (pVariableClass.VarName == strVarName)
                {
                    if (level >= pVariableClass.LevelCount)
                        throw new ArgumentOutOfRangeException ( "level" , level , "参数错误：level超出当前变量的LEVELS。" );
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
        public int GetBinaryBlockIndex(int varIndex , int timeIndex , int level)
        {

            int pBinaryBlockIndex = 0;
            VariableClass pVariableClass = null;
            for (int i = 0 ; i < varIndex ; i++)
            {
                pVariableClass = _Vars.VARS[i];
                pBinaryBlockIndex += pVariableClass.LevelCount * BaseArraySize;
            }
            pVariableClass = _Vars.VARS[varIndex];
            if (level >= pVariableClass.LevelCount)
                throw new ArgumentOutOfRangeException ( "level" , level , "参数错误：level超出当前变量的LEVELS。" );
            pBinaryBlockIndex += level * BaseArraySize;
            return pBinaryBlockIndex + timeIndex * TimePageSize;
        }
        #endregion

        #region 属性

        /// <summary>
        /// 
        /// </summary>
        public bool IsCTLFileLoaded => _IsCTLFileLoaded;
        /// <summary>
        /// 变量集合。
        /// </summary>
        public IVars Vars
        {
            get
            {
                if (_Vars == null)
                    throw new ArgumentNullException ( "VARS" , "VARS参数为空。" );
                else
                    return _Vars;
            }
        }
        /// <summary>
        /// 定义格点数据的x维或者经向格点值。
        /// </summary>
        public IXDef XDef
        {
            get
            {
                if (_XDef == null)
                    throw new ArgumentNullException ( "XDEF" , "XDEF参数为空。" );
                else
                    return _XDef;
            }
        }
        /// <summary>
        /// 定义格点数据的y维或者纬向格点值。
        /// </summary>
        public IYDef YDef
        {
            get
            {
                if (_YDef == null)
                    throw new ArgumentNullException ( "YDEF" , "YDEF参数为空。" );
                else
                    return _YDef;
            }
        }
        /// <summary>
        /// 定义格点数据的z维格点值。
        /// </summary>
        public IZDef ZDef
        {
            get
            {
                if (_ZDef == null)
                    throw new ArgumentNullException ( "ZDEF" , "ZDEF参数为空。" );
                else
                    return _ZDef;
            }
        }
        /// <summary>
        /// 定义格点数据显示到地图上的投影方式。
        /// </summary>
        public IPDef PDef
        {
            get
            {
                if (_PDef == null)
                    throw new ArgumentNullException ( "PDEF" , "PDEF参数为空。" );
                else
                    return _PDef;
            }
        }
        /// <summary>
        /// 定义格点数据的T维格点值。
        /// </summary>
        public ITDef TDef
        {
            get
            {
                if (_TDef == null)
                    throw new ArgumentNullException ( "TDEF" , "TDEF参数为空。" );
                else
                    return _TDef;
            }
        }
        /// <summary>
        /// 标识未定义或者缺失数据值。
        /// </summary>
        public float UNDef
        {
            get
            {
                if (_Undef == float.NaN)
                    throw new ArgumentNullException ( "UNDEF" , "UNDEF参数为空。" );
                else
                    return _Undef;
            }
        }
        /// <summary>
        /// 标识对应数据文件的描述。
        /// </summary>
        public string Dset
        {
            get
            {
                if (string.IsNullOrEmpty ( _Dset ))
                    throw new ArgumentNullException ( "DEST" , "DEST参数为空。" );
                else
                    return _Dset;
            }
        }
        /// <summary>
        /// 对数据集的相关描述
        /// </summary>
        public string Title
        {
            get
            {
                if (string.IsNullOrEmpty ( _Title ))
                    throw new ArgumentNullException ( "Title" , "Title参数为空。" );
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
                    throw new ArgumentNullException ( "Options" , "Options参数为空。" );
                else
                    return _Options;
            }
        }
        /// <summary>
        /// 定义格点数据的E维格点值。
        /// </summary>
        public IEDef EDef
        {
            get
            {
                if (_EDef == null)
                    throw new ArgumentNullException ( "EDef" , "EDef参数为空。" );
                else
                    return _EDef;
            }
        }
        /// <summary>
        /// 列数（纬向）
        /// </summary>
        public int XSize
        {
            get
            {
                if (_PDef != null)
                    return _PDef.ISize;
                else
                {
                    return _XDef.XSize;
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
                if (_PDef != null)
                    return _PDef.JSize;
                else
                    return _YDef.YSize;
            }
        }
        /// <summary>
        /// 基础数据块大小（bytes）
        /// </summary>
        public int BaseArraySize => XSize * YSize * 4;


        /// <summary>
        /// 时次基础数据块大小（bytes）
        /// </summary>
        public int TimePageSize => _Vars.BlocksCount * BaseArraySize;

        /// <summary>
        /// 原点纬度
        /// </summary>
        public double Orgin_Lat
        {
            set => _Orgin_Lat = value;
            get
            {
                if (Double.IsNaN ( _Orgin_Lat ))
                {
                    switch (PDef.Pro_Type)
                    {
                        case Pro_Type.LCC:
                            return ((ILcc_PDef)PDef).LatRef;
                        case Pro_Type.LCCR:
                            return ((ILccr_PDef)PDef).LatRef;
                        default:
                            throw new ArgumentUndealException ( "参数错误：未对该投影类型进行处理。" , new ArgumentException ( "参数错误：未处理该参数。" , "PDEF.Pro_Type" ) );
                    }
                }

                else
                    return _Orgin_Lat;
            }
        }
       
        #endregion
    }

}
