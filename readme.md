# Checklist
## Queries (30%)
* (5%) allPokemons - Should return a list of all pokémons
* (5%) allBattles - Should return a collection of all battles. Contains a field
argument called status which is of type BattleStatus (enum) and should be
used to filter the data based on the status of the battle
* (5%) allPlayers - Should return a collection of all players
* (5%) pokemon - Should return a specific pokémon by id 
* (5%) battle - Should return a specific battle by id
* (5%) player - Should return a specific player by id
## Mutations (35%)
* (5%) addBattle - Create a battle between two players pokémons and returns the newly created battle
* (5%) attack - Attacks a pokemon within a battle and returns the result
* (5%) addPlayer - Create a player and return the newly created player matching
the Player type
* (5%) removePlayer - Marks a player as deleted and returns either true or an
error if something happened
* (5%) addPokemonToInventory - Add a pokémon to an inventory of a specific
player and returns either true or an error if something happened. A player can
only have one of each type - therefore no duplicates allowed in the inventory
* (5%) removePokemonFromInventory - Removes a pokémon from an
inventory of a specific player and returns either true or an error if something
happened
## Bonus (5%)
* Eliminate the N+1 problem by using DataLoader (https://www.nuget.org/packages/GraphQL.DataLoader) for connected data. All boilerplate setup and dependencies are already configured within Program.cs
* You can follow this guideline: https://graphql-dotnet.github.io/docs/guides/dataloader

