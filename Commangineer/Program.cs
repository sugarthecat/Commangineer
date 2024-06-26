﻿using Commangineer;
using System;

/// <summary>
/// Initializes the game
/// </summary>

try
{
    using (var game = new Commangineer.Commangineer())
    {
        game.Run();
    };
}
// This should never be triggered, but is here just in case
catch (Exception exc)
{
    Log.LogText("A unkown critical exception has occured: " + exc.ToString());
};