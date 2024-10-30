using System.Collections.Generic;

public class Damaging : IDamaging
{
    public int Value { get; private set; }
    public List<IHealthEffect> Effects { get; private set; }
    public Damaging(int damage, List<IHealthEffect> effects = null)
    {
        Value  = damage;
        Effects = effects;
    }
    public Damaging(int damage, IHealthEffect effect)
    {
        Value = damage;
        Effects = new List<IHealthEffect>() { effect };
    }
    public Damaging(int damage)
    {
        Value = damage;
    }

}

