using Godot;
using System;

public class Menu : VBoxContainer
{
    [Export]
    public int defaultSelectedIndex = 0;
    private MenuItem selected;

    public override void _Ready()
    {
        var children = GetChildren();
        var defaultMenuItem = children[defaultSelectedIndex] as MenuItem;
        defaultMenuItem.SetActive(true, false);
        selected = defaultMenuItem;

        foreach (MenuItem child in GetChildren())
        {
            child.controlled = true;
            child.Connect("OnEnter", this, nameof(SetSelected));
        }
    }

    public void SetSelected(MenuItem item)
    {
        if (item != selected)
        {
            item.SetActive(true);
            selected.SetActive(false);
            selected = item;
        }
    }

}
