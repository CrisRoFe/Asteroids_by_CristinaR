# Asteroids

## Game Controls:

- The Gameplay is based on the original Asteroids from 1979
- Use the up/down arrows to accelerate the ship
- Use the left/down arrows to rotate the ship
- Use the space bar to shoot projectiles
- Player starts the game with 5 lives
- Player receives different punctuation that add to their score depending on the size of the shot asteroid

## Codebase design and architecture:

- The SOLID principles drove all the architecture and the code base design decisions.

- The State Machine behavioural design pattern was used to build the project's main gameplay. Each state complies with 2 principles:
    - Single Responsibility Principle (SRP), each state has a defined purpose and is clearly distinct from the purpose of any other state. Class names are representative of their purpose.
    - Open-Closed Principle (OCP), adding a new state is a matter of creating a new class that inherits from the State class.
  
- Service Locator design pattern was favoured over the Singleton design pattern to control the lifecycle of the Game, UI and Sound effects controllers:
    - The Service Locator allows for a single point of entry to retrieve reusable components.
    - The Single Responsibility Principle is implemented: by using the Service Locator, functionality is decoupled from component access control, whereas each Singleton comprises both responsibilities.
    - Risk of human-error is reduced:
        - The ServiceLocator class is sealed, which prevents inheritance: methods cannot be manipulated from other classes.
        - The fail-fast principle prevents fail-silently behaviour: The Unity interface allows the manual addition of multiple scripts to a single scene, even if they are defined as Singletons in the codebase. The Service Locator throws an exception if multiple instances of the same script are encountered and attempted to be registered, whereas it is possible for the runtime to end up with multiple instances of the same Singleton.
    
- Object Pooling design pattern was used to boost performance, as it creates a pool of objects (in this case asteroids and projectiles), that can be reused on demand, rather than repeatedly Instantiated/Destroyed.

- Asteroids and Ship Settings are defined with Scriptable Objects, which can be modified on the Unity editor, so that parameters like the speed or the amount of lives that the player has at the beginning of the game can easily be changed. Scalability is thus increased, as minimum code modification is needed to add additional parameters or create new types of objects.

- To favour decoupling and ensure the Single Responsibility Principle is followed, the Observer design pattern was implemented via built-in Unity Actions. Providers are responsible for event triggering (i.e. an asteroid has been destroyed, or the player has lost a life) whereas observers are responsible for their own domain updates upon event reception.

- Assets are loaded programatically from the Resources folder, rather than via a direct asset reference in the Unity editor. Architecture flexibility is increased as it is easier to accommodate more performance optimised methods like server asset downloading, leveraging Unity Asset Bundles, currently unavailable due to lack of back-end services.

- Script execution order was altered to ensure codebase is intermittent-error free during runtime.

- A single starting point is implemented in the Bootstrapper class, so that control over the asteroids ́ wave kick-off is the responsibility of a single component as opposed to widespread.

- To aid performance, and reduce the number of Canvases in the scene the prefabs for the “Start UI” and “Game Over UI” are being loaded from the resources folder upon demand.
