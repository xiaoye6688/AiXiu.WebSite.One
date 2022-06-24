using System;

namespace AiXiu.Model
{
    /// <summary>
    /// 返回结果类
    /// </summary>
    public class OperResult
    {
        /// <summary>
        /// 状态码
        /// </summary>
        public StatusCode StatusCode { get; private set; }

        /// <summary>
        /// 消息
        /// </summary>
        public string Message { get; private set; }

        /// <summary>
        /// 异常
        /// </summary>
        public Exception Exception { get; private set; }

        /// <summary>
        /// 操作成功
        /// </summary>
        /// <returns></returns>
        public static OperResult Succeed()
        {
            return new OperResult()
            {
                StatusCode = StatusCode.Succeed,
                Message = "操作成功！"
            };
        }

        /// <summary>
        /// 操作失败
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public static OperResult Failed(string message = null)
        {
            return new OperResult()
            {
                StatusCode = StatusCode.Failed,
                Message = message ?? "操作失败！"
            };
        }

        /// <summary>
        /// 操作异常
        /// </summary>
        /// <param name="exception"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public static OperResult Wrong(Exception exception, string message = null)
        {
            return new OperResult()
            {
                StatusCode = StatusCode.Wrong,
                Message = message ?? exception.Message,
                Exception = exception
            };
        }
    }

    /// <summary>
    /// 带返回值的返回结果类
    /// </summary>
    public class OperResult<T>
    {
        /// <summary>
        /// 状态码
        /// </summary>
        public StatusCode StatusCode { get; protected set; }

        /// <summary>
        /// 结果对象
        /// </summary>
        public T Result { get; protected set; }

        /// <summary>
        /// 消息
        /// </summary>
        public string Message { get; protected set; }

        /// <summary>
        /// 异常
        /// </summary>
        public Exception Exception { get; protected set; }

        /// <summary>
        /// 读取结果
        /// </summary>
        /// <returns></returns>
        public T ReadResult()
        {
            if (Result is T)
            {
                return (T)Result;
            }
            else
            {
                return default(T);
            }
        }

        /// <summary>
        /// 操作成功
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        public static OperResult<T> Succeed(T result)
        {
            return new OperResult<T>()
            {
                StatusCode = StatusCode.Succeed,
                Result = result,
                Message = "操作成功！"
            };
        }

        /// <summary>
        /// 操作失败
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public static OperResult<T> Failed(string message = null)
        {
            return new OperResult<T>()
            {
                StatusCode = StatusCode.Failed,
                Message = message ?? "操作失败！"
            };
        }

        /// <summary>
        /// 操作异常
        /// </summary>
        /// <param name="exception"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public static OperResult<T> Wrong(Exception exception, string message = null)
        {
            return new OperResult<T>()
            {
                StatusCode = StatusCode.Wrong,
                Message = message ?? exception.Message,
                Exception = exception
            };
        }
    }
}