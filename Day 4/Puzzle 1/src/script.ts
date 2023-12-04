import { readFileSync } from "fs";

const lines = readAllLines("input.txt");
let sumOfPoints = 0;
lines.forEach((line) => {
  sumOfPoints += getPoints(line);
});
console.log(sumOfPoints);

function getPoints(line: string): number {
  var numbers = getNumbers(line);

  const drawnWinnungNumbers = numbers.drawnNumbers.filter((drawnNumber) =>numbers.winningNumbers.includes(drawnNumber)).length;
  if (drawnWinnungNumbers > 0) {
    return Math.pow(2, drawnWinnungNumbers - 1);
  } else {
    return 0;
  }
}

function getNumbers(line: string): {
  winningNumbers: number[];
  drawnNumbers: number[];
} {
  const startIndex = line.indexOf(":");
  const separatorIndex = line.indexOf("|");

  const winningNumbers: number[] = [];
  const drawnNumbers: number[] = [];

  const regex = new RegExp("\\d+", "g");
  regex.lastIndex = startIndex;
  var match;
  while ((match = regex.exec(line)) != null) {
    if (match.index < separatorIndex) {
      winningNumbers.push(+match[0]);
    } else {
      drawnNumbers.push(+match[0]);
    }
  }

  return { winningNumbers: winningNumbers, drawnNumbers: drawnNumbers };
}

function readAllLines(filePath: string): string[] {
  return readFileSync(filePath, "utf-8").split(/\r?\n/);
}
