using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UNISTInputDisplay
{
    enum ButtonState
    {
        Nothing = 0b0000,
        C = 0b0001,
        B = 0b0010,
        BC = 0b0011,
        A = 0b0100,
        AC = 0b0101,
        AB = 0b0110,
        ABC = 0b0111,
        D = 0b1000,
        DC = 0b1001,
        DB = 0b1010,
        DBC = 0b1011,
        DA = 0b1100,
        DAC = 0b1101,
        DAB = 0b1110,
        DABC = 0b1111,
    }
}
