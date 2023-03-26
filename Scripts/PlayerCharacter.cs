using Godot;
using System;

public partial class PlayerCharacter : RigidBody2D
{
	[Export]
	// Target velocity
	int VelocityCap = 350;
	[Export]
	int Acceleration = 125;
	[Export]
	int JumpForce = 700;
	Vector2 TargetSpeed;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _IntegrateForces(PhysicsDirectBodyState2D s)
	{
		// accelerates toward the target velocity
		if (s.LinearVelocity.X < TargetSpeed.X && TargetSpeed.X > 0)
		{

			s.LinearVelocity = new Vector2(s.LinearVelocity.X + Acceleration, s.LinearVelocity.Y);
		}
		if (s.LinearVelocity.X > TargetSpeed.X && TargetSpeed.X < 0)
		{
			s.LinearVelocity = new Vector2(s.LinearVelocity.X - Acceleration, s.LinearVelocity.Y);
		}
		if(Input.IsActionJustPressed("Jump"))
		{
			if (s.LinearVelocity.Y == 0 )
			{
				s.LinearVelocity = new Vector2(s.LinearVelocity.X,s.LinearVelocity.Y - JumpForce);
			}
			GD.Print("Jumped");
		}
	}
	public override void _Process(double delta)
	{
		// sets the target velocity for a direction. the script will try to accelerate towards that target velocity
		TargetSpeed = new Vector2(0,0);

		if(Input.IsActionPressed("MoveLeft"))
		{
			TargetSpeed = new Vector2(-VelocityCap, TargetSpeed.Y);
			GD.Print("Moved Left");
		}
		if(Input.IsActionPressed("MoveRight"))
		{
			TargetSpeed = new Vector2(VelocityCap, TargetSpeed.Y);
			GD.Print("Moved Right");
		}
		


		// Debugging information
		if (OS.IsDebugBuild())
		{
			GetNode<Label>("/root/Node2D/PlayerCharacter/Camera2D/CanvasLayer/DebugUtils/Position/Value").Text = Position.ToString();
			GetNode<Label>("/root/Node2D/PlayerCharacter/Camera2D/CanvasLayer/DebugUtils/TargetVelocity/Value").Text = TargetSpeed.ToString();
			GetNode<Label>("/root/Node2D/PlayerCharacter/Camera2D/CanvasLayer/DebugUtils/Velocity/Value").Text = new Vector2((float)Math.Round(LinearVelocity.X), (float)Math.Round(LinearVelocity.Y)).ToString();
			GetNode<Label>("/root/Node2D/PlayerCharacter/Camera2D/CanvasLayer/DebugUtils/FPS/Value").Text = Engine.GetFramesPerSecond().ToString();
			GetNode<Label>("/root/Node2D/PlayerCharacter/Camera2D/CanvasLayer/DebugUtils/FrameDelay/Value").Text = Math.Round(delta*1000,2).ToString() + "ms";
		}
		// Debugging Information

		double framemult = delta / (1.0/60);
		


	}
	public override void _Input(InputEvent @event)
	{
	}
}
