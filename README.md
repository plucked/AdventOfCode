[![.NET](https://github.com/plucked/AdventOfCode/actions/workflows/dotnet.yml/badge.svg)](https://github.com/plucked/AdventOfCode/actions/workflows/dotnet.yml)

# Advent of Code

This repository contains solutions for the puzzles of the [Advent of Code](https://adventofcode.com/) from [Eric Wastl](http://was.tl/). There are 25 puzzles with two parts (except the 25th) were the second part is a follow up of the first part. 

All solutions are written in C# and executed as Unit test in the `AdventOfCodeTests` project. Additionally all tests can be run by using [BenchmarkDotNet](https://benchmarkdotnet.org/) via the `AdventOfCode` project. 

Most puzzles have their input defined in `[YEAR]_[DAY]_input.txt` files, but sometimes the input was so little that I decided to include them into the source directly.

The puzzle solution and input parsing is always separated.

Run all tests via the command line: `dotnet test AdventOfCodeTests/AdventOfCodeTests.csproj`
Run all benchmarks via the command line: `dotnet run --project AdventOfCode/AdventOfCode.csproj -- --filter '*'`


