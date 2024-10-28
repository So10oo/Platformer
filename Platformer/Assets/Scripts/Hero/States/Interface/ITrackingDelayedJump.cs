using UnityEngine;

public interface ITrackingDelayedJump
{
    public (bool, float) DelayedPressing { get; set; }

}

public static class TrackingDelayedJumpExtensions
{
    public static void SetDelayedJump(this ITrackingDelayedJump _classDelayedJump, bool value)
    {
        _classDelayedJump.DelayedPressing = (value, Time.time);
    }
}