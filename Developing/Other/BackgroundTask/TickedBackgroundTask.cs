using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Developing.Other
{
    internal class TickedBackgroundTask : BackgroundTask
    {
        private int _ticks;

        /// <inheritdoc />
        public TickedBackgroundTask(double interval, int ticks) : this(TimeSpan.FromMilliseconds(interval), ticks)
        {
        }
        /// <inheritdoc />
        public TickedBackgroundTask(TimeSpan interval, int ticks) : this(interval, ticks, new CancellationTokenSource())
        {
            
        }
        public TickedBackgroundTask(TimeSpan interval, int ticks, CancellationTokenSource cts) : base(interval, cts)
        {
            _ticks = ticks;
        }

        public override void Start(Action func)
        {
            _timerTask = DoWorkAsync(func);
        }

        /// <inheritdoc />
        protected override async Task DoWorkAsync(Action func)
        {
            try
            {
                while (_ticks-- > 0 && await _timer.WaitForNextTickAsync(_cts.Token))
                    func();
            }
            catch (OperationCanceledException)
            {
            }
            finally
            {
                _cts.Cancel();
            }
        }
    }
}
