using System.Text.RegularExpressions;
using MoreLinq;

var lines = File.ReadAllLines("input.txt").ToList();

var result = lines
    .Split(string.IsNullOrWhiteSpace, rows => rows.ToList())
    .Select(
        rows =>
        {
            for (var index = 0; index < rows.Count; index++)
            {
                if (index == rows.Count - 1)
                {
                    continue;
                }

                var rowsAbove = rows.Take(index + 1);
                var rowsBelow = rows.Skip(index + 1);
                var isMirrorRow = rowsAbove
                    .Reverse()
                    .Zip(rowsBelow)
                    .All(rowAboveAndRowBelow => rowAboveAndRowBelow.First == rowAboveAndRowBelow.Second);
                if (isMirrorRow)
                {
                    return (index + 1) * 100;
                }
            }

            var columns = rows.Transpose().Select(column => string.Join("", column)).ToList();
            for (var index = 0; index < columns.Count; index++)
            {
                if (index == columns.Count - 1)
                {
                    continue;
                }

                var columnsLeft = columns.Take(index + 1);
                var columnsRight = columns.Skip(index + 1);
                var isMirrorColumn = columnsLeft
                    .Reverse()
                    .Zip(columnsRight)
                    .All(rowAboveAndRowBelow => rowAboveAndRowBelow.First == rowAboveAndRowBelow.Second);
                if (isMirrorColumn)
                {
                    return index + 1;
                }
            }

            throw new Exception("Neither mirror row nor column found");
        })
    .Sum();

Console.WriteLine(result);