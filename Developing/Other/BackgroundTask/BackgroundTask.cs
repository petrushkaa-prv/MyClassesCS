using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Developing.Other
{
    internal class BackgroundTask
    {
        protected Task? _timerTask;
        protected readonly PeriodicTimer _timer;
        protected readonly CancellationTokenSource _cts;

        public BackgroundTask(double milliseconds) : this(TimeSpan.FromMilliseconds(milliseconds))
        {

        }
        public BackgroundTask(TimeSpan interval) : this(interval, new CancellationTokenSource())
        {
            
        }
        public BackgroundTask(TimeSpan interval, CancellationTokenSource cts)
        {
            _timer = new PeriodicTimer(interval);
            _cts = cts;
        }

        public virtual void Start(Action func)
        {
            _timerTask = DoWorkAsync(func);
        }

        protected virtual async Task DoWorkAsync(Action func)
        {
            try
            {
                while (await _timer.WaitForNextTickAsync(_cts.Token))
                {
                    func();
                }
            }
            catch (OperationCanceledException)
            {
            }
        }

        public async Task StopAsync()
        {
            if (_timerTask is null) return;
            
            await _timerTask;
            _cts.Dispose();
        }
    }
}
