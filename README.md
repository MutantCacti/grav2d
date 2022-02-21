# grav2d 
**A program which allows you to create a solar system in 2D.** Press Space to begin the game.

---
This is a small unity program I made in my free time that effectively simulates the effects of gravity on an unlimited number of celestial bodies. I do this using $F=G\frac{m_1m_2}{r^2}$ and some handy-dandy equations for finding force and velocity when given one or the other (as well as time and mass). The program works in 3D and uses three dimensions for all vector quantities, but I display it in 2D because this makes it much more usable and understandable as an interface (that is to say that it is very difficult to control a camera and move planets based off of mouse inputs in 3D space)

---
### Notes
This program has some limitations. For one thing, planets cannot attract each other, nor can they attract the sun. This is because the mass of the sun is too small relative to most of the planets (roughly 2:1) a necessary inaccuracy because otherwise the planets would have to be millions of kilometres away from the sun and would not be visible on the screen. As a result I was forced to disable planetary gravity, meaning the only body with a gravitational pull is the sun. Also, planets are limited in size to avoid them being larger than the sun. This is for obvious reasons. Otherwise, the simulation is unlimited in scope. The number of planets is not limited in any way. Time can be advanced to 16x normal speed. Beware, however, of losses in simulation fidelity when doing this with large quantities of game objects.

---
### Instructions
```
CAMERA CONTROLS
- Use the W, A, S, D keys to move the camera.
- Hold SHIFT to speed up the camera significantly.
- RIGHT CLICK on a planet to focus on it
- Press RIGHT ARROW or LEFT ARROW to cycle focus between planets
- SCROLL UP and DOWN to zoom in and out

PLANET CONTROLS
- LEFT CLICK and HOLD anywhere in space to create a planet
- LEFT CLICK and DRAG on a planet to give it a new velocity
- HOVER over a planet and press DELETE to remove it
- When focused on a planet, press DELETE to remove it

TIME CONTROLS
- Press PLUS to speed up time
- Press MINUS to slow down time
- Press SPACE to unpause
```
