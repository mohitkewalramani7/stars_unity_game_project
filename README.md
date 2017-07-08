# Title : stars_unity_game_project

# Description : 
A game developed using the Unity platform, written mainly in C# that allows the user to control a ball using the accelerometer of a mobile device. The ball rolls around the maze, trying to collect the stars it can before time runs out. Files of the Assets Directory are uploaded.

# Background and Construction : 

1) The rolling ball is represented as a Player object, which took the form or a rigidbody to ensure that is stays rolling on the maze terrain.

2) The collectibles are the stars that are deployed through the maze. Each star is made up of 4 cylinders put together.

3) The maze canvas are surrounded by walls to ensure that the ball cannot leave the terrain. A background setting is present upon which the entire maze is drawn

4) The collecibles have taken the abstract form of a prefab, with an associated common colour, but unique position through the maze

# Demo : 

View the screenshots of the game as attached.
