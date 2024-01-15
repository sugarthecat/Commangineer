using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Commangineer
{
    /// <summary>
    /// Represents a font, includes all assets for the font
    /// </summary>
    public class Font
    {
        public Dictionary<string, Texture2D> characterTextures;
        private string supportedCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
        private Texture2D defaultCharacter;

        /// <summary>
        /// Creates a new font, loads in all characters
        /// </summary>
        /// <param name="addr">The location/name of the font</param>
        /// <param name="content">The content manager to load the font</param>
        public Font(string addr, ContentManager content)
        {
            defaultCharacter = content.Load<Texture2D>(addr + "/default");
            characterTextures = new Dictionary<string, Texture2D>();
            for (int i = 0; i < supportedCharacters.Length; i++)
            {
                try
                {
                    characterTextures.Add(supportedCharacters[i] + "", content.Load<Texture2D>(addr + "/" + supportedCharacters[i]));
                }
                catch
                {
                    characterTextures.Add(supportedCharacters[i] + "", defaultCharacter);
                }
            }
        }

        /// <summary>
        /// Draws text with a font
        /// </summary>
        /// <param name="spriteBatch">The SpriteBatch to use when drawing</param>
        /// <param name="textToDisplay">The text to display</param>
        /// <param name="textArea">The area of where to draw the text</param>
        public void DrawText(SpriteBatch spriteBatch, string textToDisplay, Rectangle textArea)
        {
            int width = textArea.Width;
            int height = textArea.Height;
            int xOffset = 0;
            int yOffset = 0;

            int theoreticNewWidth = (int)(64d * textToDisplay.Length * height / 100d);
            int theoreticNewHeight = (int)(100d * width / textToDisplay.Length / 64d);
            if (theoreticNewWidth < width)
            {
                width = theoreticNewWidth;
                xOffset = (textArea.Width - width) / 2;
            }
            else if (theoreticNewHeight < height)
            {
                height = theoreticNewHeight;
                yOffset = (textArea.Height - height) / 2;
            }
            Rectangle displayArea = new Rectangle(textArea.X + xOffset, textArea.Y + yOffset, width, height);

            for (int i = 0; i < textToDisplay.Length; i++)
            {
                if (characterTextures.ContainsKey(textToDisplay[i] + ""))
                {
                    Rectangle textRectangle = new Rectangle(displayArea.X + displayArea.Width / textToDisplay.Length * i, displayArea.Y, (int)((double)displayArea.Width / textToDisplay.Length), displayArea.Height);
                    spriteBatch.Draw(characterTextures[textToDisplay[i] + ""], textRectangle, Color.White);
                }
            }
        }
    }
}