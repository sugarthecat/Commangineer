using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commangineer.GUI_Types
{
    internal class UnitEditorGUI : ScalingGUI
    {
        public UnitEditorGUI() : base(600, 400)
        {
            LoadElements("UnitEditorGUI");
        }
    }
}