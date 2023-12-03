import { readFileSync } from "fs";

const lines = readAllLines("input.txt");
const partNumbersPerLine = getPartNumbersPerLine(lines);
const gearRatios = getGearRatios(lines, partNumbersPerLine);

console.log(sum(gearRatios));

function getGearRatios(
  lines: string[],
  partNumbersPerLine: {
    line: number;
    partNumbers: { number: number; adjacentIndexes: number[] }[];
  }[]
): number[] {
  const gearRatios: number[] = [];
  lines.forEach((line, index) => {
    var starSymbolIndexes = getSymbolIndexes(line, "*");
    var partNumbersInAdjacentLines = partNumbersPerLine.filter((partNumbers) => index - 1 <= partNumbers.line && index + 1 >= partNumbers.line).flatMap(partNumbers => partNumbers.partNumbers);
    
    starSymbolIndexes.forEach(starSymbolIndex => {
      const adjacentPartNumbers = partNumbersInAdjacentLines.filter(partNumber => partNumber.adjacentIndexes.includes(starSymbolIndex));
      if(adjacentPartNumbers.length === 2) {
        gearRatios.push(adjacentPartNumbers[0].number * adjacentPartNumbers[1].number);
      }
    });
  });

  return gearRatios;
}

function getPartNumbersPerLine(lines: string[]): {
  line: number;
  partNumbers: { number: number; adjacentIndexes: number[] }[];
}[] {
  const partNumbersPerLine: {
    line: number;
    partNumbers: { number: number; adjacentIndexes: number[] }[];
  }[] = [];
  lines.forEach((line, index) => {
    const previousLine = index > 0 ? lines[index - 1] : ".".repeat(line.length);
    const nextLine =
      index < lines.length - 1 ? lines[index + 1] : ".".repeat(line.length);

    partNumbersPerLine.push({
      line: index,
      partNumbers: getPartNumbers(line, previousLine, nextLine),
    });
  });

  return partNumbersPerLine;
}

function getPartNumbers(
  line: string,
  previousLine: string,
  nextLine: string
): { number: number; adjacentIndexes: number[] }[] {
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

  return numbersAndAdjacentIndexes.filter((numberAndAdjacentIndexes) =>
    numberAndAdjacentIndexes.adjacentIndexes.some((adjacentIndex) =>
      symbolIndexes.includes(adjacentIndex)
    )
  );
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

function getSymbolIndexes(line: string, symbol: string): number[] {
  const regex = new RegExp("\\" + symbol, "g");

  const symbolIndexes: number[] = [];
  var match;
  while ((match = regex.exec(line)) != null) {
    symbolIndexes.push(match.index);
  }

  return symbolIndexes;
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
