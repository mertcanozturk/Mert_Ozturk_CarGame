# Mert_Ozturk_CarGame

Gameplay
* Each level has a different obstacle setup.
* In each level, the player drives 8 cars one by one.
* For each car, entrance and target points change.
* Before starting to drive time freezes, and time unfreezes when the player touches the screen.
* After the player arrives at the target, a new car spawns at a different entrance for a different target.
* Before driving a new car time resets to the beginning in the following way. All previously driven cars reset to their own entrance, and when the player starts driving the new car, all previously driven cars start as well and previously driven cars follow the path that they took in their turns. 

Example;

![Gameplay](https://github.com/mertcanozturk/Mert_Ozturk_CarGame/blob/master/images/car_game.gif)

Controls

![controls](https://github.com/mertcanozturk/Mert_Ozturk_CarGame/blob/master/images/controls.png)

* Control scheme should be like this
* There is a turn right and turn the left button
* There is no brake button. The car always presses the gas pedal automatically.

Creating levels

![Creator Scene](https://github.com/mertcanozturk/Mert_Ozturk_CarGame/blob/master/images/creator_scene.png)

* New levels can be created with the scene named "Creator" without writing code. After open the scene just click LevelCreator object and use Create Level button.
* You can change the positions of the entrance and target. 
* You can drag the obstacle objects on scene from the Resources folder. Also you can change its position and rotation



