using BenchmarkDotNet.Attributes;

namespace AdventOfCode.Year2015;

public class AoC2015Day21 {
    private CharacterStats bossStats = new() { HitPoints = 100, Damage = 8, Armor = 2 };

    private List<ItemStats> swords = new() {
            new() { Gold = 8, Damage = 4, Armor = 0 },
            new() { Gold = 10, Damage = 5, Armor = 0 },
            new() { Gold = 25, Damage = 6, Armor = 0 },
            new() { Gold = 40, Damage = 7, Armor = 0 },
            new() { Gold = 74, Damage = 8, Armor = 0 },
    };

    private List<ItemStats> armors = new() {
            new() { Gold = 13, Damage = 0, Armor = 1 },
            new() { Gold = 31, Damage = 0, Armor = 2 },
            new() { Gold = 53, Damage = 0, Armor = 3 },
            new() { Gold = 75, Damage = 0, Armor = 4 },
            new() { Gold = 102, Damage = 0, Armor = 5 },
    };

    private List<ItemStats> rings = new() {
            new() { Gold = 25, Damage = 1, Armor = 0 },
            new() { Gold = 50, Damage = 2, Armor = 0 },
            new() { Gold = 100, Damage = 3, Armor = 0 },
            new() { Gold = 20, Damage = 0, Armor = 1 },
            new() { Gold = 40, Damage = 0, Armor = 2 },
            new() { Gold = 80, Damage = 0, Armor = 3 },
    };

    private struct CharacterStats {
        public int HitPoints = 0;
        public int Damage = 0;
        public int Armor = 0;
    }

    private struct ItemStats {
        public int Gold = 0;
        public int Damage = 0;
        public int Armor = 0;

        public ItemStats Add(ItemStats? stats) {
            if (stats == null) {
                return this;
            }

            return new ItemStats { Gold = Gold + stats.Value.Gold, Damage = Damage + stats.Value.Damage, Armor = Armor + stats.Value.Armor, };
        }
    }

    [GlobalSetup(Targets = new[] { nameof(Solution1), nameof(Solution2) })]
    public void BenchmarkSetup() {
        Setup();
    }

    public void Setup() {
    }

    [Benchmark]
    public long Solution1() {
        int minGold = int.MaxValue;
        BattleEachCombo(
                (won, stats) => {
                    if (won) {
                        minGold = Math.Min(minGold, stats.Gold);
                    }
                });

        return minGold;
    }

    [Benchmark]
    public long Solution2() {
        int maxGold = int.MinValue;
        BattleEachCombo(
                (won, stats) => {
                    if (won == false) {
                        maxGold = Math.Max(maxGold, stats.Gold);
                    }
                });

        return maxGold;
    }

    void BattleEachCombo(Action<bool, ItemStats> cb) {
        for (int sword = 0; sword < swords.Count; sword++) {
            ItemStats? s = swords[sword];
            for (int armor = 0; armor <= armors.Count; armor++) {
                ItemStats? a = armor == 0 ? null : armors[armor - 1];

                for (int ringA = 0; ringA <= rings.Count; ringA++) {
                    ItemStats? r1 = ringA == 0 ? null : rings[ringA - 1];
                    for (int ringB = 0; ringB <= rings.Count; ringB++) {
                        if (ringB != 0 && ringB == ringA) {
                            continue;
                        }

                        ItemStats? r2 = ringB == 0 ? null : rings[ringB - 1];
                        var stats = new ItemStats().Add(s).Add(a).Add(r1).Add(r2);
                        var won = Battle(new CharacterStats { HitPoints = 100, Damage = stats.Damage, Armor = stats.Armor }, bossStats);
                        cb(won, stats);
                    }
                }
            }
        }
    }

    bool Battle(CharacterStats player, CharacterStats boss) {
        var playerDmg = Math.Max(1, player.Damage - boss.Armor);
        var bossDmg = Math.Max(1, boss.Damage - player.Armor);

        var playerDefeatsIn = boss.HitPoints % playerDmg == 0 ? boss.HitPoints / playerDmg : boss.HitPoints / playerDmg + 1;
        var bossDefeatsIn = player.HitPoints % bossDmg == 0 ? player.HitPoints / bossDmg : player.HitPoints / bossDmg + 1;

        return playerDefeatsIn <= bossDefeatsIn;
    }
}