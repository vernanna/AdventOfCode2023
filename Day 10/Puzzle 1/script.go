package main

import (
	"bufio"
	"fmt"
	"os"
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
	maximumRowIndex := len(tiles) - 1
	maximumColumnIndex := len(tiles[0]) - 1
	numberOfSteps := 0

	for {
		var nextTile Tile
		if currentTile.column > 0 {
			tileLeft := tiles[currentTile.row][currentTile.column - 1]
			if tileLeft != lastTile && connectsTo(currentTile, tileLeft) {
				nextTile = tileLeft
			}
		}
		if currentTile.column < maximumColumnIndex {
			tileRight := tiles[currentTile.row][currentTile.column + 1]
			if tileRight != lastTile && connectsTo(currentTile, tileRight) {
				nextTile = tileRight
			}
		}
		if currentTile.row > 0 {
			tileAbove := tiles[currentTile.row - 1][currentTile.column]
			if tileAbove != lastTile && connectsTo(currentTile, tileAbove) {
				nextTile = tileAbove
			}
		}
		if currentTile.row < maximumRowIndex {
			tileBelow := tiles[currentTile.row + 1][currentTile.column]
			if tileBelow != lastTile && connectsTo(currentTile, tileBelow) {
				nextTile = tileBelow
			}
		}

		lastTile = currentTile
		currentTile = nextTile

		numberOfSteps++
		if currentTile == startTile {
			break
		}
	}

	fmt.Println(numberOfSteps / 2)
}

func connectsTo(firstTile Tile, secondTile Tile) bool {
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