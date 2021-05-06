# Minesweeper

A C# console application that lets users play [minesweeper](https://en.wikipedia.org/wiki/Minesweeper_(video_game)). 
The basic architecture for the system can be found [here](https://github.com/MarioSinovcic/Minesweeper/wiki).

## Requirements

- Generate random minesweeper grids with variating size and difficulty
- Generate grids based on JSON files
- Allow users to select tiles to show
- Lose the game when a tile is selected
- Win the game when all empty tiles are shown
- Be able to flag tiles

------
### How To Play

 - show tiles by typing the x and y value with a space in between (e.g., "3 5").
 - place flags by typing "f" then the x and y value with a space in between (e.g., "f 2 1").
 - win by showing all tiles that aren't mines.
