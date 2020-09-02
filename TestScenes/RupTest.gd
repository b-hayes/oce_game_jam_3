extends Spatial


# Declare member variables here. Examples:
# var a = 2
# var b = "text"


# Called when the node enters the scene tree for the first time.
func _ready():
	pass # Replace with function body.


# Called every frame. 'delta' is the elapsed time since the previous frame.
#func _process(delta):
#	pass

func _input(event):
	if event.is_action_pressed("ui_cancel"):
		#get_tree().quit()
		get_tree().reload_current_scene()

	if event.is_action_pressed("Toggle Fullscreen"):
		OS.window_fullscreen = !OS.window_fullscreen
	
