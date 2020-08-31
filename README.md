# Solaria

Solaria is a 3D n-body gravity simulator written in the Unity3D game engine. It allows users to create and simulate their own solar systems. Variable simulation speed and precision is provided as settings for the user based on their setup/computer speed.

![Imgur Image](https://imgur.com/omAQ75X.gif)

# What is an n-body simulator?

In many cases, two-body approximations are taken when simulating gravitational forces between objects. This would mean that in a solar system, the planets are only affected by the gravitational pull of the Sun, but the pull of planets on each other is unaccounted for. An n-body simulation simulates the interaction between all bodies in a space, which allows for some complex systems (e.g. binary star systems)

![Imgur Image](https://imgur.com/7NwpW2N.gif)

# How is everything simulated?

I am simply using an iterative technique, calculating the force of each object on every other object at each time step. I would love to employ some more complicated methods I have since learned about (e.g. https://en.wikipedia.org/wiki/Octree) to improve performance in the future.

# Creating Chaos is Fun

![Imgur Image](https://imgur.com/lU3dMsa.gif)

# Download Solaria

Go to the releases of GitHub to download a copy of Solaria to try for yourself! Experiment with adding bodies to a solar system and making all sorts of wacky things! I do have ideas for an improved version of this project in the future.
