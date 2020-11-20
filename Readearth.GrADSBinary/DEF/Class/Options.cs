using System;

namespace Readearth.GrADSBinary.DEF
{
    /// <summary>
    /// 可选项定义
    /// </summary>
    public class Options : IOptions
    {
        private bool _Byteswapped = false;
        private bool _UseTemplates = false;

        /// <summary>
        /// 可选项定义构造函数
        /// </summary>
        /// <param name="strOptions"></param>
        public Options(string strOptions)
        {
            if (string.IsNullOrEmpty ( strOptions ))
                throw new ArgumentNullException ( "参数错误：该参数不能为空。" );
            if (!strOptions.ToUpper ( ).Contains ( "OPTIONS" ))
                throw new ArgumentException ( "参数错误：该参数不是OPTIONS描述。" );

            _Byteswapped = strOptions.ToUpper ( ).Contains ( "BYTESWAPPED" );
            _UseTemplates = strOptions.ToUpper ( ).Contains ( "TEMPLATE" );
        }

        /// <summary>
        /// 是否大小端转换
        /// </summary>
        public bool Byteswapped => _Byteswapped;

        /// <summary>
        /// 是否使用了模板
        /// </summary>
        public bool UseTemplates => _UseTemplates;
    }
}
