using Godot;
using System;

public class FullScreenButton : Control
{
    public void OnButtonPress()
    {
        OS.WindowFullscreen = !OS.WindowFullscreen;
    }
}
