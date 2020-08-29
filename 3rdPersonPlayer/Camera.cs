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
    public int MaxV = 0;
    public int MinV = -75;


    [Export]
    public float LookSensitivity = 0.1f;


    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        //Clip camera ignores the player so it doesnt collide with the players model and zoom in at his feet.
        _camera = (ClippedCamera)GetNode("h-pivot/v-pivot/ClippedCamera");
        _camV = (Spatial)GetNode("h-pivot/v-pivot");
        _camH = (Spatial)GetNode("h-pivot");
        _camera.AddException(GetParent());
    }

    public override void _Input(InputEvent @event)
    {
        Input.SetMouseMode(Input.MouseMode.Captured);
        if (@event is InputEventMouseMotion mouseMotion)
        {
            _camRotH += mouseMotion.Relative.x * LookSensitivity;
            _camRotV += mouseMotion.Relative.y * LookSensitivity;
        }
    }

    public override void _PhysicsProcess(float delta)
    {

    }


    public override void _Process(float delta)
    {
        //NOTE: THis would normally be in _PhysicsProcess but I theorize it might be more reponsive here with high DPI mice.
        //considering the camera clip will handle the physics related stuff anyway shouldnt cause any issues I dont think.
        //If there are problems move to _PhysicsProcess() instead.
        
        //clamp and set camera angles.
        _camRotV = (_camRotV > MaxV) ? MaxV : _camRotV;
        _camRotV = (_camRotV < MinV) ? MinV : _camRotV;
        _camH.RotationDegrees = new Vector3(_camH.RotationDegrees.x, _camRotH, _camH.RotationDegrees.z);
        _camV.RotationDegrees = new Vector3(_camRotV, _camV.RotationDegrees.y, _camV.RotationDegrees.z);
    }
}
