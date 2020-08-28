using Godot;
using System;

public class Camera : Spatial
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    private ClippedCamera _camera = null;
    private float _camRotH = 0.0f;
    private float _camRotV = 0.0f;
    private Spatial _camV;
    private Spatial _camH;

    [Export]
    public float LookSensitivity = 0.1f;
    
    
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        //Clip camera ignores the player so it doesnt collide with the players model and zoom in at his feet.
        _camera = (ClippedCamera) GetNode("h-pivot/v-pivot/ClippedCamera");
        _camV = (Spatial)GetNode("h-pivot/v-pivot");
        _camH = (Spatial)GetNode("h-pivot");
        _camera.AddException(GetParent());
    }

    public override void _Input(InputEvent @event)
    {
        if (@event is InputEventMouseMotion mouseMotion)
        {
            _camRotH += mouseMotion.Relative.x * LookSensitivity;
            _camRotV += mouseMotion.Relative.y * LookSensitivity;
        }
    }

    public override void _PhysicsProcess(float delta)
    {
        _camH.RotationDegrees = new Vector3(_camH.RotationDegrees.x, _camRotH, _camH.RotationDegrees.z);
        _camRotV = (_camRotV > 75) ? 75 : _camRotV;
        _camRotV = (_camRotV < 55) ? 55 : _camRotV;
        _camV.RotationDegrees =  new Vector3(_camRotV, _camV.RotationDegrees.y, _camV.RotationDegrees.z);
    }

    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
