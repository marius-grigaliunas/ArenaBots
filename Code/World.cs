using Godot;
using System;
using System.Collections.Generic;

public partial class World : Node
{
	public List<MeshInstance3D> baseChassisOptions = new List<MeshInstance3D>();
	[Export] Node3D bot = new Node3D();

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		MeshInstance3D boxMesh = new MeshInstance3D();
		MeshInstance3D capsuleMesh = new MeshInstance3D();

		BoxMesh box = new BoxMesh();
		boxMesh.Mesh = box;

        CapsuleMesh capsule = new CapsuleMesh();
        capsuleMesh.Mesh = capsule;

        baseChassisOptions.Add(boxMesh);
		baseChassisOptions.Add(capsuleMesh);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{

	}

    public void OnBaseOptionSelected(int index)
	{
        for (int i = 0; i < bot.GetChildren().Count; i++)
		{
			bot.GetChild(i).QueueFree();
		}

        bot.AddChild(baseChassisOptions[index]);
		
		GD.Print("Added " + baseChassisOptions[index].Name);
		GD.Print(index);
	}
}
