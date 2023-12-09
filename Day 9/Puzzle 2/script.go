package main

import (
	"bufio"
	"fmt"
	"os"
	"strconv"
	"strings"
)

type Sequence struct {
	values []int
	length int
 }

func main() {
    file, _ := os.Open("input.txt")
    defer file.Close()

	maximumSequenceLength := 0
	var sequences []Sequence

    scanner := bufio.NewScanner(file)
    for scanner.Scan() {
		stringValues := strings.Fields(scanner.Text())
		var values []int
		for index := range stringValues {
			value, _ := strconv.Atoi(stringValues[index])
			values = append(values, value)
		}
		length := len(values)
		sequence := Sequence{values: values, length: length}
		sequences = append(sequences, sequence)
		if maximumSequenceLength < length {
			maximumSequenceLength = length
		}
    }

	var coefficients [][]int
	for i := 0; i <= maximumSequenceLength + 1; i++ {
		var previousCoefficients []int
		if(i == 0) {
			previousCoefficients = []int{}
		} else {
			previousCoefficients = coefficients[i-1]
		}

		var coefficientsForSequence []int
		for index := 0; index < i; index++ {
			var coefficient int
			if index == 0 || index == i - 1 {
				coefficient = 1
			} else {
				coefficient = absoluteValue(previousCoefficients[index - 1]) + absoluteValue(previousCoefficients[index])
			}
			if(index%2 == 1) {
				coefficient = -coefficient
			}
			coefficientsForSequence = append(coefficientsForSequence, coefficient)
		}
		coefficients = append(coefficients, coefficientsForSequence)
	}

	sumOfPreviousValues := 0
	for index := range sequences {
		sequence := sequences[index]
		previousValue := getPreviousValue(sequence, coefficients)
		sumOfPreviousValues = sumOfPreviousValues + previousValue
	}

	fmt.Println(sumOfPreviousValues)
}

func getPreviousValue(sequence Sequence, coefficients [][]int) int {
	coefficientsForSequence := coefficients[sequence.length + 1]
	nextValue := 0
	for index := range sequence.values {
		value := sequence.values[index]
		coefficient := coefficientsForSequence[index + 1]
		nextValue = nextValue + (value * coefficient)
	}

	if(sequence.length%2 == 1) {
		return -nextValue
	} else {
		return nextValue
	}
}

func absoluteValue(x int) int {
	return absoluteDifference(x, 0)
 }

func absoluteDifference(x, y int) int {
	if x < y {
	   return y - x
	}
	return x - y
}