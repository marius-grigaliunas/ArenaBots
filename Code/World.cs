using Godot;
using System;
using System.Collections.Generic;

public partial class World : Node
{
	public List<Shape3D> baseChassisOptions = new List<Shape3D>();


	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		BoxShape3D box = new BoxShape3D();
		SphereShape3D sphere = new SphereShape3D();
		
		baseChassisOptions.Add(box);
		baseChassisOptions.Add(sphere);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{

	}
}
