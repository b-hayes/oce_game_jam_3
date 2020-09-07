extends Node


# Declare member variables here. Examples:
# var a = 2
# var b = "text"
var menu

# Called when the node enters the scene tree for the first time.
func _ready():
	menu = $"Menu"
	if get_parent().name != "Main":
		toggleMenu()
		

# Called every frame. 'delta' is the elapsed time since the previous frame.
#func _process(delta):
#	pass

func _input(event):
	if event.is_action_pressed("ui_cancel"):
		toggleMenu()

	if event.is_action_pressed("Toggle Fullscreen"):
		OS.window_fullscreen = !OS.window_fullscreen


func _on_PLAY_pressed():
	get_tree().change_scene("res://Levels/Level-01.tscn")


func _on_QUIT_pressed():
	get_tree().quit()

func toggleMenu():
	if menu.is_inside_tree():
		remove_child(menu)
		Input.set_mouse_mode(Input.MOUSE_MODE_CAPTURED)
		get_tree().paused = false
	else:
		add_child(menu)
		Input.set_mouse_mode(Input.MOUSE_MODE_VISIBLE)
		get_tree().paused = true
