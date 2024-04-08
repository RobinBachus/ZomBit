# ZomBit

ZomBit is a small project I am working on to learn C#. 
It will be a simple platformer game engine that will be used to create a simple platformer game.
I am trying to do what Unity does, but without Unity, but of course a much simpler version of it.

## Engine explanation

The engine starts at the `Game` class. This class is the main class of the engine and is responsible for the game loop. 
The game loop is a simple loop that runs at a fixed time step. 
This loop is responsible for updating the game and drawing it.

The `Game` class has a `Frame` property that is a `Canvas` object from WPF. This is the canvas where the game will be drawn.

The `Game` class loads in the scenes and its views.

A `Scene` is basically a level. 
When the player gets to the end objective of the scene they win and the next scene will be loaded.

A `View` is a part of a scene. It holds all the game objects that are shown in one frame. 
When the player goes to an edge of the screen, the scene will switch to the next view.

A `GameObject` is an object that can be drawn on the screen.
It has a `Position` and a `Size` property and a `Drawable` that is a `System.Windows.Shapes.Shape` object.
It alse has a `Draw` method that will be called by the `Game` class every frame.

The `ICollidable` interface is used on objects that can collide with other objects.
It has methods to check if it collides with another object and events that will be called when it collides with another object.

## Roadmap

### Game "engine"

- [ ] Create a simple game loop
	- [x] Create a simple game loop with a fixed time step
	- [ ] Make an update event
	- [ ] Make a draw event
- [x] Make use of the wpf canvas to draw the game
- [ ] Create a game object system (in progress)
	- [x] Create a base class for game objects
	- [ ] Create some basic game objects
		- [x] Create a rectangle
		- [ ] Create a circle
		- [ ] Create a triangle
	- [ ] Create a way to add sprites
- [ ] Create a scene system
	- [x] Create a base class for scenes
	- [ ] Add a scene manager for loading scenes
	- [ ] Create a way to switch scenes
	- [ ] Create a way to add views to a scene
	- [ ] Create a way to add game objects to a view
	- [ ] Create a way to switch views
- [x] Create a collision system
	- [ ] Optimize collision checking
- [ ] Create a input system
- [ ] Create a physics system (I might keep it to collisions only)

### Game

- [ ] Create a simple game
	- [ ] Create a player
		- [ ] Add a way to shoot
		- [ ] Add a way to move
		- [ ] Add a way to die
		- [ ] Add a way to win
	- [ ] Add enemies
	- [ ] Add some levels
	- [ ] Menu
		- [ ] Add a main menu
		- [ ] Add a pause menu
		- [ ] Add a game over screen
		- [ ] Add a win screen
		- [ ] Add a way to restart the game
		- [ ] Add a level select screen

### Optional
- [ ] Add a way to add sounds
- [ ] Add weapons
- [ ] Add power ups
- [ ] Add a way to add animations