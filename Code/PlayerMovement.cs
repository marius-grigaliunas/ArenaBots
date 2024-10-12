using Godot;

public partial class PlayerMovement : CharacterBody3D
{
    [Export] private float speed = 5.0f; // Movement speed
    private Vector3 velocity = new Vector3(); // Store the player's velocity

    public override void _PhysicsProcess(double delta)
    {
        // Reset vertical velocity each frame
        if (IsOnFloor())
        {
            velocity.Y = 0; // Reset vertical velocity when on the ground
        }
        else
        {
            velocity.Y -= 9.81f * (float)delta; // Apply gravity
        }

        // Get the input direction
        Vector3 direction = new Vector3();
        if (Input.IsActionPressed("ui_left"))
            direction.X -= 1; // Move left
        if (Input.IsActionPressed("ui_right"))
            direction.X += 1; // Move right
        if (Input.IsActionPressed("ui_up"))
            direction.Z -= 1; // Move forward
        if (Input.IsActionPressed("ui_down"))
            direction.Z += 1; // Move backward

        // Normalize the direction vector to avoid faster diagonal movement
        direction = direction.Normalized();

        // Move the character
        velocity.X = direction.X * speed; // Set horizontal velocity
        velocity.Z = direction.Z * speed; // Set forward/backward velocity
        
        Velocity = velocity;
        MoveAndSlide(); // Move the character and apply gravity
    }
}
