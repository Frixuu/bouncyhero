using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using UnityEngine;

namespace Frixu.BouncyHero.Extensions
{
    /// <summary> Methods that make work with AsyncOperations easier. </summary>
    public static class AsyncOperationExtensions
    {
        /// <summary>
        /// Allows to use await keyword with AsyncOperations,
        /// such as LoadLevelAsync.
        /// </summary>
        public static TaskAwaiter GetAwaiter(this AsyncOperation operation)
        {
            var tcs = new TaskCompletionSource<object>();
            operation.completed += delegate { tcs.TrySetResult(null); };
            if (operation.isDone) tcs.TrySetResult(null);
            return ((Task)tcs.Task).GetAwaiter();
        }
    }
}
