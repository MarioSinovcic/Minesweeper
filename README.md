# Minesweeper

A C# console applciation that lets users play [minesweeper](https://en.wikipedia.org/wiki/Minesweeper_(video_game)). 
The basic archtecture for the system can be found here <-link to diagram. 

## Requirements

- Generate random minesweeper grids with variating size and difficulty
- Generate grids based on JSON files
- Allow users to select tiles to show
- Lose the game when a tile is selected
- Win the game when all empty tiles are shown
- Be able to flag tiles

------
### How To Play

1. Open the app, then run the game.
2. Follow the rules:
 - show tiles by typing the x and y value with a space in between (e.g., "3 5")
 - place flags by typing "f" then the x and y value with a space in between (e.g., "f 2 1")
 - win by showing all tiles that aren't mines.
