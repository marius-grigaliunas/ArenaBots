using Godot;
using System;
using System.Collections.Generic;
using System.Reflection;

public partial class World : Node
{
	[Export] Node3D bot = new Node3D();

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		MeshInstance3D boxMesh = new MeshInstance3D();
		BoxMesh box = new BoxMesh();
		boxMesh.Mesh = box;


        bot.AddChild(boxMesh);
    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{

	}
}
