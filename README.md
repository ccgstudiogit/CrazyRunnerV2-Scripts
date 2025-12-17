# CrazyRunnerV2-Scripts
Scripts for my infinite 3D runner (2nd completed solo project). These are available for portfolio and educational purposes.

# Development Timeline
April 2024 - July 2024
Note: this was technically my first solo project, but my skills were not at the level to where I was able to complete it (scope was too big for my 
skill level) so I pivoted to Roll-A-Ball, and once I completed that came back to Crazy Runner and was able to finish it.

# About the Project
Crazy Runner is a 3D infinite runner with random obstacle/gem generation. The goal is to simply make it as far as you can without running into any
obstacle. Once the player runs into an obstacle, the run is over. The player can use AD to move left/right, space to jump, and left ctrl to roll or
use the arrow keys (left/right to move, up to jump, down to roll). Keybindings can also be re-configured in the settings menu.

# Development Reflection
Crazy Runner was my attempt at building a larger, systems-driven game after finishing Roll-A-Ball. I took the things I learned from completing
my first project and applied them to this project (scene management, programming fundamentals, collisions, movement) but also implemented a
lot more features that I was completely unfamiliar with: in-game currency that the player collects while playing the game, an in-game shop the
player can use to purchase skins and cheats using the in-game currency, a procedurally generated obstacle system, and finally a file-save system
that tracked player stats such as total games played, farthest distance, highest score, as well as keeping track of which items the player has
purchased and how many gems they have (the in-game currency). Working through these systems helped me understand how to manage game states, structure
data, and design gameplay loops that tie multiple moving parts together.

I still used free assets for most of the visuals, but this allowed me to focus almost entirely on gameplay programming, testing, and system design. I
did experiment with a small amount of 3D modeling, but the biggest part of this project was definitely the programming aspect.

![alt text](https://github.com/ccgstudiogit/CrazyRunnerV2-Scripts/blob/main/Screenshots/CrazyRunner_menu.jpg)

![alt text](https://github.com/ccgstudiogit/CrazyRunnerV2-Scripts/blob/main/Screenshots/CrazyRunner_stats.jpg)

![alt text](https://github.com/ccgstudiogit/CrazyRunnerV2-Scripts/blob/main/Gifs/store.gif)

![alt text](https://github.com/ccgstudiogit/CrazyRunnerV2-Scripts/blob/main/Gifs/slow.gif)

![alt text](https://github.com/ccgstudiogit/CrazyRunnerV2-Scripts/blob/main/Gifs/fast.gif)

![alt text](https://github.com/ccgstudiogit/CrazyRunnerV2-Scripts/blob/main/Gifs/player_loss.gif)
