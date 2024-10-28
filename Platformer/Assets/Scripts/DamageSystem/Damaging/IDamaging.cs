using System.Collections.Generic;

public interface IDamaging
{
    public int Value { get; }
    public List<IHealthEffect> Effects { get; }

}

