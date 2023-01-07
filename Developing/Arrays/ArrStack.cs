using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Developing.GeneralInterfaces;
using Developing.Interfaces;

namespace Developing.Arrays;

public class ArrStack<T> : IEnumerable<T>, IMyCollection<T>, IOrderedContainer<T>, IMyStack<T>, IIndexed<T>
{
    private int _size;
    private int _current;
    private T[] _data;

    /// <inheritdoc />
    public int Size => _size;

    public int Count => _current;

    /// <inheritdoc />
    public bool IsEmpty => _current == 0;

    public ArrStack()
    {
        _size = 4;
        _current = 0;
        _data = new T[_size];
    }

    protected void Expand()
    {
        Span<T> span = _data;
        _data = new T[_size *= 2];
        span.TryCopyTo(_data);
    }

    /// <inheritdoc />
    public void Push(T elem)
    {
        if(_current + 1 == _size)
            this.Expand();

        _data[_current++] = elem;
    }

    /// <inheritdoc />
    public void Push(params T[] arr)
    {
        foreach (var val in arr)
            this.Push(val);
    }

    /// <inheritdoc />
    public T Peek => _data[_current];

    /// <inheritdoc />
    public T Pop()
    {
        _data[_current] = default;
        return _data[--_current];
    }

    public ArrStack<T> Copy()
    {
        var arr = new T[this._size];
        for (int i = 0; i < _data.Length; i++)
        {
            arr[i] = _data[i];
        }

        return new ArrStack<T>()
        {
            _current = this._current,
            _size = this._size,
            _data = arr
        };
    }

    /// <inheritdoc />
    public void Clear()
    {
        _size = 4;
        _current = 0;
        _data = new T[_size];
    }

    /// <inheritdoc />
    public IEnumerator<T> GetEnumerator()
    {
        for (int i = 0; i < _current; i++)
            yield return _data[i];
        
    }

    /// <inheritdoc />
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }



    /// <inheritdoc />
    public bool Contains(T element)
    {
        return _data.Contains(element);
    }

    /// <inheritdoc />
    public override bool Equals(object obj) 
        => this == (ArrStack<T>)obj;

    /// <inheritdoc />
    public override int GetHashCode() 
        => HashCode.Combine(this._data, this._size, this._current);

    /// <inheritdoc />
    public override string ToString()
    {
        return string.Join(" -> ", this);
    }

    private bool ValidateIndex(int idx)
    {
        if (IsEmpty || idx < 0 || idx >= _current)
            return false;

        return true;
    }

    /// <inheritdoc />
    public T this[int key]
    {
        get
        {
            if(!ValidateIndex(key))
                throw new IndexOutOfRangeException("Collection was empty or index was out of bounds");

            return _data[key];
        }
        set
        {
            if (!ValidateIndex(key))
                throw new IndexOutOfRangeException("Collection was empty or index was out of bounds");

            _data[key] = value;
        }
    }
}