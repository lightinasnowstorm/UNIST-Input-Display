using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace UNISTInputDisplay
{



    public class Main : Game
    {
        readonly GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public Main()
        {
            graphics = new GraphicsDeviceManager(this)
            {
                PreferredBackBufferWidth = 270,
                PreferredBackBufferHeight = 600
            };
            IsMouseVisible = false;
            Content.RootDirectory = "Content";
        }

        readonly UNISTKeysInput KeysInput = new UNISTKeysInput("settings.json");

        protected override void Initialize()
        {

            base.Initialize();
        }

        Texture2D Arrows;
        Texture2D Buttons;
        Texture2D Numbers;

        protected override void LoadContent()
        {
            Arrows = Content.Load<Texture2D>("arrows");
            Buttons = Content.Load<Texture2D>("buttons");
            Numbers = Content.Load<Texture2D>("numbers");

            spriteBatch = new SpriteBatch(GraphicsDevice);
        }





        protected override void UnloadContent()
        {

        }

        readonly List<InputState> inputStates = new List<InputState>() { new InputState(Directions.NeutralMiddle, ButtonState.Nothing) };

        protected override void Update(GameTime gameTime)
        {

            InputState CurrentState = KeysInput.GetCurrentButtonState();
            if (inputStates[0] == CurrentState)
            {
                if (inputStates[0].frameCount < 9999)
                    inputStates[0].frameCount++;
            }
            else
            {
                inputStates.Insert(0, CurrentState);
            }
            if(inputStates.Count>30)
            {
                inputStates.RemoveRange(20, 11);
            }
            base.Update(gameTime);
        }

        private void NumberRender(int number, int XBase, int YBase)
        {
            XBase += (int)Math.Log10(number) * 24;
            do
            {
                int remainder = number % 10;
                number /= 10;

                //draw rem
                spriteBatch.Draw(Numbers, new Vector2(XBase, YBase + 10), new Rectangle(24 * remainder, 0, 24, 30), Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
                XBase -= 24;

            } while (number != 0);
        }

        private void DrawInputState(InputState state, int YBase)
        {
            //arrows
            spriteBatch.Draw(Arrows, new Vector2(0, YBase + 8), new Rectangle(0, 36 * (int)state.direction, 36, 36), Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
            //buttons
            spriteBatch.Draw(Buttons, new Vector2(40, YBase), new Rectangle(0, 50 * (int)state.buttonState, 126, 50), Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);

            //numbers
            NumberRender(state.frameCount, 175, YBase);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.DarkSeaGreen);

            spriteBatch.Begin();
            int YBase = 0;
            foreach(InputState inputState in inputStates)
            {
                DrawInputState(inputState, YBase);
                YBase += 50;
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
