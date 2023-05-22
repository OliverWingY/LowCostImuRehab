# LowCostImuRehab
This repo contains a rehabilitation game and a program to allow communication between a bluetooth client and the game.
The important files are here:

-Blender files: files necessary to create the arm

-Unity project/Rehabilitation arms: The files for the unity game, if opening the project from unity this is the folder to open

-Unity project/Rehabilitation arms/Assets/Scripts/MIddleware: These are the scripts necessary for commuicating with the middleware and calibrating the arms

-Unity project/Rehabilitation arms/Assets/Scenes/Game.unity: The game itself

-Unity project/Middleware: the .net application for communicating with the bluetooth client, the important class is imuDataConnector.cs. the build events are configured so the assembly is automatically put where the game needs it.

Everything else is either self explanitory or tests, tools, or now obselete files and folders, yes this repo is not particularly clean.
