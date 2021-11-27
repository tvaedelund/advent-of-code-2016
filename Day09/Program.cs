using System.Diagnostics;

Console.WriteLine("AoC 2016 Day08");
var sw = Stopwatch.StartNew();

var input = File.ReadAllText("input.txt").Trim();
// input = "ADVENT";
// input = "A(1x5)BC";
// input = "(3x3)XYZ";
// input = "A(2x2)BCD(2x2)EFG";
// input = "(6x1)(1x3)A";
// input = "X(8x2)(3x3)ABCY";

var decomp = string.Empty;
for (int i = 0; i < input.Length; i++)
{
    if (input[i] != '(')
    {
         decomp += input[i];
         continue;
    }

    var pos = input.IndexOf(')', i);
    var marker = input.Substring(i + 1, pos - i - 1).Split('x').Select(int.Parse).ToArray();

    for (int j = 0; j < marker[1]; j++)
    {
        decomp += input.Substring(pos + 1, marker[0]);
    }

    i = pos + marker[0];
}

var result = decomp.Length;

Console.WriteLine($"Result: {result}");
Console.WriteLine($"Time: {sw.ElapsedMilliseconds}ms");
