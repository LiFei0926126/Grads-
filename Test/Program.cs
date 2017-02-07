//#define Debug1
#define Debug2


using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using Readearth.GrADSBinary;

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
            IBinaryData pBinaryData = new BinaryDataClass(@"ModelData\201612011200_004_d01.dat");

            ICTLInfo pctl = new CTLInfoClass();
            pctl.LoadCTL(@"ModelData\201612011200_004_d01 - 副本.ctl");

            pBinaryData.CTLInfo = pctl;
#endif

#if Debug2
            //数据文件与CTL描述文件的文件名匹配的数据加载方式
            IBinaryData pBinaryData = new BinaryDataClass(@"ModelData\201612011200_004_d01.dat");
#endif

            //读取数据
            float[,] data = pBinaryData.ReadData("slp", 0, 0);
            //输出ASC
            bool res = pBinaryData.Export2ASC(data, @"result\slp.asc", true);
            sw.Stop();
           long  ms = sw.ElapsedMilliseconds;
            Console.WriteLine(string.Format("耗时：{0}ms。", ms));
            Console.ReadKey();
        }
    }
}
