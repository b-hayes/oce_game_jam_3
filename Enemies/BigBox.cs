using Godot;

namespace oce_game_jam_3.Enemies
{
    public class BigBox : KinematicBody
    {
        // Declare member variables here. Examples:
        // private int a = 2;
        // private string b = "text";
        public int Speed = 200;
        private Vector3 _velocity = new Vector3();
        float reflection = 0.1f;
        float gravity = (float) ProjectSettings.GetSetting("physics/3d/default_gravity");
        private Navigation _nav;
        private AI.AIBase ai = new AI.AIBase(false);

        // Called when the node enters the scene tree for the first time.
        public override void _Ready()
        {
            GD.Print("Enemy is ready");
            _nav = GetParent<Navigation>();
        }

        public override void _PhysicsProcess(float delta)
        {
            
            _velocity.y -= gravity * delta;
            var _newvelocity = MoveAndSlide(_velocity);
            var collisions = GetSlideCount();
            if (collisions > 0)
            {
                _newvelocity.y = -_velocity.y  * reflection;
            }
            _velocity = _newvelocity;
        }

         // Called every frame. 'delta' is the elapsed time since the previous frame.
         public override void _Process(float delta)
         {
             
         }
    }
}