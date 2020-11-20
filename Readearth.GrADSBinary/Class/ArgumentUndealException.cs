using System;

namespace Readearth.GrADSBinary
{
    /// <summary>
    /// 未处理参数异常
    /// </summary>
    public class ArgumentUndealException : ArgumentException
    {
        #region 成员变量

        private string _Msg = string.Empty;
        #endregion

        #region 构造函数
        /// <summary>
        /// 
        /// </summary>
        public ArgumentUndealException() : base ( )
        { }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public ArgumentUndealException(string message)
            : base ( message )
        {
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        public ArgumentUndealException(string message , Exception innerException)
            : base ( message , innerException )
        { }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="paramName"></param>
        public ArgumentUndealException(string message , string paramName)
            : base ( message , paramName )
        { }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="paramName"></param>
        /// <param name="innerException"></param>
        public ArgumentUndealException(string message , string paramName , Exception innerException)
            : base ( message , paramName , innerException )
        { }
        #endregion
    }
}
