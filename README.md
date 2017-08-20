# Project Bistro
**Version 1.0**

Restaurant manager game in Unity3D. Set up, build and manage your restaurant, waiters and menu from the bottom-up

--- 

## Installation 

1. Download the .exe and _Data files here: [ProjectBistro Game Builds](https://github.com/chriskok/ProjectBistro/tree/master/ProjectBistro%20Game%20Builds)

2. Make sure they're in the same place on your computer and run the .exe file. 

---

## Screenshots

<img src="https://github.com/chriskok/ProjectBistro/blob/master/Screenshots/1.JPG" width = 500>

User selects tiles, shaping their restaurant.

<img src="https://github.com/chriskok/ProjectBistro/blob/master/Screenshots/2.JPG" width = 500>

User selects the layout of their restaurant, placing the waiters, tables and chairs. 

<img src="https://github.com/chriskok/ProjectBistro/blob/master/Screenshots/3.JPG" width = 500>

User chooses the details of their menu (price, size and quality)

<img src="https://github.com/chriskok/ProjectBistro/blob/master/Screenshots/4.JPG" width = 500>

Customers spawn in random seats and the waiter delivers the food (if possible for the waiter). 

---

## Future Updates

### Customer Mechanic
- During the main scene, spawn customers to the right of the screen.
- The user will be able to touch the character (or click if on PC) to seat them immediately to a random available seat.

### Customer Orders
- Ordering should follow a formula based on the price, quality and size of the food selected by the players. Potentially updating the frequency of customer spawning as well. 

### Long-Term Goal
- Add a log for all the orders that come in as well as to display other messages we may want the user to see.
- Provide decorations to the restaurant for the user to pick. 
- Add the ability for the player to buy more tiles.
- Optimized for users on mobile platforms. 

---

## Lessons Learnt

### Optimization
- The OnMouseX() functions work okay with touch screen as they imply touches to be mouse clicks but are very slow for performance because of the insane amounts of Raycasts being cast each frame. Hence, for future use we should implement the OnTouch() functions in greater detail (first with the customers and then updating the previous OnMouseX() functions). 

### Communication
- A lot of time could be saved if we discussed as a team what exactly our variable names and conventions would be before starting the project. 
- We could also communicate better about what kind of algorigthms we'd be using to do certain things in the game that we could individually work around easily. 

### Minor Details
- When using the Singleton Pattern, remember that other objects which require the objects using that don't get destroyed must use the gameObject.Find() function instead of just making it a public variable. 
- Remember that Start() gets called everytime you load a scene but later than when the Singleton Pattern object starts to instantiate objects. 

---

## Contributors

- Christopher Kok Kye Shyang (<ckok@purdue.edu>)
- Eric Chen 

---

## License & copyright

© Christopher Kok Kye Shyang & Eric Chen, Purdue University 

---
