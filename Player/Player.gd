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
var gravity : int = ProjectSettings.get_setting("physics/2d/default_gravity")


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
	#Apply gravity
	velocity.y -= delta * gravity
	move_and_slide(velocity.rotated(Vector3(0, 1, 0), rotation.y))
	
	#Walking Animation
	if velocity.z || velocity.x:
		if	$AnimationPlayer.current_animation != "Walking":
			$AnimationPlayer.play("Walking")
	elif $AnimationPlayer.current_animation == "Walking":
		$AnimationPlayer.stop()
	#Turn torso so strafe left and right doesnt look odd with walking animation
	var torsoRotation = (Input.get_action_strength("strafe right") - Input.get_action_strength("strafe left")) * 45
	$Body/Torso.rotation_degrees.z = torsoRotation
		
func attack():
	print("Attacking...")
	#TODO:
	# - Punch animation with no weapon
	# - Swing animation while holding a melee weapon
	# - Throw animaiton for throable weapons.
	
func setWalkingSpeed(speed):
	$AnimationPlayer.playback_speed = speed
	walkingSpeed = speed

