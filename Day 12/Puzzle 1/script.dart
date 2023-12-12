import 'dart:ffi';
import 'dart:io';
import 'dart:convert';
import 'package:collection/collection.dart';
import 'package:darq/darq.dart';

class Row {
  List<String> springs;
  List<int> damagedSpringGroupLengths;
  int numberOfSprings;
  int numberOfDamagedSprings;

  Row(this.springs, this.damagedSpringGroupLengths)
      : numberOfSprings = springs.length,
        numberOfDamagedSprings = damagedSpringGroupLengths.sum;

  static Row Create(String line) {
    var parts = line.split(" ");
    var springs = parts[0].iterable.toList();
    var damagedSpringGroupLengths = parts[1].split(",").map((length) => int.parse(length)).toList();

    return Row(springs, damagedSpringGroupLengths);
  }

  int numberOfPermutations() {
    var permutations = [""];
    for (var spring in springs){
      if(spring != "?") {
        permutations = permutations.map((permutation) => permutation + spring).toList();
      }else{
        permutations = permutations.expand((permutation) => ["$permutation.", "$permutation#"]).toList();
      }
    }

    print("Permutations found: ${permutations.length}");

    var regex = RegExp(damagedSpringGroupLengths.map((groupLength) => "#{$groupLength}").join("\\.+"));
   
    return permutations.count((permutation) => permutation.iterable.count((spring) => spring == "#") == numberOfDamagedSprings && regex.hasMatch(permutation));
  }
}

main() async {
  var lines = await File("input.txt")
      .openRead()
      .map(utf8.decode)
      .transform(LineSplitter())
      .toList();

  var rows = lines.map((line) => Row.Create(line));

  var sumOfPermutations = rows.sum((row) => row.numberOfPermutations());
  print(sumOfPermutations);
}