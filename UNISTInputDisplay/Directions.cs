using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UNISTInputDisplay
{
    enum HorizontalDirections
    {
        Away = 0b00_01,
        Neutral = 0b00_10,
        Towards = 0b00_11
    }

    enum VerticalDirections
    {
        Down = 0b01_00,
        Middle = 0b10_00,
        Up = 0b11_00
    }

    enum Directions
    {
        // eights fours twos ones
        AwayDown = 0b01_01, //5
        NeutralDown = 0b01_10,//6
        TowardsDown = 0b01_11,//7
        AwayMiddle = 0b10_01,//9
        NeutralMiddle = 0b10_10,//10 //no display
        TowardsMiddle = 0b10_11,//11
        AwayUp = 0b11_01,//13
        NeutralUp = 0b11_10,//14
        TowardsUp = 0b11_11//15

    }
}
