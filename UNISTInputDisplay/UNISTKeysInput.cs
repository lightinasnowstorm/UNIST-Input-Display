using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Newtonsoft.Json;
using System.IO;
using Microsoft.CSharp;

namespace UNISTInputDisplay
{
    class UNISTKeysInput
    {
        public Keys Left = Keys.A, Right = Keys.D, Up = Keys.W, Down = Keys.S;

        public Keys A = Keys.U, B = Keys.I, C = Keys.K, D = Keys.J;

        public volatile bool LeftPressed, RightPressed, UpPressed, DownPressed;
        public volatile bool APressed, BPressed, CPressed, DPressed;

        private readonly InterceptKeys Interceptor;

        public UNISTKeysInput(string SettingsFilename)
        {
            Interceptor = new InterceptKeys(this);
            if (!File.Exists(SettingsFilename))
            {
                return;
            }
            try
            {
                dynamic KeySettings = JsonConvert.DeserializeObject(File.ReadAllText(SettingsFilename));
                Left = strToKey(KeySettings.Left.ToString());
                Right = strToKey(KeySettings.Right);
                Up = strToKey(KeySettings.Up);
                Down = strToKey(KeySettings.Down);
                A = strToKey(KeySettings.A);
                B = strToKey(KeySettings.B);
                C = strToKey(KeySettings.C);
                D = strToKey(KeySettings.D);
            }
            catch { }
            
        }

        public InputState GetCurrentButtonState()
        {
            KeyboardState keyState = Keyboard.GetState();
            int buttonState = 0b0000;
            buttonState |= APressed ? 0b0100 : 0;
            buttonState |= BPressed? 0b0010 : 0;
            buttonState |= CPressed ? 0b0001 : 0;
            buttonState |= DPressed ? 0b1000 : 0;

            int horiz_dir = 0b10;
            horiz_dir -= LeftPressed ? 0b1 : 0;
            horiz_dir += RightPressed ? 0b1 : 0;

            int vert_dir = 0b10_00;
            vert_dir -= DownPressed ? 0b100 : 0;
            vert_dir += UpPressed ? 0b100 : 0;

            int direction = horiz_dir + vert_dir;

            return new InputState((Directions)direction, (ButtonState)buttonState);
        }

        private Keys strToKey<T>(T keyString)
        => (Keys)Enum.Parse(typeof(Keys), keyString.ToString());
    }
}
