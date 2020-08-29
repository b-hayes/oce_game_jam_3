using Godot;

namespace oce_game_jam_3._3rdPersonPlayer
{
    public class Player : KinematicBody
    {
        // Declare member variables here. Examples:
        // private int a = 2;
        // private string b = "text";

        [Export] private string name = "Mr Bean";
        [Export] private float health = 100.0f;
        [Export] private float moveSpeed = 5;

        // Called when the node enters the scene tree for the first time.
        public override void _Ready()
        {
            ((Label) GetNode("HUD/Name")).Text = name;
        }

        // Called every frame. 'delta' is the elapsed time since the previous frame.
        public override void _Process(float delta)
        {
            var velocity = new Vector3(
                (Input.GetActionStrength("strafe right") - Input.GetActionStrength("strafe left")) * moveSpeed,
                0,
                (Input.GetActionStrength("backward") -Input.GetActionStrength("forward")) * moveSpeed
            );
            MoveAndSlide(velocity);
            
            //rotate to match camera if specified
            if (cameraSwivel != null)
            {
                GD.Print(cameraSwivel.RotationDegrees.y);
                RotationDegrees = new Vector3(0,cameraSwivel.RotationDegrees.y,0);
            }
        }
        
        
    }
}