using BenchmarkDotNet.Attributes;

namespace AdventOfCode.Year2015;

public class AoC2015Day22 {
    private const int SPELL_MAGICMISSILE = 0;
    private const int SPELL_DRAIN = 1;
    private const int SPELL_SHIELD = 2;
    private const int SPELL_POISON = 3;
    private const int SPELL_RECHARGE = 4;

    private const int MANA_COST_MAGICMISSILE = 53;
    private const int MANA_COST_DRAIN = 73;
    private const int MANA_COST_SHIELD = 113;
    private const int MANA_COST_POISON = 173;
    private const int MANA_COST_RECHARGE = 229;

    private const int DAMAGE_MAGICMISSILE = 4;
    private const int DAMAGE_DRAIN = 2;
    private const int HEAL_DRAIN = 2;
    private const int ARMOR_SHIELD = 7;
    private const int DAMAGE_POISON = 3;
    private const int MANA_RECHARGE = 101;

    private const int TIMER_SHIELD = 6;
    private const int TIMER_POISON = 6;
    private const int TIMER_RECHARGE = 5;

    private struct GameState {
        public int Mana;
        public int ManaSpend;
        public int PlayerHP;
        public int BossHP;
        public int BossDmg;
        public bool PlayerTurn;
        public int ShieldTimer;
        public int PoisonTimer;
        public int RechargeTimer;
        public int PlayerArmor;

        public GameState(int mana, int manaSpend, int playerHp, int bossHp, int bossDmg, bool playerTurn, int shieldTimer, int poisonTimer, int rechargeTimer) {
            Mana = mana;
            ManaSpend = manaSpend;
            PlayerHP = playerHp;
            BossHP = bossHp;
            BossDmg = bossDmg;
            PlayerTurn = playerTurn;
            ShieldTimer = shieldTimer;
            PoisonTimer = poisonTimer;
            RechargeTimer = rechargeTimer;
            PlayerArmor = 0;
        }
    }

    [Benchmark]
    public long Solution1() {
        return Run(false);
    }

    [Benchmark]
    public long Solution2() {
        return Run(true);
    }

    private long Run(bool hardMode) {
        var games = new Stack<GameState>();
        games.Push(new GameState(500, 0, 50, 58, 9, true, 0, 0, 0));

        GameState bestGame = games.Peek();
        bestGame.ManaSpend = int.MaxValue;

        while (games.TryPop(out var game)) {
            if (game.ManaSpend > bestGame.ManaSpend) {
                continue;
            }

            if (hardMode) {
                game.PlayerHP--;
            }

            // apply effects
            if (game.ShieldTimer > 0) {
                game.ShieldTimer--;
                if (game.ShieldTimer == 0) {
                    game.PlayerArmor = 0;
                }
            }

            if (game.PoisonTimer > 0) {
                game.BossHP -= DAMAGE_POISON;
                game.PoisonTimer--;

                if (game.BossHP <= 0) {
                    // PLAYER WON
                    if (bestGame.ManaSpend > game.ManaSpend) {
                        bestGame = game;
                    }

                    continue;
                }
            }

            if (game.RechargeTimer > 0) {
                game.Mana += MANA_RECHARGE;
                game.RechargeTimer--;
            }

            if (game.PlayerTurn) {
                for (int i = 0; i < 5; i++) {
                    switch (i) {
                        case SPELL_MAGICMISSILE: {
                            if (game.Mana < MANA_COST_MAGICMISSILE) {
                                continue;
                            }

                            var nextState = game;
                            nextState.BossHP -= DAMAGE_MAGICMISSILE;
                            nextState.Mana -= MANA_COST_MAGICMISSILE;
                            nextState.ManaSpend += MANA_COST_MAGICMISSILE;
                            nextState.PlayerTurn = false;

                            if (nextState.BossHP <= 0) {
                                // PLAYER WON
                                if (bestGame.ManaSpend > nextState.ManaSpend) {
                                    bestGame = nextState;
                                }

                                continue;
                            }

                            games.Push(nextState);
                            break;
                        }
                        case SPELL_DRAIN: {
                            if (game.Mana < MANA_COST_DRAIN) {
                                continue;
                            }

                            var nextState = game;
                            nextState.PlayerHP += HEAL_DRAIN;
                            nextState.BossHP -= DAMAGE_DRAIN;
                            nextState.Mana -= MANA_COST_DRAIN;
                            nextState.ManaSpend += MANA_COST_DRAIN;
                            nextState.PlayerTurn = false;

                            if (nextState.BossHP <= 0) {
                                // PLAYER WON
                                if (bestGame.ManaSpend > nextState.ManaSpend) {
                                    bestGame = nextState;
                                }

                                continue;
                            }

                            games.Push(nextState);

                            break;
                        }
                        case SPELL_SHIELD: {
                            if (game.ShieldTimer > 0 || game.Mana < MANA_COST_SHIELD) {
                                continue;
                            }

                            var nextState = game;
                            nextState.ShieldTimer = TIMER_SHIELD;
                            nextState.Mana -= MANA_COST_SHIELD;
                            nextState.ManaSpend += MANA_COST_SHIELD;
                            nextState.PlayerArmor = ARMOR_SHIELD;
                            nextState.PlayerTurn = false;

                            games.Push(nextState);
                            break;
                        }
                        case SPELL_POISON: {
                            if (game.PoisonTimer > 0 || game.Mana < MANA_COST_POISON) {
                                continue;
                            }

                            var nextState = game;
                            nextState.PoisonTimer = TIMER_POISON;
                            nextState.Mana -= MANA_COST_POISON;
                            nextState.ManaSpend += MANA_COST_POISON;
                            nextState.PlayerTurn = false;
                            games.Push(nextState);
                            break;
                        }
                        case SPELL_RECHARGE: {
                            if (game.RechargeTimer > 0 || game.Mana < MANA_COST_RECHARGE) {
                                continue;
                            }

                            var nextState = game;
                            nextState.RechargeTimer = TIMER_RECHARGE;
                            nextState.Mana -= MANA_COST_RECHARGE;
                            nextState.ManaSpend += MANA_COST_RECHARGE;
                            nextState.PlayerTurn = false;
                            games.Push(nextState);
                            break;
                        }
                    }
                }
            } else {
                var damage = Math.Max(1, game.BossDmg - game.PlayerArmor);
                game.PlayerHP -= damage;
                if (game.PlayerHP <= 0) {
                    // PLAYER LOST
                    continue;
                }

                game.PlayerTurn = true;
                games.Push(game);
            }
        }

        return bestGame.ManaSpend;
    }
}