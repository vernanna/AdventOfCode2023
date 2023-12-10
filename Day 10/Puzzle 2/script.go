package main

import (
	"bufio"
	"fmt"
	"os"
	"slices"
	"strings"
)

type Tile struct {
	row int
	column int
	symbol string
 }

func main() {
    file, _ := os.Open("input.txt")
    defer file.Close()

	var tiles [][]Tile
	var startTile Tile

    scanner := bufio.NewScanner(file)
    for scanner.Scan() {
		line := scanner.Text()
		symbols := strings.Split(line, "")
		row := len(tiles)
		var tilesInLine []Tile

		for i := 0; i < len(symbols); i++ {
			tile := Tile{row: row, column: i, symbol: symbols[i]}
			tilesInLine = append(tilesInLine, tile)
			if(tile.symbol == "S") {
				startTile = tile
			}
		}

		tiles = append(tiles, tilesInLine)
    }

	var lastTile Tile
	currentTile := startTile
	var tilesInLoop []Tile

	for {
		tilesInLoop = append(tilesInLoop, currentTile)
		var nextTile Tile
		if currentTile.hasTileLeft() {
			tileLeft := currentTile.tileLeft(tiles)
			if tileLeft != lastTile && currentTile.connectsTo(tileLeft) {
				nextTile = tileLeft
			}
		}
		if currentTile.hasTileRight(tiles) {
			tileRight := currentTile.tileRight(tiles)
			if tileRight != lastTile && currentTile.connectsTo(tileRight) {
				nextTile = tileRight
			}
		}
		if currentTile.hasTileAbove() {
			tileAbove := currentTile.tileAbove(tiles)
			if tileAbove != lastTile && currentTile.connectsTo(tileAbove) {
				nextTile = tileAbove
			}
		}
		if currentTile.hasTileBelow(tiles) {
			tileBelow := currentTile.tileBelow(tiles)
			if tileBelow != lastTile && currentTile.connectsTo(tileBelow) {
				nextTile = tileBelow
			}
		}

		lastTile = currentTile
		currentTile = nextTile

		if currentTile == startTile {
			break
		}
	}

	// Remove all symbols that are not part of the main loop as they are not relevant
	var cleanedTiles [][]Tile
	for row := 0; row < len(tiles); row++ {
		var tilesInRow []Tile
		for column := 0; column < len(tiles[row]); column++ {
			tile := tiles[row][column]
			var symbol string
			if !slices.Contains(tilesInLoop, tile) {
				symbol = "."
			} else {
				symbol = tile.symbol
			}
			tilesInRow = append(tilesInRow, Tile{row: tile.row, column: tile.column, symbol: symbol})
		}
		cleanedTiles = append(cleanedTiles, tilesInRow)
	}
	tiles = cleanedTiles

	var extendedTiles [][]Tile
	for r := 0; r < len(tiles); r++ {
		row := len(extendedTiles)
		var tilesInRow []Tile
		var tilesInInsertedRow []Tile
		for c := 0; c < len(tiles[r]); c++ {
			tile := tiles[r][c]
			tilesInRow = append(tilesInRow, Tile{row: row, column: len(tilesInRow), symbol: tile.symbol})
			if tile.hasTileRight(tiles) {
				var symbol string
				if tile.connectsTo(tile.tileRight(tiles)) {
					symbol = "-"
				} else {
					symbol = "*"
				}
				tilesInRow = append(tilesInRow, Tile{row: row, column: len(tilesInRow), symbol: symbol})
			}
			if tile.hasTileBelow(tiles) {
				var symbol string
				if tile.connectsTo(tile.tileBelow(tiles)) {
					symbol = "|"
				} else {
					symbol = "*"
				}
				tilesInInsertedRow = append(tilesInInsertedRow, Tile{row: row + 1, column: len(tilesInInsertedRow), symbol: symbol})
				if tile.hasTileRight(tiles) {
					tilesInInsertedRow = append(tilesInInsertedRow, Tile{row: row + 1, column: len(tilesInInsertedRow), symbol: "*"})
				}
			}
		}
		extendedTiles = append(extendedTiles, tilesInRow)
		if(len(tilesInInsertedRow) > 0) {
			extendedTiles = append(extendedTiles, tilesInInsertedRow)
		}
	}
	tiles = extendedTiles

	edgeTiles := getEdgeTiles(tiles)
	var outsideTiles []Tile

	for i := 0; i < len(edgeTiles); i++ {
		edgeTile := edgeTiles[i]
		if !edgeTile.isEmpty() || slices.Contains(outsideTiles, edgeTile) {
			continue
		}
		connectedTiles := edgeTile.getConnectedEmptyTiles(tiles, []Tile{})
		outsideTiles = append(outsideTiles, connectedTiles...)
	}

	numberOfOriginalOutsideTiles := 0
	for i := 0; i < len(outsideTiles); i++ {
		if outsideTiles[i].symbol == "." {
			numberOfOriginalOutsideTiles++
		}
	}

	numberOfOriginalEmptyTiles := 0
	for r := 0; r < len(tiles); r++ {
		for c := 0; c < len(tiles[r]); c++ {
			if(tiles[r][c].symbol == ".") {
				numberOfOriginalEmptyTiles++
			}
		}
	}

	fmt.Println(numberOfOriginalEmptyTiles - numberOfOriginalOutsideTiles)
}

