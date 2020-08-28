using Godot;

namespace oce_game_jam_3._3rdPersonPlayer
{
    public class Player : KinematicBody
    {
        // Declare member variables here. Examples:
        // private int a = 2;
        // private string b = "text";

        public string name = "Mr Bean";

        [Export]
        public float lookSensitivity = 15.0f;

        // Called when the node enters the scene tree for the first time.
        public override void _Ready()
        {
            GD.Print("Player [" + this.name + "] is awake!");
        }

        // Called every frame. 'delta' is the elapsed time since the previous frame.
        public override void _Process(float delta)
        {
            if (Input.IsActionPressed("forward"))
            {
                Input.GetActionStrength("forward");
                GD.Print("Move forward 👆");
            }
        }

        public override void _Input(InputEvent @event)
        {
            base._Input(@event);
        }
    }
}