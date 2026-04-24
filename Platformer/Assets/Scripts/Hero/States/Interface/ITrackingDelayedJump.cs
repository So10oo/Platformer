using UnityEngine;

public interface ITrackingDelayedJump
{
    private static (bool, float) DelayedPressing { get; set; }

    public float timeElapsed => Mathf.Abs(DelayedPressing.Item2 - Time.time);

    public bool isPressed => DelayedPressing.Item1;

    public void SetDelayedJump(bool value)
    {
       DelayedPressing = (value, Time.time);
    }
}

public static class TrackingDelayedJumpExtensions
{
    public static void SetDelayedJump(this ITrackingDelayedJump _classDelayedJump, bool value)
    {
        _classDelayedJump.SetDelayedJump(value);
    }
}