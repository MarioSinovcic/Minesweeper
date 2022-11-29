# Minesweeper

A C# console application that lets users play [minesweeper](https://en.wikipedia.org/wiki/Minesweeper_(video_game)). 
The basic architecture for the system can be found [here](https://github.com/MarioSinovcic/Minesweeper/wiki).

<details>
<summary>Design Docs</summary>
![minesweeper - overall flow](https://user-images.githubusercontent.com/38930019/204669563-19caa44e-0838-48c8-8f58-7b8d99121426.PNG)
![minesweeper - diagram](https://user-images.githubusercontent.com/38930019/204669626-d2639b59-00a4-4659-a31c-32402ba678dc.PNG)
![minesweeper - client](https://user-images.githubusercontent.com/38930019/204669616-6385ba2e-9b73-4c0a-91df-c36102214f81.PNG)
![minesweeper - controller](https://user-images.githubusercontent.com/38930019/204669623-05d6f265-8c5d-4772-bd18-90dfe6771ce9.PNG)
![minesweeper - domain](https://user-images.githubusercontent.com/38930019/204669628-27498c34-0fd9-4a37-8c78-85554be8b7e2.PNG)


</details>

## Requirements

- Generate random minesweeper grids with variating size and difficulty
- Generate grids based on JSON files
- Allow users to select tiles to show
- Lose the game when a tile is selected
- Win the game when all empty tiles are shown
- Be able to flag tiles

------
### How To Run It

 - either open the solution in an appropriate IDE (e.g., Rider).
 - or alternatively in terminal navigate to the Program.cs file in the Client project and then run "dotnet run" in your terminal.


------
### How To Play

 - show tiles by typing the x and y value with a space in between (e.g., "3 5").
 - place flags by typing "f" then the x and y value with a space in between (e.g., "f 2 1").
 - win by showing all tiles that aren't mines.
