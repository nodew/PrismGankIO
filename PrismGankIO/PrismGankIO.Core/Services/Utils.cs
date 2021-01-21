using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PrismGankIO.Core.Services
{
    static class Utils
    {
        public static async Task<T> RetryAsync<T>(
            Func<Task<T>> runTask, 
            Action<Exception> handleException,
            int count = 3, 
            CancellationToken cancellationToken = default)
        {
            T result = default;
            for (int i = 0; i < count; i++)
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    cancellationToken.ThrowIfCancellationRequested();
                }

                try
                {
                    result = await runTask();
                    break;
                } 
                catch (Exception e)
                {
                    if (i == count - 1)
                    {
                        handleException(e);
                        throw e;
                    }
                }
            }

            return result;
        }
    }
}
