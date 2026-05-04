Credits -----------------------------------------------------------------------------------



The Team

===========================================================================================



Irving - Programming

* Movement System- 

  * Animator plays 3rd party animations in accordance with the action methods.
  * Listens for unity input system action events and calls methods accordingly.
  * State machine that handles turning on and off certain parts of script that shouldn't be on during those states ( Locomotion state, Jump state, swing state, fall state, ledge state)
  * The player is a rigidbody object and every movement action adds linear velocity to the Rigid body. 
  * Swinging mechanic, checks for objects under the swinging layer in the view of the camera and attaches a highlight to it. When the swing action is performed, switches to swing state and disables movement input. Releasing the swing button switches to fall state with lessened gravity to make it feel floaty.
  * Ledge grab mechanic, The player shoots out a ray cast within a certain range, if something that can be grabbed is in that range it casts another line cast to find the top face of the object, if the face is in grabbing range player goes into the ledge grab state making the rigidbody kinematic and teleporting to the point where the ledge is, Top faces y coordinate and x coordinate - distance from the front face.
* Combat System - 

  * Animator plays adjusted 3rd party animations on a separate layer as movement with a mask to be able to have the moving legs as well as the swinging arms.
  * State Machine to turn off certain actions in different states, (Attacking, Hurting, Healing, InCombat, OutCombat)
  * Take and deal damage, weapon system. Weapons hold Attack objects and uses them in order, Attack 1, attack 2.....)
  * Attack objects hold an animation override/damage integer/ and trigger collider object. when played the animation overrides the Attack animation in the animator and instantiates the collider object.
  * Health System, Each damageable object has a damageable type script ( PlayerHealth, EnemyHealth). The damageable holds Max health, current health and a reference to a health bar. It also holds methods for taking damage, healing damage, and dying. For the playerHealth each of those methods calls an action event as to be able to do other things with the method. For example the CombatAudio script listens for OnPlayerHurt event and plays the hurt sound. 
  * Health bar only appears when in combat, Attacking hurting or healing all put the player into the InCombat state.
* Pause / UI System

  * Using Unity's UI system. 
  * Sprite health bar that shows health as hearts each heart correlates to 2 health, with a half heart being 1. The bar updates every time the player is hurt or healed and can be as many hearts as needed. Increasing max health will give more hearts to the bar.
  * Listens for Pause input and disables player movement inputs from being read. 
  * Menus full of Buttons/sliders/input fields/
  * Persistent Settings
  * Settings menu references a class that gets variables from a static class and then sets those variables to the values of each of the interactables in the settings menu. When changing the interactables' values the settings class sets the values in the static class to that incoming value. This way the players' settings save through game sessions.
* Scenes/Levels

  * Death Mechanic

    * When the player dies I lock the movement inputs and show the Death menu UI.
    * Restart level button reloads the scene, main menu loads the main menu scene, quit game closes the application
  * Checkpoint mechanic

    * When starting the scene there is a player spawner object that grabs the prefab of everything the player needs spawned in and holds an initial spawn point position. It instantiates the player at the transform position of the spawn point object
    * Checkpoint objects are just a trigger collider that heal the player when walked into and then tells the player spawner that this checkpoint is the current checkpoint.
    * When the player falls off the level into the void there is a trigger at the bottom that tells the Player spawner to bring the player over to its current checkpoint.
  * Main menu scene

    * Used terrain tools for the environment and UI system for the buttons.
    * Gave 3rd party grass objects the Grass material so they sway in the wind and made the camera view bob over time
  * Test Scene

    * Just a playground i made to test all the mechanics while i was developing them.
* Sound System

  * Combat sounds and Movement sounds are on separate objects and use a class that holds every sound and has methods to play a one shot of each sound. 
  * Each sound event has a few different clips it can play and chooses randomly which clip to play
  * Background music looping for every level. The music stops when paused or dead.



























Unity Asset Store Packages

===========================================================================================































Sound Effects and Music

=============================================================================================



Most are public domain from OpenGameArt.Org, but some need credited for copyright.







Forest - Made by Tarush Singhal (Prod. Faccio)





Title:

&#x20;   I swear I saw it - background track



Author:

&#x20;   yd



URL:

&#x20;   https://opengameart.org/content/i-swear-i-saw-it-background-track



License(s):

&#x20;   \* CC0 ( http://creativecommons.org/publicdomain/zero/1.0/legalcode )



File(s):

&#x20;   \* IswearIsawit.zip

&#x20;   \* IswearIsawit.ogg



\----------------------------------------







