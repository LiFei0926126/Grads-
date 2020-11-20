#define Debug1
//#define Debug2


using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using Readearth.GrADSBinary;
using System.IO;

namespace Test
{
    

    class Program
    {
        static void Main(string[] args)
        {
            Stopwatch sw = new Stopwatch();
            sw.Reset();
            sw.Start();
#if Debug1
            //数据文件与CTL描述文件的文件名不匹配的数据加载方式
            IBinaryData pBinaryData = new BinaryDataClass ( @"F:\ClassLib\Grads二进制文件解析\Test\bin\Debug\ModelData\wrfd01_2016-12-01_16.ctl" );

            ICTLInfo pctl = new CTLInfoClass();
            pctl.LoadCTL( @"F:\ClassLib\Grads二进制文件解析\Test\bin\Debug\ModelData\wrfd01_2016-12-01_16.ctl" );

            //pBinaryData.CTLInfo = pctl;
            //float[,,,] data = pBinaryData.ReadData ( "PM25" );
            //pBinaryData.Export2ASC ( "slp", i, 0, @"result\slp_" + i.ToString ( "000" ) + ".asc", true );
#endif

#if Debug2
            //数据文件与CTL描述文件的文件名匹配的数据加载方式
            FileStream fs = new FileStream(@"ModelData\wrfd01_2016-12-01_16.dat", FileMode.Open, FileAccess.Read, FileShare.Read);
            FileStream fs1 = new FileStream(@"ModelData\wrfd01_2016-12-01_16.dat", FileMode.Open, FileAccess.Read, FileShare.Read);


            IBinaryData pBinaryData = new BinaryDataClass(@"ModelData\wrfd01_2016-12-01_16.dat");
#endif

            //读取数据
            //float[,] data = pBinaryData.ReadData("slp", 0, 0);
            //输出ASC
            //for (int i = 0; i < pBinaryData.CTLInfo.TDEF.TSize; i++)
            //{
            //    bool res = pBinaryData.Export2ASC("slp", i, 0, @"result\slp_"+i.ToString("000")+".asc", true);

            //    long ms = sw.ElapsedMilliseconds;
            //    Console.WriteLine(string.Format("耗时：{0}ms。", ms));
            //}
            sw.Stop();
            Console.ReadKey();
        }
    }
}
