using Commangineer.GUI_Element_Types;
using Microsoft.Xna.Framework;
using System;
using System.Diagnostics;
using System.Reflection;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Collections.Generic;

namespace Commangineer.GUI_Types
{
    /// <summary>
    /// The GUI object for the title screen
    /// </summary>
    internal class TitleScreenGUI : ScalingGUI
    {
        public TitleScreenGUI() : base(600, 400)
        {
            LoadElements("TitleScreenGUI");
        }
    }
}