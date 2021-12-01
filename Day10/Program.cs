using System.Text.RegularExpressions;

var input = File.ReadAllLines("input.txt");

Console.WriteLine($"Part1: {Solve(input).p1}");
Console.WriteLine($"Part2: {Solve(input).p2}");

(int p1, int p2)  Solve(string[] data)
{
    var regex = new Regex(@"value (?<tarval>\d+) goes to bot (?<tarbot>\d+)|bot (?<srcbot>\d+) gives low to (?<mintar>bot|output) (?<minval>\d+) and high to (?<maxtar>bot|output) (?<maxval>\d+)");

    data = data.OrderBy(x => x).ToArray();

    var initBots = InitBots(data, regex);

    var result = Execute(data, initBots, regex);

    return result;
}

(int p1, int p2)  Execute(string[] data, Dictionary<int, List<int>> bots, Regex regex)
{
    var r1 = 0;
    var r2 = new int[100];

    var instrs = data
        .Select(x => regex.Match(x))
        .Where(x => x.Groups["srcbot"].Success)
        .Select(x => new
        {
            srcBot = int.Parse(x.Groups["srcbot"].Value),
            minTar = x.Groups["mintar"].Value,
            minVal = int.Parse(x.Groups["minval"].Value),
            maxTar = x.Groups["maxtar"].Value,
            maxVal = int.Parse(x.Groups["maxval"].Value)
        })
        .ToList();

    while (instrs.Any())
    {
        var srcBot = bots.First(x => x.Value.Count(y => y > 0) == 2);
        var instr = instrs.First(x => x.srcBot == srcBot.Key);

        instrs.Remove(instr);

        var minVal = srcBot.Value.Min();
        if (instr.minTar == "bot")
        {
            bots[instr.minVal].Add(minVal);
        }
        else
        {
            r2[instr.minVal] = minVal;
        }

        var maxVal = srcBot.Value.Max();
        if (instr.maxTar == "bot")
        {
            bots[instr.maxVal].Add(maxVal);
        }
        else
        {
            r2[instr.maxVal] = maxVal;
        }

        srcBot.Value.Clear();

        var bot = bots.Where(x => x.Value.Any()).FirstOrDefault(x => x.Value.Min() == 17 && x.Value.Max() == 61);
        if (bot.Key > 0)
        {
            r1 = bot.Key;
        }
    }

    return (r1, r2[0] * r2[1] * r2[2]);
}

Dictionary<int, List<int>> InitBots(string[] data, Regex regex)
{
    var bots = new Dictionary<int, List<int>>();

    foreach (var line in data)
    {
        var instr = regex.Match(line);
        if (instr.Groups["tarbot"].Success)
        {
            var tarBot = int.Parse(instr.Groups["tarbot"].Value);
            var tarVal = int.Parse(instr.Groups["tarval"].Value);
            if (bots.TryGetValue(tarBot, out var values))
            {
                values.Add(tarVal);
            }
            else
            {
                bots[tarBot] = new() { tarVal };
            }
        }
        else if (instr.Groups["srcbot"].Success)
        {
            var srcBot = int.Parse(instr.Groups["srcbot"].Value);
            bots[srcBot] = new();
        }
    }

    return bots;
}