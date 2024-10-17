using Godot;
using System;

public partial class PlayerMovement : CharacterBody3D
{
    [Export] float movementSpeed = 5f;
    Vector3 velocity;
    [Export] CsgBox3D mesh;

    public override void _PhysicsProcess(double delta)
    {


        velocity = Vector3.Zero;

        if (Input.IsActionPressed("ui_up"))
        {
            velocity.Z = -1;
        }
        if (Input.IsActionPressed("ui_down"))
        {
            velocity.Z = 1;
        }
        if (Input.IsActionPressed("ui_left"))
        {
            velocity.X = -1;
        }
        if (Input.IsActionPressed("ui_right"))
        {
            velocity.X = 1;
        }

        if (!IsOnFloor())
        {
            velocity.Y -= 9.8f * (float)delta;
        }

        velocity = velocity.Normalized();
        Velocity = velocity * movementSpeed;

        MoveAndSlide();

    }

}
