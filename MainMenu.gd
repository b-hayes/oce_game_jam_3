extends Node


# Declare member variables here. Examples:
# var a = 2
# var b = "text"
var menu

# Called when the node enters the scene tree for the first time.
func _ready():
	menu = $"Menu"

# Called every frame. 'delta' is the elapsed time since the previous frame.
#func _process(delta):
#	pass

func _input(event):
	if event.is_action_pressed("ui_cancel"):
		#get_tree().quit()
		if menu.is_inside_tree():
			remove_child(menu)
		else:
			add_child(menu)
		

	if event.is_action_pressed("Toggle Fullscreen"):
		OS.window_fullscreen = !OS.window_fullscreen


func _on_PLAY_pressed():
	get_tree().change_scene("res://Levels/Level-01.tscn")


func _on_QUIT_pressed():
	get_tree().quit()
