using System;

namespace DialogueSystem.Editor
{
    public class GlobalCounterErrors
    {
        public event Action<bool> StateErrorChange;

        int value = 0;

        public void AddErrors()
        {
            if (++value == 1)
                StateErrorChange?.Invoke(true);
        }

        public void SubstractErrors()
        {
            if (--value == 0)
                StateErrorChange?.Invoke(false);
        }

        public void ResetErrors()
        {
            value = 0;
        }

    }
}