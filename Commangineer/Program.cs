using Commangineer;
using System;

try
{
    using (var game = new Commangineer.Commangineer())
    {
        game.Run();
    };
}
catch (Exception exc)
{
    Log.LogText("A unkown critical exception has occured: " + exc.ToString());
};