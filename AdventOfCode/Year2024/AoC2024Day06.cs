using AdventOfCode.Utilities;
using BenchmarkDotNet.Attributes;

namespace AdventOfCode.Year2024;

public class AoC2024Day06
{
    // input is negative when line is empty
    private readonly char[,] map;
    
    public AoC2024Day06(string? customInput = null)
    {
        var content = (customInput ?? EmbeddedInput.ReadAllText("Year2024/2024_06_input.txt"));
        var lines = content.Split('\n', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
        map = new char[lines.Length, lines[0].Length];
        for (int i = 0; i < lines.Length; i++)
        {
            var line = lines[i];
            for (int j = 0; j < line.Length; j++)
            {
                map[i, j] = line[j];
            }
        }
    }

    [Benchmark]
    public long Solution1()
    {
        var minX = 0;
        var maxX = map.GetLength(0);
        var minY = 0;
        var maxY = map.GetLength(1);
        // coordinates of every '#' in the map
        var obstacles = new HashSet<(int x, int y)>();
        for (int x = minX; x < maxX; x++)
        {
            for (int y = minY; y < maxY; y++)
            {
                if (map[x, y] == '#')
                {
                    obstacles.Add((x, y));
                }
            }
        }

        var guardPosition = (x: 0, y:0);
        for (int x = minX; x < maxX; x++)
        {
            bool found = false;
            for (int y = minY; y < maxY; y++)
            {
                if (map[x, y] == '^')
                {
                    guardPosition = (x, y);
                    found = true;
                    break;
                }
            }
            
            if (found)
            {
                break;
            }
        }
        
        var guardDirection = 'U';
        var visited = new HashSet<(int, int)>();
        
        while(guardPosition.x >= 0 && guardPosition.x < maxX && guardPosition.y >= 0 && guardPosition.y < maxY)
        {
            visited.Add(guardPosition);

            var nextPosition = guardDirection switch
            {
                'U' => (guardPosition.x - 1, guardPosition.y),
                'D' => (guardPosition.x + 1, guardPosition.y),
                'L' => (guardPosition.x, guardPosition.y - 1),
                'R' => (guardPosition.x, guardPosition.y + 1),
                _ => throw new InvalidOperationException()
            };
            if (obstacles.Contains(nextPosition))
            {
                guardDirection = guardDirection switch
                {
                    'U' => 'R',
                    'R' => 'D',
                    'D' => 'L',
                    'L' => 'U',
                    _ => guardDirection
                };
            }
            else
            {
                guardPosition = nextPosition;
            }
        }
        
        return visited.Count;
    }

    [Benchmark]
    public long Solution2()
    {
        var minX = 0;
        var maxX = map.GetLength(0);
        var minY = 0;
        var maxY = map.GetLength(1);
        var count = 0;
        for (int x = minX; x < maxX; x++)
        {
            for (int y = minY; y < maxY; y++)
            {
                if (map[x, y] != '#' && FindCyclicWithNewObstacle((x, y), minX, maxX, minY, maxY))
                {
                    count++;
                }
            }
        }
        
        return count;
    }
    
    private bool FindCyclicWithNewObstacle((int x, int y) newObstacle, int minX, int maxX, int minY, int maxY)
    {
        // coordinates of every '#' in the map
        var obstacles = new HashSet<(int x, int y)>();
        for (int x = minX; x < maxX; x++)
        {
            for (int y = minY; y < maxY; y++)
            {
                if (map[x, y] == '#')
                {
                    obstacles.Add((x, y));
                }
            }
        }

        var guardPosition = (x: 0, y:0);
        for (int x = minX; x < maxX; x++)
        {
            bool found = false;
            for (int y = minY; y < maxY; y++)
            {
                if (map[x, y] == '^')
                {
                    guardPosition = (x, y);
                    found = true;
                    break;
                }
            }
            
            if (found)
            {
                break;
            }
        }
        
        if (newObstacle == guardPosition)
        {
            return false;
        }
        
        obstacles.Add(newObstacle);
        
        var guardDirection = 'U';
        var visitedMovingUp = new HashSet<(int, int)>();
        var visitedMovingDown = new HashSet<(int, int)>();
        var visitedMovingLeft = new HashSet<(int, int)>();
        var visitedMovingRight = new HashSet<(int, int)>();
        
        while(guardPosition.x >= 0 && guardPosition.x < maxX && guardPosition.y >= 0 && guardPosition.y < maxY)
        {
            if ((guardDirection == 'U' && !visitedMovingUp.Add(guardPosition)) ||
                (guardDirection == 'D' && !visitedMovingDown.Add(guardPosition)) ||
                (guardDirection == 'L' && !visitedMovingLeft.Add(guardPosition)) ||
                (guardDirection == 'R' && !visitedMovingRight.Add(guardPosition)))
            {
                return true;
            }

            var nextPosition = guardDirection switch
            {
                'U' => (guardPosition.x - 1, guardPosition.y),
                'D' => (guardPosition.x + 1, guardPosition.y),
                'L' => (guardPosition.x, guardPosition.y - 1),
                'R' => (guardPosition.x, guardPosition.y + 1),
                _ => throw new InvalidOperationException()
            };
            if (obstacles.Contains(nextPosition))
            {
                guardDirection = guardDirection switch
                {
                    'U' => 'R',
                    'R' => 'D',
                    'D' => 'L',
                    'L' => 'U',
                    _ => guardDirection
                };
            }
            else
            {
                guardPosition = nextPosition;
            }
        }

        return false;
    }
}