extends KinematicBody


# Declare member variables here. Examples:
# var a = 2
# var b = "text"

export var playerName = "Mr Bean"
export var health = 100;

var velocity = Vector3(0,0,0)


# Called when the node enters the scene tree for the first time.
func _ready():
	$HUD/Name.text = playerName;
	
func _process(delta):
	velocity.z = (Input.get_action_strength("backward") - Input.get_action_strength("forward")) * 5
	velocity.x = (Input.get_action_strength("strafe right") - Input.get_action_strength("strafe left")) * 5
	
	# Move the player	
	move_and_slide(velocity)
	


# Called every frame. 'delta' is the elapsed time since the previous frame.
#func _process(delta):
#	pass
