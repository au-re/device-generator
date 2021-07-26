using Godot;
using System;

public class CameraGimbal : Spatial
{
    [Export] float rotationSpeed = Mathf.Pi / 2;
    [Export] float maxZoom = 3.0f;
    [Export] float minZoom = 0.5f;
    [Export] float zoomSpeed = 0.09f;
    [Export] float rotationEaseOutSpeed = 0.0125f;
    float zoom = 1.5f;
    float fadeOutRotationY = 1f;
    float fadeOutRotationX = 1f;
    string fadingDirection = "";

    private void GetInputKeyboard(float delta)
    {
        // rotate outer gimbal
        float yRotation = 0;

        if (Input.IsActionPressed("cam_left")) yRotation = -1;
        if (Input.IsActionPressed("cam_right")) yRotation = 1;

        // easing off the rotation
        // ugly but it works, I really don't have any alternative in mind
        if (Input.IsActionJustReleased("cam_left"))
        {
            fadeOutRotationY = -1f;
            fadingDirection = "left";
        }

        if (Input.IsActionJustReleased("cam_right"))
        {
            fadeOutRotationY = 1f;
            fadingDirection = "right";
        }

        if (fadingDirection == "left" && fadeOutRotationY < 0)
        {
            fadeOutRotationY += rotationEaseOutSpeed;
            yRotation = fadeOutRotationY;
        }

        if (fadingDirection == "right" && fadeOutRotationY > 0)
        {
            fadeOutRotationY -= rotationEaseOutSpeed;
            yRotation = fadeOutRotationY;
        }

        if (
            fadingDirection == "left" && fadeOutRotationY > 0
            || fadingDirection == "right" && fadeOutRotationY < 0)
        {
            fadingDirection = "";
        }
        // --

        RotateObjectLocal(Vector3.Up, yRotation * rotationSpeed * delta);

        // rotate inner gimbal
        float xRotation = 0;
        var innerGimbal = (Spatial)GetNode("InnerGimbal");

        if (Input.IsActionPressed("cam_up")) xRotation = -1;
        if (Input.IsActionPressed("cam_down")) xRotation = 1;

        // easing off the rotation
        // ugly but it works, I really don't have any alternative in mind
        if (Input.IsActionJustReleased("cam_up"))
        {
            fadeOutRotationX = -1f;
            fadingDirection = "up";
        }

        if (Input.IsActionJustReleased("cam_down"))
        {
            fadeOutRotationX = 1f;
            fadingDirection = "down";
        }

        if (fadingDirection == "up" && fadeOutRotationX < 0)
        {
            fadeOutRotationX += rotationEaseOutSpeed;
            xRotation = fadeOutRotationX;
        }

        if (fadingDirection == "down" && fadeOutRotationX > 0)
        {
            fadeOutRotationX -= rotationEaseOutSpeed;
            xRotation = fadeOutRotationX;
        }

        if (
            fadingDirection == "up" && fadeOutRotationX > 0
            || fadingDirection == "down" && fadeOutRotationX < 0)
        {
            fadingDirection = "";
        }
        // --

        innerGimbal.RotateObjectLocal(Vector3.Right, xRotation * rotationSpeed * delta);
    }

    public override void _Input(InputEvent inputEvent)
    {
        if (inputEvent.IsActionPressed("cam_zoom_in"))
        {
            Scale = PUtils.Lerp(Scale, Vector3.One * minZoom, zoomSpeed);
        }

        if (inputEvent.IsActionPressed("cam_zoom_out"))
        {
            Scale = PUtils.Lerp(Scale, Vector3.One * maxZoom, zoomSpeed);
        }
    }

    public override void _Process(float delta)
    {
        GetInputKeyboard(delta);

        // clamp x rotation
        var innerGimbal = (Spatial)GetNode("InnerGimbal");
        innerGimbal.Rotation = new Vector3(
            Mathf.Clamp(innerGimbal.Rotation.x, -1.4f, 0),
            innerGimbal.Rotation.y,
            innerGimbal.Rotation.z
        );
    }
}
