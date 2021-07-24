using Godot;
using System;

public class CameraGimbal : Spatial
{

    float rotationSpeed = Mathf.Pi / 2;

    private void GetInputKeyboard(float delta)
    {
        // rotate outer gimbal
        var yRotation = 0;

        if (Input.IsActionPressed("cam_left")) yRotation -= 1;
        if (Input.IsActionPressed("cam_right")) yRotation += 1;

        RotateObjectLocal(Vector3.Up, yRotation * rotationSpeed * delta);

        // rotate inner gimbal
        var xRotation = 0;
        var innerGimbal = (Spatial)GetNode("InnerGimbal");

        if (Input.IsActionPressed("cam_up")) xRotation -= 1;
        if (Input.IsActionPressed("cam_down")) xRotation += 1;

        innerGimbal.RotateObjectLocal(Vector3.Right, xRotation * rotationSpeed * delta);
    }

    public override void _Process(float delta)
    {
        GetInputKeyboard(delta);
    }
}
