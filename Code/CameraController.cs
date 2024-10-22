using Godot;
using System;

public partial class CameraController : Node3D
{
    [Export]
    public float RotationSpeed = 0.005f;

    [Export]
    public float ZoomSpeed = 0.05f;

    [Export]
    public float PanSpeed = 0.01f;

    [Export]
    public float MinZoom = 1.0f;

    [Export]
    public float MaxZoom = 20.0f;

    private Camera3D camera;
    private Vector2 lastMousePosition;
    private bool isDragging = false;
    private bool isPanning = false;
    private float rotationX = 0;
    private float rotationY = 0;

    public override void _Ready()
    {
        camera = GetNode<Camera3D>("Camera3D");
    }

    public override void _Input(InputEvent @event)
    {
        if (@event is InputEventMouseButton mouseButton)
        {
            if (mouseButton.ButtonIndex == MouseButton.Right)
            {
                isDragging = mouseButton.Pressed;
                if (isDragging)
                {
                    lastMousePosition = mouseButton.Position;
                }
            }
            else if (mouseButton.ButtonIndex == MouseButton.Middle)
            {
                isPanning = mouseButton.Pressed;
                if (isPanning)
                {
                    lastMousePosition = mouseButton.Position;
                }
            }
            else if (mouseButton.ButtonIndex == MouseButton.WheelUp)
            {
                Vector3 zoomDir = -camera.Transform.Basis.Z.Normalized();
                camera.Position += zoomDir * ZoomSpeed;
                camera.Position = camera.Position.LimitLength(MaxZoom);
            }
            else if (mouseButton.ButtonIndex == MouseButton.WheelDown)
            {
                Vector3 zoomDir = camera.Transform.Basis.Z.Normalized();
                camera.Position += zoomDir * ZoomSpeed;
                camera.Position = camera.Position.LimitLength(MaxZoom);

                if (camera.Position.Length() < MinZoom)
                {
                    camera.Position = camera.Position.Normalized() * MinZoom;
                }
            }
        }
        else if (@event is InputEventMouseMotion mouseMotion && (isDragging || isPanning))
        {
            Vector2 delta = mouseMotion.Position - lastMousePosition;
            lastMousePosition = mouseMotion.Position;

            if (isDragging)
            {
                // Update rotation angles
                rotationY -= delta.X * RotationSpeed;
                rotationX = Mathf.Clamp(rotationX - delta.Y * RotationSpeed, -Mathf.Pi / 2, Mathf.Pi / 2);

                // Reset rotation
                Transform3D transform = Transform;
                transform.Basis = Basis.Identity;
                Transform = transform;

                // Apply rotations in the correct order
                // First rotate around Y axis (horizontal)
                RotateY(rotationY);
                // Then rotate around X axis (vertical)
                RotateObjectLocal(Vector3.Right, rotationX);
            }
            else if (isPanning)
            {
                Vector3 right = camera.Transform.Basis.X;
                Vector3 up = camera.Transform.Basis.Y;
                Position -= right * (delta.X * PanSpeed) + up * (delta.Y * PanSpeed);
            }
        }
    }
}