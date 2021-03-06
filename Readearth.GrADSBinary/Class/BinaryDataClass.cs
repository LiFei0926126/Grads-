﻿using System;
using System.IO;
using System.Text;

using Projection;

using Readearth.GrADSBinary.DEF;

namespace Readearth.GrADSBinary
{
    /// <summary>
    /// 二进制文件操作类
    /// </summary>
    public class BinaryDataClass : Object, IBinaryData
    {
        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="ctlFilePath">CTL 描述文件</param>
        public BinaryDataClass(string ctlFilePath)
        {
            if (!File.Exists ( ctlFilePath ))
                throw new FileNotFoundException ( "文件错误：未能找到该文件。" , ctlFilePath );
            else
            {
                _CTLInfo = new CTLInfoClass ( ctlFilePath );

                if (_CTLInfo.Options.UseTemplates)
                { }
                else
                {
                    _DataFilePath = Path.Combine ( Path.GetDirectoryName ( ctlFilePath ) , _CTLInfo.Dset );
                }
                VerifyData ( );
            }

        }
        #endregion

        #region 成员变量
        /// <summary>
        /// 数据文件路径
        /// </summary>
        private string _DataFilePath = string.Empty;

        /// <summary>
        /// 数据CTL描述文件路径
        /// </summary>
        private string _CTLFilePath = string.Empty;

        /// <summary>
        /// CTL描述文件信息
        /// </summary>
        private ICTLInfo _CTLInfo = null;

        /// <summary>
        /// 数据验证结果
        /// </summary>
        private bool _DataVerifyResult = false;

        /// <summary>
        /// 是否进行了数据验证
        /// </summary>
        private bool _ISDataVerify = false;

        #endregion

        #region 属性
        /// <summary>
        /// CTL信息
        /// </summary>
        public ICTLInfo CTLInfo
        {
            get
            {
                if (_CTLInfo == null)
                    throw new Exception ( "该属性为空。" );
                else
                    return _CTLInfo;
            }
            set => _CTLInfo = value;
        }
        /// <summary>
        /// 数据验证结果
        /// </summary>
        public bool VerifyDataResult => _DataVerifyResult;
        #endregion


        #region 公有方法
        /// <summary>
        /// 验证数据
        /// </summary>
        /// <returns></returns>
        public bool VerifyData()
        {
            if (_CTLInfo == null)
                throw new ArgumentNullException ( "CTLInfo" , "参数错误：没有CTL描述信息，无法验证数据正确性。" );
            else if (string.IsNullOrEmpty ( _DataFilePath ))
                throw new ArgumentNullException ( "DataFilePath" , "参数错误：没有指定数据文件路径，无法验证数据正确性。" );
            else
            {
                Stream s = File.Open ( _DataFilePath , FileMode.Open , FileAccess.Read , FileShare.Read );
                long dataLength = s.Length;
                s.Close ( );

                if (CTLInfo.TimePageSize == dataLength / CTLInfo.TDef.TSize)
                    _DataVerifyResult = true;
                else
                    _DataVerifyResult = false;
            }
            _ISDataVerify = true;
            return _DataVerifyResult;
        }

