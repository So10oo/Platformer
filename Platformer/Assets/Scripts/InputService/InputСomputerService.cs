using UnityEngine;

public class InputСomputerService : IInputService
{
    public bool GetButtonAttackDown()
    {
        return Input.GetButtonDown("Fire1");
        //return Input.GetKeyDown(KeyCode.Q);
    }

    public bool GetButtonJump()
    {
        return Input.GetKey(KeyCode.Space);
    }

    public bool GetButtonJumpDown()
    {
        return Input.GetKeyDown(KeyCode.Space);
    }

    public bool GetButtonJumpUp()
    {
        return Input.GetKeyUp(KeyCode.Space);
    }

    public float GetHorizontalMove()
    {
        return Input.GetAxis("Horizontal");
    }

    public float GetVerticalMove()
    {
        return Input.GetAxis("Vertical");
    }

    public bool GetButtonDashDown()
    {
        return Input.GetKeyDown(KeyCode.LeftShift);
    }

    public bool GetButtonInteraction()
    {
        return Input.GetKeyDown(KeyCode.F);
    }
}
