using Developing.Lists;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Developing.Other;

public class TaskRing
{
    private BackgroundTask _observer;
    private DlRingList<(Task task, int shared)> _ring;

    public TaskRing(int count, Action func)
    {
        _ring = new DlRingList<(Task task, int shared)>();

        for (int i = 0; i < count; i++)
        {
            _ring.AddFront((new Task(func), default));
        }

        _observer = new BackgroundTask(100);
    }

    public void Execute()
    {
        foreach (var (task, _) in _ring)
        {
            task.Start();
        }

        _observer.Start(() =>
        {
            var ptr = _ring.Head;
            while (true)
            {
                Task.Delay(1000);



                ptr = ptr.Next;
            }
        });
    }

    private class DlRingList<T> : DlList<T>
    {

        private void ReconnectEnds()
        {
            base.Rear.Next = base.Head;
            base.Head.Prev = base.Rear;
        }

        public new void AddFront(T val)
        {
            base.AddFront(val);

            ReconnectEnds();
        }

        public new void AddEnd(T val)
        {
            base.AddEnd(val);

            ReconnectEnds();
        }

        public new void RemoveEnd()
        {
            base.RemoveEnd();

            ReconnectEnds();
        }

        public new void RemoveFront()
        {
            base.RemoveFront();

            ReconnectEnds();
        }
    }
}
