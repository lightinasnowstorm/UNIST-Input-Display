using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UNISTInputDisplay
{
    class InputState
    {
        public InputState(Directions direction, ButtonState buttonState, int frameCount = 1)
        {
            this.buttonState = buttonState;
            this.direction = direction;
            this.frameCount = frameCount;
        }

        public static bool operator ==(InputState left, InputState right)
        {
            return left.direction == right.direction
                && left.buttonState == right.buttonState;
        }
        public static bool operator !=(InputState left, InputState right)
        {
            return !(left == right);
        }

        public readonly Directions direction;
        public readonly ButtonState buttonState;
        public int frameCount;
    }
}
