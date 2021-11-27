using System.Diagnostics;
using System.Text;
using System.Text.RegularExpressions;

Console.WriteLine("AoC 2016 Day08");
var sw = Stopwatch.StartNew();

var input = File.ReadAllLines("input.txt");

var screen = new int[6, 50];

var debug = false;
if (debug)
{
    test();
}
else
{
    foreach (var instr in input)
    {
        var rect = Regex.Match(instr, @"rect (\d*)x(\d*)");
        if (rect.Success)
        {
            createRect(int.Parse(rect.Groups[1].Value), int.Parse(rect.Groups[2].Value));
            continue;
        }

        var col = Regex.Match(instr, @"column x=(\d*) by (\d*)");
        if (col.Success)
        {
            rotateCol(int.Parse(col.Groups[1].Value), int.Parse(col.Groups[2].Value));
            continue;
        }

        var row = Regex.Match(instr, @"row y=(\d*) by (\d*)");
        if (row.Success)
        {
            rotateRow(int.Parse(row.Groups[1].Value), int.Parse(row.Groups[2].Value));
            continue;
        }

        throw new Exception();
    }

    System.Console.WriteLine(GetPrintableScreen());
}

var result = count();

Console.WriteLine($"Result: {result}");
Console.WriteLine($"Time: {sw.ElapsedMilliseconds}ms");

int count()
{
    var result = 0;

    for (int y = 0; y < screen.GetLength(0); y++)
    {
        for (int x = 0; x < screen.GetLength(1); x++)
        {
            result += screen[y, x];
        }
    }

    return result;
}

void createRect(int a, int b)
{
    for (int y = 0; y < b; y++)
    {
        for (int x = 0; x < a; x++)
        {
            screen[y, x] = 1;
        }
    }
}

void rotateRow(int row, int by)
{
    by = by % screen.GetLength(1);

    for (int i = 0; i < by; i++)
    {
        var last = screen[row, screen.GetLength(1) - 1];
        for (int x = screen.GetLength(1) - 1; x > 0; x--)
        {
            screen[row, x] = screen[row, x - 1];
        }
        screen[row, 0] = last;
    }
}

void rotateCol(int col, int by)
{
    by = by % screen.GetLength(0);

    for (int i = 0; i < by; i++)
    {
        var last = screen[screen.GetLength(0) - 1, col];
        for (int y = screen.GetLength(0) - 1; y > 0; y--)
        {
            screen[y, col] = screen[y - 1, col];
        }
        screen[0, col] = last;
    }
}

string GetPrintableScreen()
{
    var sb = new StringBuilder();

    for (int y = 0; y < screen.GetLength(0); y++)
    {
        for (int x = 0; x < screen.GetLength(1); x++)
        {
            sb.Append(screen[y, x]);
        }

        sb.AppendLine();
    }

    return sb.ToString();
}

void test()
{
    screen = new int[3, 7];

    createRect(3, 2);

    rotateCol(1, 1);

    rotateRow(0, 4);

    rotateCol(1, 1);

    System.Console.WriteLine(GetPrintableScreen());
}