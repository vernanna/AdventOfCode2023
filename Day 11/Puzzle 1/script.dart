import 'dart:io';
import 'dart:convert';
import 'package:collection/collection.dart';
import 'package:darq/darq.dart';

class Galaxy {
  int row;
  int column;

  Galaxy(this.row, this.column);
  
  int distanceTo(Galaxy otherGalaxy) {
    return (row - otherGalaxy.row).abs() + (column - otherGalaxy.column).abs();
  }
}

main() async {
    var lines = await File("input.txt")
        .openRead()
        .map(utf8.decode)
        .transform(LineSplitter())
        .toList();
    var characters = lines.map((line) => line.iterable.toList()).toList();
    var rowsNumbersWithoutGalaxies = characters
        .mapIndexed((index, row) => row.all((character) => character == ".") ? index + 1 : 0)
        .where((index) => index > 0);
    var columnNumbersWithoutGalaxies = List<int>.generate(characters[0].length, (i) => i)
        .map((columnIndex) => characters.map((row) => row[columnIndex]))
        .mapIndexed((index, column) => column.all((character) => character == ".") ? index + 1 : 0)
        .where((index) => index > 0);
    var galaxies = lines.expandIndexed((index, element) => getGalaxies(element, index + 1, rowsNumbersWithoutGalaxies, columnNumbersWithoutGalaxies)).toList();
    var sumOfDistances = galaxies
        .cartesian(galaxies)
        .where((firstAndSecondGalaxy) => firstAndSecondGalaxy.item0 != firstAndSecondGalaxy.item1)
        .map((firstAndSecondGalaxy) => firstAndSecondGalaxy.item0.distanceTo(firstAndSecondGalaxy.item1))
        .sum / 2;

    print(sumOfDistances);
}

Iterable<Galaxy> getGalaxies(String line, int rowNumber, Iterable<int> rowsWithoutGalaxies, Iterable<int> columnsWithoutGalaxies) {
    rowNumber = rowNumber + rowsWithoutGalaxies.count((rowNumberWithoutGalaxy) => rowNumberWithoutGalaxy < rowNumber);
    return line.split("").mapIndexed((index, character) => character == "#" ? Galaxy(rowNumber, index + 1 + columnsWithoutGalaxies.count((columnNumberWithoutGalaxy) => columnNumberWithoutGalaxy < index + 1)) : null).whereType();
}
