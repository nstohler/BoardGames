# BoardGames

# Mastermind

Rules here:
https://en.wikipedia.org/wiki/Mastermind_(board_game)

## TODO

- Add hint system
  - hints cost attempts
  - "no duplicates in code" / "at least one duplicate"
  - "green is not in the code"

- Add difficult levels
  - Easy: no duplicate colors allowed
  - Normal: duplicates allowed
  - Hard (?): 
    - more colors
    - longer code
    - less attemps

- Create main game logic, independent of representation
  - use unit test to verify it works
  - no output (maybe add logging later)
- Create console app
  - simplest version first (one game, app ends)
  - simple line by line, all text
  - then add menu/retry, ...
  - add points system
  - optional: 
    - add dependency injection
    - add logging (Serilog)
- Create WPF app    