        /// <summary>
        /// 读取数据
        /// </summary>
        /// <param name="VarName">要素</param>
        /// <returns>返回数据数组 [time(时),level(层),y(行),x(列)] </returns>
        public float[,,,] ReadData(string VarName)
        {
            if (!_ISDataVerify)
                VerifyData ( );
            if (_DataVerifyResult)
            {
                #region Read Data
                VariableClass var = null;
                var = _CTLInfo.Vars.GetVariableByName ( VarName , true );

                if (var == null)
                    throw new Exception ( "错误的要素。" );

                float[,,,] data = new float[_CTLInfo.TDef.TSize , var.LevelCount , _CTLInfo.YSize , _CTLInfo.XSize];

                FileStream fs = new FileStream ( _DataFilePath , FileMode.Open , FileAccess.Read , FileShare.Read );

                BinaryReader br = new BinaryReader ( fs );
                for (int t = 0 ; t < _CTLInfo.TDef.TSize ; t++)
                {

                    int readBegin = _CTLInfo.GetBinaryBlockIndex ( VarName , t , 0 );
                    fs.Seek ( readBegin , SeekOrigin.Begin );

                    for (int m = 0 ; m < var.LevelCount ; m++)
                    {
                        for (int j = 0 ; j < _CTLInfo.YSize ; j++)
                        {
                            for (int i = 0 ; i < _CTLInfo.XSize ; i++)
                            {
                                byte[] bytes = br.ReadBytes ( 4 );

                                data[t , m , j , i] = Convert2Float ( bytes );
                            }
                        }
                    }
                }

                br.Close ( );
                fs.Close ( );
                fs.Dispose ( );
                #endregion

                return data;
            }
            else
                throw new Exception ( "数据验证不通过。" );

        }

