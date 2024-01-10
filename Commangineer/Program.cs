using Commangineer;
using Microsoft.Xna.Framework;
using System;
using System.Diagnostics;
using System.Windows.Input;

try
{
    using (var game = new Commangineer.Commangineer())
    {
        game.Run();
    };
}
catch (Exception exc)
{
    Log.logText("A unkown critical exception has occured: " + exc.ToString());
};