using System.Collections.Generic;
using System.Linq;

public class Damaging : IDamaging
{
    public int Value { get; private set; }
    public List<IHealthEffect> Effects { get; private set; }
    public Damaging(int damage, IEnumerable<IHealthEffect> effects = null)
    {
        Value  = damage;
        Effects = effects.ToList();
    }
    public Damaging(int damage, params IHealthEffect[] effects)
    {
        Value = damage;
        Effects = effects.ToList();
    }
    public Damaging(int damage)
    {
        Value = damage;
    }

}

