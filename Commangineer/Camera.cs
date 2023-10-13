using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Commangineer
{
    internal static class Camera
    {
        private static double x = 0;
        private static double y = 0;
        private static double xAcceleration = 0;
        private static double yAcceleration = 0;
        private static double xVelocity = 0;
        private static double yVelocity = 0;
        private static int scaleFactor = 1;

        /// <summary>
        /// Sets the camera accelleration
        /// </summary>
        /// <param name="x">the X vector acceleration</param>
        /// <param name="y">the Y vector acceleration</param>
        public static void SetAcceleration(double x, double y)
        {
            xAcceleration = x;
            yAcceleration = y;
        }

        /// <summary>
        /// Changes the camera scale according to a mouse scroll
        /// </summary>
        /// <param name="change">The change in the mouse scroll</param>
        public static void UpdateScale(int change)
        {
            double previousCameraScale = scaleFactor;
            scaleFactor = (int)Math.Floor(scaleFactor * Math.Pow(1.001, change));

            //If screen is too small, shrink game
            if (Commangineer.GetScreenWidth() > scaleFactor * Commangineer.GetLevel().GetTileWidth())
            {
                scaleFactor = Commangineer.GetScreenWidth() / Commangineer.GetLevel().GetTileWidth();
            }
            if (Commangineer.GetScreenHeight() > scaleFactor * Commangineer.GetLevel().GetTileHeight())
            {
                scaleFactor = Commangineer.GetScreenHeight() / Commangineer.GetLevel().GetTileHeight();
            }
            int MINIMUM_GAME_TILE_SCREEN_SIZE = 5;
            //if game is too small, make it bigger
            if (scaleFactor * MINIMUM_GAME_TILE_SCREEN_SIZE > Commangineer.GetScreenWidth())
            {
                scaleFactor = Commangineer.GetScreenWidth() / MINIMUM_GAME_TILE_SCREEN_SIZE;
            }
            if (scaleFactor * MINIMUM_GAME_TILE_SCREEN_SIZE > Commangineer.GetScreenHeight())
            {
                scaleFactor = Commangineer.GetScreenHeight() / MINIMUM_GAME_TILE_SCREEN_SIZE;
            }
            double midPointX = x + (Commangineer.GetScreenWidth()) / 2;
            midPointX *= (scaleFactor / previousCameraScale);
            x = midPointX - Commangineer.GetScreenWidth() / 2;
            double midPointY = y + (Commangineer.GetScreenHeight()) / 2;
            midPointY *= (scaleFactor / previousCameraScale);
            y = midPointY - Commangineer.GetScreenHeight() / 2;
        }

        /// <summary>
        /// Updates the camera acceleration values according to the KeyboardState
        /// </summary>
        /// <param name="keyboardState"></param>
        private static void UpdateAcceleration(KeyboardState keyboardState)
        {
            double xAcceleration = 0;
            double yAcceleration = 0;
            if (keyboardState.IsKeyDown(Keys.W))
            {
                yAcceleration -= 1;
            }
            if (keyboardState.IsKeyDown(Keys.S))
            {
                yAcceleration += 1;
            }
            if (keyboardState.IsKeyDown(Keys.A))
            {
                xAcceleration -= 1;
            }
            if (keyboardState.IsKeyDown(Keys.D))
            {
                xAcceleration += 1;
            }
            SetAcceleration(xAcceleration, yAcceleration);
        }

        /// <summary>
        /// Updates the camera movement
        /// </summary>
        /// <param name="keyboardState">The current state of the keyboard</param>
        /// <param name="ms">The miliseconds passed since the last update</param>
        public static void UpdateMovement(KeyboardState keyboardState, int ms)
        {
            double timeIncrement = (double)ms / 1000;
            UpdateAcceleration(keyboardState);
            int speedFactor = 3;
            x += speedFactor * (xVelocity * timeIncrement + xAcceleration * 1 / 2 * timeIncrement * timeIncrement);
            y += speedFactor * (yVelocity * timeIncrement + yAcceleration * 1 / 2 * timeIncrement * timeIncrement);
            xVelocity += xAcceleration;
            yVelocity += yAcceleration;
            double CAMERA_SPEED_DECAY = 0.001d;
            xVelocity *= Math.Pow(1 - CAMERA_SPEED_DECAY, ms);
            yVelocity *= Math.Pow(1 - CAMERA_SPEED_DECAY, ms);
            double CAMERA_SPEED_DECREASE = 1d;
            //decrease camera speed if keys not active
            if (xAcceleration == 0)
            {
                if (xVelocity > 1)
                {
                    xVelocity -= CAMERA_SPEED_DECREASE;
                }
                if (xVelocity < -1)
                {
                    xVelocity += CAMERA_SPEED_DECREASE;
                }
            }
            if (yAcceleration == 0)
            {
                if (yVelocity > 1)
                {
                    yVelocity -= CAMERA_SPEED_DECREASE;
                }
                if (yVelocity < -1)
                {
                    yVelocity += CAMERA_SPEED_DECREASE;
                }
            }
            if (xVelocity < 1 && xVelocity > -1 && xAcceleration == 0) { xVelocity = 0; }
            if (yVelocity < 1 && yVelocity > -1 && yAcceleration == 0) { yVelocity = 0; }
            if (x + Commangineer.GetScreenWidth() > (scaleFactor * Commangineer.GetLevel().GetTileWidth()))
            {
                x = scaleFactor * Commangineer.GetLevel().GetTileWidth() - Commangineer.GetScreenWidth();
            }
            if (y + Commangineer.GetScreenHeight() > (scaleFactor * Commangineer.GetLevel().GetTileHeight()))
            {
                y = scaleFactor * Commangineer.GetLevel().GetTileHeight() - Commangineer.GetScreenHeight();
            }
            if (x < 0)
            {
                x = 0;
                xVelocity = 0;
            }
            if (y < 0)
            {
                y = 0;
                yVelocity = 0;
            }
            //Debug.WriteLine("xv: " + xVelocity);
            //Debug.WriteLine("yv: " + yVelocity);
        }

        public static void Draw(SpriteBatch spriteBatch, TexturedObject toDraw, Rectangle drawPosition)
        {
            Rectangle destinationRectangle = new Rectangle(
                (int)Math.Floor(drawPosition.X * scaleFactor - x),
                (int)Math.Floor(drawPosition.Y * scaleFactor - y),
                scaleFactor,
                scaleFactor);
            Rectangle screenView = new Rectangle(0, 0, Commangineer.GetScreenWidth(), Commangineer.GetScreenHeight());
            if (screenView.Intersects(destinationRectangle))
            {
                spriteBatch.Draw(toDraw.GetTexture(), destinationRectangle, Color.White);
            }
        }

        public static void Draw(SpriteBatch spriteBatch, TexturedObject toDraw)
        {
            Rectangle destinationRectangle = new Rectangle(
                (int)Math.Floor(toDraw.GetPosition().X * scaleFactor - x),
                (int)Math.Floor(toDraw.GetPosition().Y * scaleFactor - y),
               scaleFactor * toDraw.GetSize().X,
                scaleFactor * toDraw.GetSize().Y);
            Rectangle screenView = new Rectangle(0, 0, Commangineer.GetScreenWidth(), Commangineer.GetScreenHeight());
            if (screenView.Intersects(destinationRectangle))
            {
                spriteBatch.Draw(toDraw.GetTexture(), destinationRectangle, Color.White);
            }
        }
    }
}