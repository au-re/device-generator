using Godot;
using System;

public class MenuItem : Control
{
    [Export]
    public string text = "";
    public bool active = false;
    public bool controlled = false;

    [Signal]
    public delegate void OnEnter(MenuItem item);

    public override void _Ready()
    {
        var label = (Label)GetNode("Label");
        label.Text = text;
    }

    public void SetActive(bool active, bool playSound = true)
    {
        var panel = (Panel)GetNode("Panel");
        var sound = (AudioStreamPlayer)GetNode("Sound");
        panel.Visible = active;
        if (active == true && playSound)
        {
            sound.Play();
        }
    }

    public void OnMenuItemEnter()
    {
        EmitSignal(nameof(OnEnter), this);
    }

    public void OnMenuItemExit() { }
}
