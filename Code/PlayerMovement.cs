using Godot;
using System;

public partial class PlayerMovement : CharacterBody3D
{
    [Export] float movementSpeed = 5f;
    Vector3 velocity;
    [Export] CsgBox3D mesh;
    private bool isCollidingThisFrame;
    private bool wasCollidingLastFrame;
    StandardMaterial3D mat1 = new StandardMaterial3D();


    public override void _PhysicsProcess(double delta)
    {
        isCollidingThisFrame = CheckCollisionWithWall();

        if (isCollidingThisFrame && !wasCollidingLastFrame)
        {
            //Start
            StartCollisionWithWall();
        }

        if (!isCollidingThisFrame && wasCollidingLastFrame)
        {
            EndCollisionWithWall();
        }

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

        wasCollidingLastFrame = isCollidingThisFrame;
    }

    public void StartCollisionWithWall()
    {
        mat1.AlbedoColor = Color.FromString("Red", Colors.Red);

        GD.Print("Collision started!");
        mesh.MaterialOverride = mat1;
    }

    public void EndCollisionWithWall()
    {
        GD.Print("Collision Ended!");
        mesh.MaterialOverride = null;
    }

    public bool CheckCollisionWithWall()
    {
        for (int i = 0; i < GetSlideCollisionCount(); i++)
        {
            var collision = GetSlideCollision(i);

            if (((Node)collision.GetCollider()).Name.ToString().StartsWith("Obstacle"))
            {

                return true;
            }
        }

        return false;
    }
}
