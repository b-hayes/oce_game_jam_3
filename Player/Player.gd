extends KinematicBody


# Declare member variables here. Examples:
# var a = 2
# var b = "text"

export var playerName = "Mr Bean"
export var health = 100
export var lookSensitivity = 0.1
export var walkingSpeed = 5

var velocity = Vector3(0,0,0)
var hRot = 0


# Called when the node enters the scene tree for the first time.
func _ready():
	Input.set_mouse_mode(Input.MOUSE_MODE_CAPTURED)
	$HUD/Name.text = playerName;
	$AnimationPlayer.playback_speed  = walkingSpeed
	
func _process(delta):
	pass
	
func _input(event):
	if event is InputEventMouseMotion:
		hRot += -event.relative.x

func _physics_process(delta):
	velocity.z = (Input.get_action_strength("backward") - Input.get_action_strength("forward")) * walkingSpeed
	velocity.x = (Input.get_action_strength("strafe right") - Input.get_action_strength("strafe left")) * walkingSpeed
	rotation_degrees.y = hRot * lookSensitivity
	move_and_slide(velocity.rotated(Vector3(0, 1, 0), rotation.y))
	
	#Walking Animation
	if velocity.z || velocity.x:
		if	$AnimationPlayer.current_animation != "Walking":
			$AnimationPlayer.play("Walking")
	elif $AnimationPlayer.current_animation == "Walking":
		$AnimationPlayer.stop()
		
func attack():
	print("Attacking...")
	
func setWalkingSpeed(speed):
	$AnimationPlayer.playback_speed = speed
	walkingSpeed = speed

