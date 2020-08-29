extends KinematicBody


# Declare member variables here. Examples:
# var a = 2
# var b = "text"

export var playerName = "Mr Bean"
export var health = 100
export var lookSensitivity = 0.1

var velocity = Vector3(0,0,0)
var hRot = 0


# Called when the node enters the scene tree for the first time.
func _ready():
	Input.set_mouse_mode(Input.MOUSE_MODE_CAPTURED)
	$HUD/Name.text = playerName;
	
func _process(delta):
	pass
	
func _input(event):
	if event is InputEventMouseMotion:
		hRot += -event.relative.x

func _physics_process(delta):
	velocity.z = (Input.get_action_strength("backward") - Input.get_action_strength("forward")) * 5
	velocity.x = (Input.get_action_strength("strafe right") - Input.get_action_strength("strafe left")) * 5
	rotation_degrees.y = hRot * lookSensitivity
	print(rotation.y)
	move_and_slide(velocity.rotated(Vector3(0, 1, 0), rotation.y))

func attack():
	
