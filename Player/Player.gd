extends KinematicBody


# Declare member variables here. Examples:
# var a = 2
# var b = "text"

export var playerName = "Mr Bean"
export var health = 100
export var lookSensitivity = 0.1
export var walkingSpeed = 0.5

var hRot = 0
var gravity : int = ProjectSettings.get_setting("physics/3d/default_gravity")


# Called when the node enters the scene tree for the first time.
func _ready():
	Input.set_mouse_mode(Input.MOUSE_MODE_CAPTURED)
	$HUD/Name.text = playerName;
	
func _process(delta):
	pass
	
func _input(event):
	#Mouse look
	if event is InputEventMouseMotion:
		hRot += -event.relative.x

func _physics_process(delta):
	dash(delta) || walk(delta)
	
func walk(delta):
	#Note that negetive Z is forwards. -X is left
	var direction = Vector3()
	direction.z = (Input.get_action_strength("backward") - Input.get_action_strength("forward"))
	if direction.z > 0:
		#walking backwards is slower
		direction.z = direction.z / 2
		
	direction.x = (Input.get_action_strength("strafe right") - Input.get_action_strength("strafe left"))
	rotation_degrees.y = hRot * lookSensitivity
	
	#Apply gravity
	direction.y -= delta * gravity
	var collisions = move_and_collide(direction.rotated(Vector3(0, 1, 0), rotation.y))
	
	#Walking Animation
	$AnimationPlayer.playback_speed = max(abs(direction.z), abs(direction.x)) * 10
	if direction.z || direction.x:
		if	$AnimationPlayer.current_animation != "Walking":
			$AnimationPlayer.play("Walking")
	elif $AnimationPlayer.current_animation == "Walking":
		$AnimationPlayer.stop()
		
	#Turn torso so strafe left and right doesnt look odd with walking animation
	var torsoRotation = direction.x * (90 - (abs(direction.z) *45 ) )
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

#launch the player by some dash ammount in the direction they are currently moving.
func dash(delta):
	#TODO: if dashing dash and return TRUE so that wlak is not fired.
	pass
