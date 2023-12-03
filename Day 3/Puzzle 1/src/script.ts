import { readFileSync } from "fs";

const lines = readAllLines("input.txt");
let sumOfPartNumbers = 0;
lines.forEach((line, index) => {
  const previousLine = index > 0 ? lines[index - 1] : ".".repeat(line.length);
  const nextLine = index < lines.length - 1 ? lines[index + 1] : ".".repeat(line.length);

  sumOfPartNumbers += sum(getPartNumbers(line, previousLine, nextLine));
});
console.log(sumOfPartNumbers);

function getPartNumbers(
  line: string,
  previousLine: string,
  nextLine: string
): number[] {
  const numbersAndAdjacentIndexes = getNumbers(line).map((number) => {
    return {
      number: number.number,
      adjacentIndexes: Array.from(
        { length: number.number.toString().length + 2 },
        (_, index) => index + number.index - 1
      ).filter((index) => index >= 0 && index < line.length),
    };
  });

  const symbolIndexes = distinct(
    getSymbols(line)
      .concat(getSymbols(previousLine))
      .concat(getSymbols(nextLine))
      .map((symbol) => symbol.index)
  );

  return numbersAndAdjacentIndexes
    .filter((numberAndAdjacentIndexes) =>
      numberAndAdjacentIndexes.adjacentIndexes.some((adjacentIndex) =>
        symbolIndexes.includes(adjacentIndex)
      )
    )
    .map((numberAndAdjacentIndexes) => numberAndAdjacentIndexes.number);
}

function getNumbers(line: string): { number: number; index: number }[] {
  const regex = new RegExp("\\d+", "g");

  const numbersInLine: { number: number; index: number }[] = [];
  var match;
  while ((match = regex.exec(line)) != null) {
    numbersInLine.push({ number: +match[0], index: match.index });
  }

  return numbersInLine;
}

function getSymbols(line: string): { symbol: string; index: number }[] {
  const regex = new RegExp("[^.\\d]", "g");

  const symbolsInLine: { symbol: string; index: number }[] = [];
  var match;
  while ((match = regex.exec(line)) != null) {
    symbolsInLine.push({ symbol: match[0], index: match.index });
  }

  return symbolsInLine;
}

function readAllLines(filePath: string): string[] {
  return readFileSync(filePath, "utf-8").split(/\r?\n/);
}

function sum(numbers: number[]): number {
  return numbers.reduce((partialSum, x) => partialSum + x, 0);
}

function distinct<T>(values: T[]): T[] {
  return [...new Set(values)];
}