        /// <summary>
        /// 读取数据
        /// </summary>
        /// <param name="VarName">要素</param>
        /// <param name="timeIndex">时间维索引</param>
        /// <returns>返回数据数组 [level(层),y(行),x(列)] </returns>
        public float[,,] ReadData(string VarName , int timeIndex)
        {
            if (!_ISDataVerify)
                VerifyData ( );
            if (_DataVerifyResult)
            {

                #region Read Data 
                VariableClass var = null;
                var = _CTLInfo.Vars.GetVariableByName ( VarName , true );

                if (var == null)
                    throw new Exception ( "错误的要素。" );

                float[,,] data = new float[var.LevelCount , _CTLInfo.YSize , _CTLInfo.XSize];

                FileStream fs = new FileStream ( _DataFilePath , FileMode.Open , FileAccess.Read , FileShare.Read );
                int readBegin = _CTLInfo.GetBinaryBlockIndex ( VarName , timeIndex , 0 );
                fs.Seek ( readBegin , SeekOrigin.Begin );
                BinaryReader br = new BinaryReader ( fs );
                for (int m = 0 ; m < var.LevelCount ; m++)
                {
                    for (int j = 0 ; j < _CTLInfo.YSize ; j++)
                    {
                        for (int i = 0 ; i < _CTLInfo.XSize ; i++)
                        {
                            byte[] bytes = br.ReadBytes ( 4 );


                            data[m , j , i] = Convert2Float ( bytes );
                        }
                    }
                }

                br.Close ( );
                fs.Close ( );
                fs.Dispose ( );
                #endregion
                return data;
            }
            else
                throw new Exception ( "数据验证不通过。" );
        }
        /// <summary>
        /// 读取数据
        /// </summary>
        /// <param name="VarName">要素</param>
        /// <param name="timeIndex">时间维索引</param>
        /// <param name="Level">层次</param>
        /// <returns>返回数据数组 [y(行),x(列)] </returns>
        public float[,] ReadData(string VarName , int timeIndex , int Level)
        {
            if (!_ISDataVerify)
                VerifyData ( );
            if (_DataVerifyResult)
            {
                #region Read Data 
                VariableClass var = null;
                var = _CTLInfo.Vars.GetVariableByName ( VarName , true );

                if (var == null)
                    throw new Exception ( "错误的要素。" );

                float[,] data = new float[_CTLInfo.YSize , _CTLInfo.XSize];

                FileStream fs = new FileStream ( _DataFilePath , FileMode.Open , FileAccess.Read , FileShare.Read );
                int readBegin = _CTLInfo.GetBinaryBlockIndex ( VarName , timeIndex , 0 );
                fs.Seek ( readBegin , SeekOrigin.Begin );
                BinaryReader br = new BinaryReader ( fs );

                for (int j = 0 ; j < _CTLInfo.YSize ; j++)
                {
                    for (int i = 0 ; i < _CTLInfo.XSize ; i++)
                    {
                        byte[] bytes = br.ReadBytes ( 4 );

                        data[j , i] = Convert2Float ( bytes );
                    }
                }
                br.Close ( );
                fs.Close ( );
                fs.Dispose ( );

                #endregion
                return data;
            }
            else
                throw new Exception ( "数据验证不通过。" );
        }
        /// <summary>
        /// 读取数据
        /// </summary>
        /// <param name="VarName">要素</param>
        /// <param name="timeIndex">时间维索引</param>
        /// <param name="Level">层次</param>
        /// <param name="iIndex">列索引</param>
        /// <param name="jIndex">行索引</param>
        /// <returns>返回数据</returns>
        public float ReadData(string VarName , int timeIndex , int Level , int iIndex , int jIndex)
        {
            if (!_ISDataVerify)
                VerifyData ( );
            if (_DataVerifyResult)
            {
                #region Read Data 
                VariableClass var = null;
                var = _CTLInfo.Vars.GetVariableByName ( VarName , true );
                float data = -9999.0f;

                FileStream fs = new FileStream ( _DataFilePath , FileMode.Open , FileAccess.Read , FileShare.Read );
                int readBegin = _CTLInfo.GetBinaryBlockIndex ( VarName , timeIndex , Level ) + 4 * (jIndex * _CTLInfo.XSize + iIndex);
                fs.Seek ( readBegin , SeekOrigin.Begin );
                BinaryReader br = new BinaryReader ( fs );

                byte[] bytes = br.ReadBytes ( 4 );

                data = Convert2Float ( bytes );

                br.Close ( );
                fs.Close ( );
                fs.Dispose ( );
                #endregion
                return data;
            }
            else
                throw new Exception ( "数据验证不通过。" );
        }
        /// <summary>
        /// 读取数据
        /// </summary>
        /// <param name="VarName">要素</param>
        /// <param name="timeIndex">时间维索引</param>
        /// <param name="Level">层次</param>
        /// <param name="lon">目标点经度</param>
        /// <param name="lat">目标点纬度</param>
        /// <returns>返回数据</returns>
        public float ReadData(string VarName , int timeIndex , int Level , double lon , double lat)
        {
            XYPoint nearestPoint = CTLInfo.GetXYIndex ( lon , lat );

            return ReadData ( VarName , timeIndex , Level , nearestPoint.ColIndex , nearestPoint.RowIndex );
        }
        /// <summary>
        /// 输出到ASC文件。带prj文件。
        /// </summary>
        /// <param name="VarName">变量名。</param>
        /// <param name="timeIndex">时间维索引</param>
        /// <param name="Level">Z维索引。</param>
        /// <param name="outputPath">输出文件完整路径。</param>
        /// <param name="isPrjWith">是否输出prj文件</param>
        /// <returns>返回输出是否成功。</returns>
        /// <exception cref="Exception">异常</exception>
        public bool Export2ASC(string VarName , int timeIndex , int Level , string outputPath , bool isPrjWith)
        {
            float[,] data = ReadData ( VarName , timeIndex , Level );
            return Export2ASC ( data , outputPath , isPrjWith );
        }
        /// <summary>
        /// 输出到ASC文件。带prj文件。
        /// </summary>
        /// <param name="data">变量数组。</param>
        /// <param name="outputPath">输出文件完整路径。</param>
        /// <param name="isPrjWith">是否输出PRJ文件。</param>
        /// <returns>返回输出是否成功。</returns>
        /// <exception cref="Exception">异常</exception>
        public bool Export2ASC(float[,] data , string outputPath , bool isPrjWith)
        {
            bool res = false;
            try
            {
                //float[,] data = ReadData(VarName, timeIndex, Level);
                StringBuilder sb = new StringBuilder ( );
                #region ASC 头文件
                int col = -1, row = -1;
                float xllcorner = -1.0f, yllcorner = -1.0f, cellSize = -1.0f;
                if (CTLInfo.PDef.Pro_Type == Pro_Type.LCC)
                {
                    col = ((ILcc_PDef)CTLInfo.PDef).ISize;
                    row = ((ILcc_PDef)CTLInfo.PDef).JSize;

                    cellSize = (float)((ILcc_PDef)CTLInfo.PDef).DX;

                    LamProjection lp = new LamProjection ( new VectorPointXY ( ((ILcc_PDef)CTLInfo.PDef).SLon , CTLInfo.Orgin_Lat ) , ((ILcc_PDef)CTLInfo.PDef).Struelat , ((ILcc_PDef)CTLInfo.PDef).Ntruelat , GeoRefEllipsoid.WGS_84 );
                    VectorPointXY xyRef = lp.GeoToProj ( new VectorPointXY ( ((ILcc_PDef)CTLInfo.PDef).LonRef , ((ILcc_PDef)CTLInfo.PDef).LatRef ) );
                    xllcorner = (float)(xyRef.X - ((ILcc_PDef)CTLInfo.PDef).IRef * ((ILcc_PDef)CTLInfo.PDef).DX);
                    yllcorner = (float)(xyRef.Y - ((ILcc_PDef)CTLInfo.PDef).JRef * ((ILcc_PDef)CTLInfo.PDef).DY);

                    #region PRJ
                    if (isPrjWith)
                    {
                        sb.AppendLine ( "Projection    LAMBERT" );
                        sb.AppendLine ( "Datum         WGS84" );
                        sb.AppendLine ( "Spheroid      WGS84" );
                        sb.AppendLine ( "Units         METERS" );
                        sb.AppendLine ( "Zunits        NO" );
                        sb.AppendLine ( "Xshift        0.0" );
                        sb.AppendLine ( "Yshift        0.0" );
                        sb.AppendLine ( "Parameters" );
                        sb.AppendLine ( string.Format ( "  {0}  0  0.0 /* 1st standard parallel" , ((ILcc_PDef)CTLInfo.PDef).Ntruelat ) );
                        sb.AppendLine ( string.Format ( "  {0}  0  0.0 /* 2nd standard parallel" , ((ILcc_PDef)CTLInfo.PDef).Struelat ) );
                        sb.AppendLine ( string.Format ( "  {0}  0  0.0 /* central meridian" , ((ILcc_PDef)CTLInfo.PDef).SLon ) );
                        sb.AppendLine ( string.Format ( "  {0}  0  0.0 /* latitude of projection's origin" , ((ILcc_PDef)CTLInfo.PDef).LatRef ) );
                        sb.AppendLine ( "0.0 /* false easting (meters)" );
                        sb.AppendLine ( "0.0 /* false northing (meters)" );

                        StreamWriter swPrj = new StreamWriter ( outputPath.Replace ( ".asc" , ".prj" ) );
                        swPrj.Write ( sb.ToString ( ) );
                        swPrj.Flush ( );
                        sb.Length = 0;
                        swPrj.Close ( );
                        swPrj.Dispose ( );
                    }
                    #endregion
                }
                else if (CTLInfo.PDef.Pro_Type == Pro_Type.LCCR)
                {
                    col = ((ILccr_PDef)CTLInfo.PDef).ISize;
                    row = ((ILccr_PDef)CTLInfo.PDef).JSize;

                    cellSize = (float)((ILccr_PDef)CTLInfo.PDef).DX;

                    LamProjection lp = new LamProjection ( new VectorPointXY ( ((ILccr_PDef)CTLInfo.PDef).SLon , CTLInfo.Orgin_Lat ) , ((ILccr_PDef)CTLInfo.PDef).Struelat , ((ILccr_PDef)CTLInfo.PDef).Ntruelat , GeoRefEllipsoid.WGS_84 );
                    VectorPointXY xyRef = lp.GeoToProj ( new VectorPointXY ( ((ILccr_PDef)CTLInfo.PDef).LonRef , ((ILccr_PDef)CTLInfo.PDef).LatRef ) );
                    xllcorner = (float)(xyRef.X - ((ILccr_PDef)CTLInfo.PDef).IRef * ((ILccr_PDef)CTLInfo.PDef).DX);
                    yllcorner = (float)(xyRef.Y - ((ILccr_PDef)CTLInfo.PDef).JRef * ((ILccr_PDef)CTLInfo.PDef).DY);

                    #region PRJ
                    if (isPrjWith)
                    {
                        sb.AppendLine ( "Projection    LAMBERT" );
                        sb.AppendLine ( "Datum         WGS84" );
                        sb.AppendLine ( "Spheroid      WGS84" );
                        sb.AppendLine ( "Units         METERS" );
                        sb.AppendLine ( "Zunits        NO" );
                        sb.AppendLine ( "Xshift        0.0" );
                        sb.AppendLine ( "Yshift        0.0" );
                        sb.AppendLine ( "Parameters" );
                        sb.AppendLine ( string.Format ( "  {0}  0  0.0 /* 1st standard parallel" , ((ILccr_PDef)CTLInfo.PDef).Ntruelat ) );
                        sb.AppendLine ( string.Format ( "  {0}  0  0.0 /* 2nd standard parallel" , ((ILccr_PDef)CTLInfo.PDef).Struelat ) );
                        sb.AppendLine ( string.Format ( "  {0}  0  0.0 /* central meridian" , ((ILccr_PDef)CTLInfo.PDef).SLon ) );
                        sb.AppendLine ( string.Format ( "  {0}  0  0.0 /* latitude of projection's origin" , ((ILccr_PDef)CTLInfo.PDef).LatRef ) );
                        sb.AppendLine ( "0.0 /* false easting (meters)" );
                        sb.AppendLine ( "0.0 /* false northing (meters)" );

                        StreamWriter swPrj = new StreamWriter ( outputPath.Replace ( ".asc" , ".prj" ) );
                        swPrj.Write ( sb.ToString ( ) );
                        swPrj.Flush ( );
                        sb.Length = 0;
                        swPrj.Close ( );
                        swPrj.Dispose ( );
                    }
                    #endregion
                }


                sb.AppendLine ( "ncols         " + col );
                sb.AppendLine ( "nrows         " + row );
                sb.AppendLine ( "xllcorner     " + xllcorner );
                sb.AppendLine ( "yllcorner     " + yllcorner );
                sb.AppendLine ( "cellsize      " + cellSize );
                sb.AppendLine ( "NODATA_value  -9999" );
                #endregion

                #region ASC
                for (int iii = data.GetLength ( 0 ) - 1 ; iii >= 0 ; iii--)//逐列
                {
                    string[] rowDatas = new string[col];
                    for (int jjj = 0 ; jjj < data.GetLength ( 1 ) ; jjj++)//逐行
                    {
                        float var = data[iii , jjj];

                        rowDatas[jjj] = var.ToString ( "f7" );
                    }
                    sb.AppendLine ( GetConcatString ( rowDatas , " " ) );
                }
                if (Path.GetExtension ( outputPath ).ToLower ( ) != ".asc")
                    outputPath += ".asc";
                StreamWriter sw = new StreamWriter ( outputPath );
                sw.Write ( sb.ToString ( ) );
                sw.Flush ( );
                sb.Length = 0;
                sw.Close ( );
                sw.Dispose ( );
                #endregion


                res = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return res;
        }

        #endregion



        #region 私有方法
        private string GetConcatString(string[] data , string strConcat)
        {
            return string.Join ( strConcat , data );
        }

        /// <summary>
        /// 字节数组转换为float
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        private float Convert2Float(byte[] bytes)
        {
            byte[] tmp = new byte[4];
            if (CTLInfo.Options.Byteswapped)
                for (int mm = 0 ; mm < 4 ; mm++)
                    tmp[mm] = bytes[3 - mm];
            else
                tmp = bytes;
            float tmpValue = BitConverter.ToSingle ( tmp , 0 );
            if (tmpValue == CTLInfo.UNDef)
                tmpValue = -9999.0f;

            return tmpValue;
        }

        #endregion

     

    }
}
