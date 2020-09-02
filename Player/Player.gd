extends KinematicBody


# Declare member variables here. Examples:
# var a = 2
# var b = "text"

export var playerName = "Mr Bean"
export var health = 100
export var lookSensitivity = 0.1
export var walkingSpeed = 1

var velocity = Vector3(0,0,0)
var hRot = 0
var gravity : int = ProjectSettings.get_setting("physics/3d/default_gravity")


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
	var direction = Vector3()
	direction.z = (Input.get_action_strength("backward") - Input.get_action_strength("forward"))
	velocity.z = direction.z * walkingSpeed
	direction.x = (Input.get_action_strength("strafe right") - Input.get_action_strength("strafe left"))
	velocity.x = direction.x * walkingSpeed
	rotation_degrees.y = hRot * lookSensitivity
	#Apply gravity
	velocity.y -= delta * gravity
	#velocity = move_and_slide(velocity.rotated(Vector3(0, 1, 0), rotation.y))
	
	#TESTing move and cilide instead
	direction.y = - delta * gravity
	var collisions = move_and_collide(direction.rotated(Vector3(0, 1, 0), rotation.y))
	
	#Walking Animation
	$AnimationPlayer.playback_speed = max(abs(velocity.z), abs(velocity.x)) * 10
	if direction.z || direction.x:
		if	$AnimationPlayer.current_animation != "Walking":
			$AnimationPlayer.play("Walking")
	elif $AnimationPlayer.current_animation == "Walking":
		$AnimationPlayer.stop()
		
	#Turn torso so strafe left and right doesnt look odd with walking animation
	var torsoRotation = direction.x * 45
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