func (tile Tile) hasTileLeft() bool {
	return tile.column > 0
}

func (tile Tile) hasTileRight(tiles [][]Tile) bool {
	return tile.column < len(tiles[tile.row]) - 1
}

func (tile Tile) hasTileAbove() bool {
	return tile.row > 0
}

func (tile Tile) hasTileBelow(tiles [][]Tile) bool {
	return tile.row < len(tiles) - 1
}

func (tile Tile) tileLeft(tiles [][]Tile) Tile {
	return tiles[tile.row][tile.column - 1]
}

func (tile Tile) tileRight(tiles [][]Tile) Tile {
	return tiles[tile.row][tile.column + 1]
}

func (tile Tile) tileAbove(tiles [][]Tile) Tile {
	return tiles[tile.row - 1][tile.column]
}

func (tile Tile) tileBelow(tiles [][]Tile) Tile {
	return tiles[tile.row + 1][tile.column]
}

func (tile Tile) isEmpty() bool {
	return tile.symbol == "." || tile.symbol == "*"
}

func (firstTile Tile) connectsTo(secondTile Tile) bool {
	// First tile is left to second tile
	if (firstTile.row == secondTile.row && firstTile.column == secondTile.column - 1 && 
		(firstTile.symbol == "S" || firstTile.symbol == "-" || firstTile.symbol == "L" || firstTile.symbol == "F") && 
		(secondTile.symbol == "S" || secondTile.symbol == "-" || secondTile.symbol == "J" || secondTile.symbol == "7")) ||
	// First tile is right to second tile
	(firstTile.row == secondTile.row && firstTile.column == secondTile.column + 1 && 
		(firstTile.symbol == "S" || firstTile.symbol == "-" || firstTile.symbol == "J" || firstTile.symbol == "7") && 
		(secondTile.symbol == "S" || secondTile.symbol == "-" || secondTile.symbol == "L" || secondTile.symbol == "F")) ||
	// First tile is above second tile
	(firstTile.row == secondTile.row - 1 && firstTile.column == secondTile.column && 
		(firstTile.symbol == "S" || firstTile.symbol == "|" || firstTile.symbol == "7" || firstTile.symbol == "F") && 
		(secondTile.symbol == "S" || secondTile.symbol == "|" || secondTile.symbol == "L" || secondTile.symbol == "J")) || 
	// First tile is under second tile
	(firstTile.row == secondTile.row + 1 && firstTile.column == secondTile.column && 
		(firstTile.symbol == "S" || firstTile.symbol == "|" || firstTile.symbol == "L" || firstTile.symbol == "J") && 
		(secondTile.symbol == "S" || secondTile.symbol == "|" || secondTile.symbol == "7" || secondTile.symbol == "F")) {
		return true
	}

	return false
}

func (tile Tile) getConnectedEmptyTiles(tiles [][]Tile, connectedTiles []Tile) []Tile {
	if(tile.hasTileLeft()) {
		tileLeft := tile.tileLeft(tiles)
		if tileLeft.isEmpty() && !slices.Contains(connectedTiles, tileLeft) {
			connectedTiles = append(connectedTiles, tileLeft)
			connectedTiles = tileLeft.getConnectedEmptyTiles(tiles, connectedTiles)
		}
	}
	if(tile.hasTileRight(tiles)) {
		tileRight := tile.tileRight(tiles)
		if tileRight.isEmpty() && !slices.Contains(connectedTiles, tileRight) {
			connectedTiles = append(connectedTiles, tileRight)
			connectedTiles = tileRight.getConnectedEmptyTiles(tiles, connectedTiles)
		}
	}
	if(tile.hasTileAbove()) {
		tileAbove := tile.tileAbove(tiles)
		if tileAbove.isEmpty() && !slices.Contains(connectedTiles, tileAbove) {
			connectedTiles = append(connectedTiles, tileAbove)
			connectedTiles = tileAbove.getConnectedEmptyTiles(tiles, connectedTiles)
		}
	}
	if(tile.hasTileBelow(tiles)) {
		tileBelow := tile.tileBelow(tiles)
		if tileBelow.isEmpty() && !slices.Contains(connectedTiles, tileBelow) {
			connectedTiles = append(connectedTiles, tileBelow)
			connectedTiles = tileBelow.getConnectedEmptyTiles(tiles, connectedTiles)
		}
	}

	return connectedTiles
}

func getEdgeTiles(tiles [][]Tile) []Tile {
	var edgeTiles []Tile 
	
	row := 0
	for column := 0; column < len(tiles[row]); column++ {
		tile := tiles[row][column]
		edgeTiles = append(edgeTiles, tile)
	}

	for row = 1; row < len(tiles) - 1; row++ {
		tilesInRow := tiles[row]
		edgeTiles = append(edgeTiles, tilesInRow[0])
		edgeTiles = append(edgeTiles, tilesInRow[len(tilesInRow) - 1])
	}

	row = len(tiles) - 1
	for column := 0; column < len(tiles[row]); column++ {
		tile := tiles[row][column]
		edgeTiles = append(edgeTiles, tile)
	}

	return edgeTiles
}

func printTiles(tiles [][]Tile) {
	for row := 0; row < len(tiles); row++ {
		for column := 0; column < len(tiles[row]); column++ {
			tile := tiles[row][column]
			fmt.Print(tile.symbol)
		}
		fmt.Println("")
	}
}