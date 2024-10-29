using GameTestApp.Configuration;
using GameTestApp.Factories;
using Microsoft.Extensions.DependencyInjection;
using Richard2DGameFramework.Logging;
using Richard2DGameFramework.Model.Creatures;
using Richard2DGameFramework.Services;
using Richard2DGameFramework.Worlds;
using System.Diagnostics;

namespace GameTestApp
{
    class Program
    {
        static void Main(string[] args)
        {
            // Opsætning af Dependency Injection (DI)
            var serviceProvider = new ServiceCollection()
                .AddSingleton<ILogger, MyLogger>()
                .AddSingleton<ICombatService, CombatService>()
                .AddSingleton<ILootService, LootService>()
                .AddSingleton<ICreatureFactory, CreatureFactory>()
                .BuildServiceProvider();

            // Hent tjenester fra DI-containeren
            ILogger logger = serviceProvider.GetService<ILogger>();
            ICombatService combatService = serviceProvider.GetService<ICombatService>();
            ILootService lootService = serviceProvider.GetService<ILootService>();
            ICreatureFactory creatureFactory = serviceProvider.GetService<ICreatureFactory>();

            try
            {
                // Opsætning af logning (tilføj ConsoleTraceListener)
                var consoleListener = new ConsoleTraceListener();
                if (logger is MyLogger myLogger)
                {
                    myLogger.RegisterListener(consoleListener);
                }

                // Angiv stien til konfigurationsfilen
                string configFilePath = "gameconfig.xml";

                // Indlæs verdenen fra XML-konfigurationsfilen
                World world = ConfigReader.LoadConfig(configFilePath, logger, creatureFactory);
                logger.LogInfo($"Verden initialiseret: {world}");

                // Vis starttilstand
                world.DisplayWorldObjects();
                world.DisplayCreatures();

                // Test af angreb og død
                Creature goblin = world.GetCreatures().Find(c => c.Name == "Goblin");
                if (goblin == null)
                {
                    logger.LogError("Goblin blev ikke fundet i verdenen.");
                }
                else
                {
                    logger.LogInfo($"Goblin initial HitPoints: {goblin.HitPoint}");

                    // Goblin angriber sig selv for at dræbe sig selv
                    combatService.Attack(goblin, goblin);
                    logger.LogInfo($"{goblin.Name} angreb sig selv for at dræbe sig selv.");

                    // Vis tilstand efter selvangreb
                    world.DisplayCreatures();
                }

                // Skabninger looter objekter på deres positioner
                foreach (var creature in world.GetCreatures())
                {
                    var objAtPosition = world.GetObjectAt(creature.X, creature.Y);
                    if (objAtPosition != null)
                    {
                        lootService.Loot(creature, objAtPosition, world);
                        logger.LogInfo($"{creature.Name} looter {objAtPosition.Name}.");
                    }
                }

                // Vis tilstand efter looting
                world.DisplayCreatures();
                world.DisplayWorldObjects();

                // Skabninger angriber hinanden
                var creatures = world.GetCreatures();
                if (creatures.Count >= 2)
                {
                    Creature creature1 = creatures[0];
                    Creature creature2 = creatures[1];

                    // Skabning 1 angriber Skabning 2
                    combatService.Attack(creature1, creature2);
                    logger.LogInfo($"{creature1.Name} angreb {creature2.Name}.");

                    // Skabning 2 angriber Skabning 1
                    combatService.Attack(creature2, creature1);
                    logger.LogInfo($"{creature2.Name} angreb {creature1.Name}.");
                }

                // Vis sluttilstand
                world.DisplayCreatures();
            }
            catch (Exception ex)
            {
                logger.LogError($"Fejl ved indlæsning af konfiguration: {ex.Message}");
            }

            Console.WriteLine("Tryk på en tast for at afslutte...");
            Console.ReadKey();
        }
    }
}
