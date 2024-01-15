using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using Point = Microsoft.Xna.Framework.Point;
using RectangleF = System.Drawing.RectangleF;
using Vector2 = Microsoft.Xna.Framework.Vector2;

namespace Commangineer
{
    /// <summary>
    /// Represents the game's camera in levels
    /// </summary>
    public static class Camera
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
            if (Commangineer.GetScreenWidth() > scaleFactor * Commangineer.Level.GetTileWidth())
            {
                scaleFactor = Commangineer.GetScreenWidth() / Commangineer.Level.GetTileWidth();
            }
            if (Commangineer.GetScreenHeight() > scaleFactor * Commangineer.Level.GetTileHeight())
            {
                scaleFactor = Commangineer.GetScreenHeight() / Commangineer.Level.GetTileHeight();
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
            //move camera, according to kinematics
            x += speedFactor * (xVelocity * timeIncrement + xAcceleration * timeIncrement * timeIncrement);
            y += speedFactor * (yVelocity * timeIncrement + yAcceleration * timeIncrement * timeIncrement);
            xVelocity += xAcceleration;
            yVelocity += yAcceleration;
            //exponentially decay camera speed - Gives a dynamic "slow down"
            double CAMERA_SPEED_DECAY = 0.001d;
            xVelocity *= Math.Pow(1 - CAMERA_SPEED_DECAY, ms);
            yVelocity *= Math.Pow(1 - CAMERA_SPEED_DECAY, ms);
            double CAMERA_SPEED_DECREASE = 1d;
            //decrease camera speed if not accelerating
            if (xAcceleration == 0)
            {
                if (xVelocity > CAMERA_SPEED_DECREASE)
                {
                    xVelocity -= CAMERA_SPEED_DECREASE;
                }
                if (xVelocity < -CAMERA_SPEED_DECREASE)
                {
                    xVelocity += CAMERA_SPEED_DECREASE;
                }
            }
            if (yAcceleration == 0)
            {
                if (yVelocity > CAMERA_SPEED_DECREASE)
                {
                    yVelocity -= CAMERA_SPEED_DECREASE;
                }
                if (yVelocity < -CAMERA_SPEED_DECREASE)
                {
                    yVelocity += CAMERA_SPEED_DECREASE;
                }
            }
            //If camera speed is close enough to 0, set it to 0
            if (xVelocity <= CAMERA_SPEED_DECREASE && xVelocity >= -CAMERA_SPEED_DECREASE && xAcceleration == 0)
            {
                xVelocity = 0;
            }
            if (yVelocity <= CAMERA_SPEED_DECREASE && yVelocity >= -CAMERA_SPEED_DECREASE && yAcceleration == 0)
            {
                yVelocity = 0;
            }
            //If
            if (x + Commangineer.GetScreenWidth() > (scaleFactor * Commangineer.Level.GetTileWidth()))
            {
                x = scaleFactor * Commangineer.Level.GetTileWidth() - Commangineer.GetScreenWidth();
            }
            if (y + Commangineer.GetScreenHeight() > (scaleFactor * Commangineer.Level.GetTileHeight()))
            {
                y = scaleFactor * Commangineer.Level.GetTileHeight() - Commangineer.GetScreenHeight();
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
        }

        /// <summary>
        /// Draws a object the camera can see
        /// </summary>
        /// <param name="spriteBatch">The sprite batch to be drawn with</param>
        /// <param name="toDraw">The rotatable object to be drawn</param>
        public static void Draw(SpriteBatch spriteBatch, RotatableTexturedObject toDraw)
        {
            Rectangle destinationRectangle = new Rectangle(
                (int)Math.Floor(toDraw.Position.X * scaleFactor - x),
                (int)Math.Floor(toDraw.Position.Y * scaleFactor - y),
               (int)(scaleFactor * toDraw.Size.X),
                (int)(scaleFactor * toDraw.Size.Y));

            Rectangle visualRectangle = new Rectangle(
                (int)Math.Floor((toDraw.Position.X + toDraw.Size.X / 2f) * scaleFactor - x),
                (int)Math.Floor((toDraw.Position.Y + toDraw.Size.Y / 2f) * scaleFactor - y),
               (int)(scaleFactor * toDraw.Size.X),
                (int)(scaleFactor * toDraw.Size.Y));

            Texture2D texture = toDraw.GetTexture();
            Rectangle screenView = new Rectangle(0, 0, Commangineer.GetScreenWidth(), Commangineer.GetScreenHeight());
            if (screenView.Intersects(destinationRectangle))
            {
                spriteBatch.Draw(texture, visualRectangle, null, Color.White, toDraw.Angle, new Vector2(texture.Width / 2f, texture.Height / 2f), SpriteEffects.None, 0f);
            }
        }

        /// <summary>
        /// Draws a bullet the camera can see
        /// </summary>
        /// <param name="spriteBatch">The sprite batch to be drawn with</param>
        /// <param name="bulletFrame">The bullet frame to draw</param>
        public static void Draw(SpriteBatch spriteBatch, BulletFrame bulletFrame)
        {
            Vector2 deltaPosition = bulletFrame.destination - bulletFrame.origin;
            Vector2 midpoint = (bulletFrame.destination + bulletFrame.origin) / 2;
            float bulletAngle = (float)Math.Atan2(deltaPosition.Y, deltaPosition.X);
            Texture2D texture = Assets.GetImage("bulletFrame");
            Rectangle visualRectangle = new Rectangle(
                (int)(midpoint.X * scaleFactor - x),
                (int)(midpoint.Y * scaleFactor - y),
                (int)Math.Abs(deltaPosition.Length() * scaleFactor),
                (int)(bulletFrame.size * scaleFactor)
                );
            spriteBatch.Draw(texture, visualRectangle, null, Color.White, bulletAngle, new Vector2(texture.Width / 2f, texture.Height / 2f), SpriteEffects.None, 0f);
        }

        /// <summary>
        /// Draws a object the camera can see
        /// </summary>
        /// <param name="spriteBatch">The sprite batch to be drawn with</param>
        /// <param name="toDraw">The object to be drawn</param>
        public static void Draw(SpriteBatch spriteBatch, TexturedObject toDraw)
        {
            Point projectedPoint = Project(toDraw.Position);
            Rectangle destinationRectangle = new Rectangle(
                projectedPoint.X,
                projectedPoint.Y,
               (int)(scaleFactor * toDraw.Size.X),
                (int)(scaleFactor * toDraw.Size.Y));
            Rectangle screenView = new Rectangle(0, 0, Commangineer.GetScreenWidth(), Commangineer.GetScreenHeight());
            if (screenView.Intersects(destinationRectangle))
            {
                spriteBatch.Draw(toDraw.GetTexture(), destinationRectangle, Color.White);
            }
        }

        /// <summary>
        /// Draws a texture to the screen
        /// </summary>
        /// <param name="spriteBatch">The sprite batch to be drawn with</param>
        /// <param name="position">The position to start drawing at</param>
        /// <param name="size">The size of what to draw</param>
        /// <param name="toDraw">The texture to be drawn</param>
        public static void DrawProjected(SpriteBatch spriteBatch, Vector2 position, Vector2 size, Texture2D toDraw)
        {
            Point projectedTopLeft = Project(position);
            Point projectedBottomRight = Project(position + size);
            Rectangle destinationRectangle = new Rectangle(
                projectedTopLeft,
                projectedBottomRight - projectedTopLeft);
            Rectangle screenView = new Rectangle(0, 0, Commangineer.GetScreenWidth(), Commangineer.GetScreenHeight());
            if (screenView.Intersects(destinationRectangle))
            {
                spriteBatch.Draw(toDraw, destinationRectangle, Color.White);
            }
        }

        /// <summary>
        /// Returns a point influenced by the camera scale
        /// </summary>
        /// <param name="startPoint">The point to influence</param>
        /// <returns>A new influenced point</returns>
        public static Point Project(Point startPoint)
        {
            return new Point(
                (int)Math.Floor(startPoint.X * scaleFactor - x),
                (int)Math.Floor(startPoint.Y * scaleFactor - y)
                );
        }

        /// <summary>
        /// Returns a point influenced by the camera scale
        /// </summary>
        /// <param name="startPoint">The vector position to influence</param>
        /// <returns>A new influenced point</returns>
        public static Point Project(Vector2 startPoint)
        {
            return new Point(
                (int)Math.Floor(startPoint.X * scaleFactor - x),
                (int)Math.Floor(startPoint.Y * scaleFactor - y)
                );
        }

        /// <summary>
        /// Uninfluences a rectangle by the camera scale
        /// </summary>
        /// <param name="rectangle">The rectangle to uninfluence</param>
        /// <returns>A new uninfluenced rectangle</returns>
        public static RectangleF Deproject(RectangleF rectangle)
        {
            return new RectangleF(
                    (float)(rectangle.X + x) / scaleFactor,
                    (float)(rectangle.Y + y) / scaleFactor,
                rectangle.Width / scaleFactor,
                rectangle.Height / scaleFactor

                );
        }

        /// <summary>
        /// Uninfluences a vector point by the camera scale
        /// </summary>
        /// <param name="startPoint">The vector point to uninfluence</param>
        /// <returns>A new uninfluenced vector point</returns>
        public static Vector2 Deproject(Vector2 startPoint)
        {
            return new Vector2(
                    (float)(startPoint.X + x) / scaleFactor,
                    (float)(startPoint.Y + y) / scaleFactor
                );
        }
    }
}