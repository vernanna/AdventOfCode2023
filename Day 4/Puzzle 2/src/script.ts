import { readFileSync } from "fs";

const lines = readAllLines("input.txt");
let cards: { number: number; numberOfWonCards: number }[] = [];
cards = lines.reverse().reduce((processedCards, line, index) => {
  return processedCards.concat([processCard(line, processedCards)]);
}, cards);
const numberOfCars = cards.length + sum(cards.map(card => card.numberOfWonCards));
console.log(numberOfCars);

function processCard(
  line: string,
  cards: { number: number; numberOfWonCards: number }[]
): { number: number; numberOfWonCards: number } {
  var numbers = getNumbers(line);
  const numberOfDrawnWinnungNumbers = numbers.drawnNumbers.filter(
    (drawnNumber) => numbers.winningNumbers.includes(drawnNumber)
  ).length;
  const wonCardNumbers = Array.from(
    { length: numberOfDrawnWinnungNumbers },
    (_, index) => index + numbers.cardNumber + 1
  );
  const wonCards = cards.filter((card) => wonCardNumbers.includes(card.number));

  return {
    number: numbers.cardNumber,
    numberOfWonCards:
      sum(wonCards.map((card) => card.numberOfWonCards)) +
      numberOfDrawnWinnungNumbers,
  };
}

function getNumbers(line: string): {
  cardNumber: number;
  winningNumbers: number[];
  drawnNumbers: number[];
} {
  const separatorIndex = line.indexOf("|");

  const winningNumbers: number[] = [];
  const drawnNumbers: number[] = [];

  const regex = new RegExp("\\d+", "g");
  const cardNumber = +regex.exec(line)![0];

  var match;
  while ((match = regex.exec(line)) != null) {
    if (match.index < separatorIndex) {
      winningNumbers.push(+match[0]);
    } else {
      drawnNumbers.push(+match[0]);
    }
  }

  return { cardNumber: cardNumber, winningNumbers: winningNumbers, drawnNumbers: drawnNumbers };
}

function readAllLines(filePath: string): string[] {
  return readFileSync(filePath, "utf-8").split(/\r?\n/);
}

function sum(numbers: number[]): number {
  return numbers.reduce((partialSum, x) => partialSum + x, 0);
}
