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
        [Export(PropertyHint.None, "If specified Spatial node path exists the character will copy the nodes X rotation. Typically used for making the character match the camera focus." )]
        private string cameraSwivelPath = "Camera/h-pivot";

        private Spatial cameraSwivel = null;
        

        // Called when the node enters the scene tree for the first time.
        public override void _Ready()
        {
            ((Label) GetNode("HUD/Name")).Text = name;
            GD.Print(cameraSwivelPath);
            if (!cameraSwivelPath.Empty() && GetNode(cameraSwivelPath) != null)
            {
                cameraSwivel = (Spatial)GetNode(cameraSwivelPath);
                GD.Print("Using camera rotation");
            }
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