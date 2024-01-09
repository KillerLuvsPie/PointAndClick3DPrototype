# ABOUT

This prototype was part of the work done for an internship at NotAGameStudios for a Digital Games Development course that I took. This was a collaborative work with another intern from the same course I was taking.

This project is a 3D point and click game with some simple puzzle mechanics, some interactive elements and simple NPC AI, made in Unity Engine. I was in charge of all the technicalities during development, such as programming, creating documentation, incorporating the assets with the programming (playing animations when pressing controls, using variables to control animation properties, play sounds during certain events) and cleaning/fixing any problems or bugs found in the scene.

# GAMEPLAY FLOW
- The scene starts with the player character on the street
- The player must move around and explore the environment
- Interact with robots and camera posts to find out the code needed to unlock each other
- Unlock sequence: cameras -> shock drone -> flying drones -> store shutters
- The level ends when the player enters the door behind the store shutters
- The level resets

# ASSETS DEVELOPED BY ME
- Technical document (A document to explain the project, such as folder structure, planned mechanics, scene arrangement, etc.)
- GIT setup using a client (gitignore file, repo creation, invited other developers)
- NPC "barks"
- Sound editing using Audacity
- Icons in connection menus using screenshots and importing/editing them in GIMP
- Programming and scripts

# MECHANICS DEVELOPED BY ME
- Character navigation done with Unity navmesh system. Use of navmesh obstacles on geometry for obstacles
- Camera system using CineMachine. Use of trigger box colliders to switch between cameras
- Interaction queue when clicking NPCs and robots
  - Interaction markers to let the player know that object can be interacted with. Use of world to screen point to render world items in canvas
- Dialogue system with NPCs *
- Roaming NPCs using navmesh agent for movement
  - NPC AI. This is how it behaves: Spawn -> walk to the elevator -> rotate to look at elevator when in position -> enter elevator or wait in line if not the next in queue or elevator is busy -> begin elevator animation sequence when NPC enters it
  - Elevator animation sequence: close doors -> raise elevator -> open doors -> wait a bit -> close doors -> lower elevator -> open doors -> mark elevator as ready to receive another NPC
- All robots and camera posts have a list of connections and show the code needed to unlock the respective entity
  - Hovering over a connection will show a line towards the respective entity
- Robots and the store shutter's keypad open a side menu with an interactable keypad. Can insert numbers (up to 9 digits), delete inserted numbers and check if code is correct. Inserting the correct code opens the connections display for robots
- Certain animations (mostly animations that use transform properties)
  - Flying drone hovering animation
  - Interaction markers growing and shrinking depending if mouse is hovering interactable or not
  - Store shutters opening animation
  - Elevator animations (use of animation layers for doors and cabin position)
  - Player character and NPC animations implemented with blend tree. Use of characters normalized speed to blend between idle and walking animations
- Implemented sound effects and background noise. Some sound effects implemented with animation events
- Pause menu (right mouse button)

*Mechanics marked with * are unfinished to some extent due to limited time to refine or fully complete said systems
