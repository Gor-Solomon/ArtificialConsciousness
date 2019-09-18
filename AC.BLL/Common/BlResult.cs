using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AC.BLL.Common
{
    public class BlResult
    {
        public bool Succeed { get; protected set; }
        public BLErrorCodeTypeEnum ErrorCode { get; protected set; }
        public string ErrorMessage { get; protected set; }
        public Exception Exception { get; protected set; }

        public void Success()
        {
            Succeed = true;
        }

        public BlResult Fail(BLErrorCodeTypeEnum errorEnum)
        {
            return Fail(errorEnum, null, null);
        }

        public BlResult Fail(BLErrorCodeTypeEnum errorEnum, Exception ex)
        {
            return Fail(errorEnum, null, ex);
        }

        public BlResult Fail(string errorMessage)
        {
            return Fail(BLErrorCodeTypeEnum.Unknown, errorMessage, null);
        }

        public BlResult Fail(Exception exception)
        {
            return Fail(BLErrorCodeTypeEnum.Unknown, exception?.Message, exception);
        }

        public BlResult Fail(string errorMessage, Exception exception)
        {
            return Fail(BLErrorCodeTypeEnum.Unknown, errorMessage, exception);
        }

        public BlResult Fail(BLErrorCodeTypeEnum errorCode, string errorMessage, Exception exception)
        {
            Succeed = false;
            ErrorCode = errorCode;
            ErrorMessage = errorMessage;
            Exception = exception;

            return this;
        }
    }

    public class BlResult<T> : BlResult
    {
        public T Value { get; protected set; }

        public BlResult<T> Success(T value)
        {
            Succeed = true;
            Value = value;

            return this;
        }
    }
}
