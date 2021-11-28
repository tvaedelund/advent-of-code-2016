using System.Diagnostics;

Console.WriteLine("AoC 2016 Day08");
var input = File.ReadAllText("input.txt").Trim();

// input = "ADVENT";
// input = "A(1x5)BC";
// input = "(3x3)XYZ";
// input = "A(2x2)BCD(2x2)EFG";
// input = "(6x1)(1x3)A";
// input = "X(8x2)(3x3)ABCY";

// input = "X(8x2)(3x3)ABCY";
// input = "(27x12)(20x12)(13x14)(7x10)(1x12)A";
// input = "(25x3)(3x3)ABC(2x3)XY(5x2)PQRSTX(18x9)(3x2)TWO(5x7)SEVEN";

var sw = Stopwatch.StartNew();

Console.WriteLine($"Part1: {Solve(input)}");
Console.WriteLine($"Time: {sw.ElapsedMilliseconds}ms");

Console.WriteLine();

sw.Restart();
Console.WriteLine($"Part2: {Solve(input, true)}");
Console.WriteLine($"Time: {sw.ElapsedMilliseconds}ms");


int Solve(string data, bool isV2 = false)
{
    var result = 0;

    for (int i = 0; i < input.Length; i++)
    {
        if (input[i] != '(')
        {
            result += 1;
            continue;
        }

        var seq = (isV2) ? decompV2(data.Substring(i)) : decompV1(data.Substring(i));

        result += seq.len;

        i += seq.pos;
    }

    return result;
}

(int len, int pos) decompV1(string seq)
{
    var pos = seq.IndexOf(')');
    var marker = seq.Substring(1, pos - 1).Split('x').Select(int.Parse).ToArray();

    var length = marker[0] * marker[1];

    return (length, pos + marker[0]);
}

(int len, int pos) decompV2(string seq)
{
    var pos = seq.IndexOf(')');
    var marker = seq.Substring(1, pos - 1).Split('x').Select(int.Parse).ToArray();

    var length = marker[0] * marker[1];

    return (length, pos + marker[0]);
}
