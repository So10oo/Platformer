using System;


public class ReactiveProperty<T>
{
    private T _value = default;
    public T value 
    {
        get
        {
            return _value;
        }
        set
        {
             _value = value;
            OnPropertyChange?.Invoke(value);
        }

    }

    public Action<T> OnPropertyChange;

}

