// File: ConfigReader.cs
using Richard2DGameFramework.Logging;
using Richard2DGameFramework.Model.Attack;
using Richard2DGameFramework.Model.Creatures;
using Richard2DGameFramework.Model.Defence;
using Richard2DGameFramework.Model.WorldObjects;
using Richard2DGameFramework.Worlds;
using System.Xml;

namespace Richard2DGameFramework.Configuration
{
    /// <summary>
    /// Håndterer indlæsning af spilkonfiguration fra en XML-fil.
    /// </summary>
    public static class ConfigReader
    {
        /// <summary>
        /// Indlæser konfigurationsfilen og opretter verdenen baseret på dens indhold.
        /// </summary>
        /// <param name="filename">Stien til XML-konfigurationsfilen.</param>
        /// <param name="logger">Logger til brug for konfigurationsaktiviteter.</param>
        /// <returns>En instans af <see cref="World"/> initialiseret med konfigurationsdata.</returns>
        public static World LoadConfig(string filename, ILogger logger)
        {
            XmlDocument configDoc = new XmlDocument();

            try
            {
                configDoc.Load(filename);
                logger.LogInfo($"Indlæser konfigurationsfil: {filename}");
            }
            catch (Exception ex)
            {
                logger.LogError($"Fejl ved indlæsning af konfigurationsfil: {ex.Message}");
                throw;
            }

            // Læs World elementet først for at få MaxX og MaxY
            XmlNode worldNode = configDoc.DocumentElement.SelectSingleNode("World");
            if (worldNode == null)
            {
                logger.LogError("World-konfiguration ikke fundet i XML-filen.");
                throw new Exception("World-konfiguration ikke fundet i XML-filen.");
            }

            int maxX, maxY;
            try
            {
                maxX = int.Parse(worldNode.SelectSingleNode("MaxX")?.InnerText.Trim() ?? "10");
                maxY = int.Parse(worldNode.SelectSingleNode("MaxY")?.InnerText.Trim() ?? "10");
                logger.LogInfo($"Verden dimensioner læst: MaxX={maxX}, MaxY={maxY}");
            }
            catch (Exception ex)
            {
                logger.LogError($"Fejl ved læsning af verdens dimensioner: {ex.Message}");
                throw;
            }

            // Opret en ny verden med de korrekte dimensioner
            World world = new World(maxX, maxY, logger);

            // Læs Creatures
            XmlNodeList creatureNodes = worldNode.SelectNodes("Creatures/Creature");
            if (creatureNodes == null)
            {
                logger.LogWarning("Ingen skabninger fundet i konfigurationsfilen.");
            }
            else
            {
                foreach (XmlNode creatureNode in creatureNodes)
                {
                    try
                    {
                        string name = creatureNode.SelectSingleNode("Name")?.InnerText.Trim() ?? "Unknown";
                        int hitPoint = int.Parse(creatureNode.SelectSingleNode("HitPoint")?.InnerText.Trim() ?? "100");
                        int x = int.Parse(creatureNode.SelectSingleNode("X")?.InnerText.Trim() ?? "0");
                        int y = int.Parse(creatureNode.SelectSingleNode("Y")?.InnerText.Trim() ?? "0");

                        Creature creature = new Creature
                        {
                            Name = name,
                            HitPoint = hitPoint,
                            X = x,
                            Y = y,
                            Attacks = new List<AttackItem>(), // Initialiser som tom liste
                            Defences = new List<DefenceItem>()
                        };

                        // Læs Defences
                        XmlNodeList defenceNodes = creatureNode.SelectNodes("Defences/DefenceItem");
                        if (defenceNodes != null)
                        {
                            foreach (XmlNode defenceNode in defenceNodes)
                            {
                                string defenceName = defenceNode.SelectSingleNode("Name")?.InnerText.Trim() ?? "UnknownDefence";
                                int reduceHitPoint = int.Parse(defenceNode.SelectSingleNode("ReduceHitPoint")?.InnerText.Trim() ?? "0");

                                DefenceItem defenceItem = new DefenceItem
                                {
                                    Name = defenceName,
                                    ReduceHitPoint = reduceHitPoint,
                                    X = x, // Position af skabning
                                    Y = y
                                };
                                // Tilføj DefenceItem til skabningen uden logning
                                creature.AddDefence(defenceItem);
                            }
                        }

                        world.AddCreature(creature); // Denne metode logger allerede tilføjelsen
                    }
                    catch (Exception ex)
                    {
                        logger.LogError($"Fejl ved indlæsning af skabning: {ex.Message}");
                        continue; // Fortsæt med næste skabning
                    }
                }
            }

            // Læs WorldObjects (inklusive AttackItem og DefenceItem)
            XmlNodeList objectNodes = worldNode.SelectNodes("WorldObjects/*");
            if (objectNodes == null)
            {
                logger.LogWarning("Ingen WorldObjects fundet i konfigurationsfilen.");
            }
            else
            {
                foreach (XmlNode objectNode in objectNodes)
                {
                    try
                    {
                        WorldObject worldObject = null;
                        string type = objectNode.Name; // F.eks. "AttackItem", "DefenceItem", "WorldObject"
                        string name = objectNode.SelectSingleNode("Name")?.InnerText.Trim() ?? "UnknownObject";
                        int x = int.Parse(objectNode.SelectSingleNode("X")?.InnerText.Trim() ?? "0");
                        int y = int.Parse(objectNode.SelectSingleNode("Y")?.InnerText.Trim() ?? "0");
                        bool lootable = bool.Parse(objectNode.SelectSingleNode("Lootable")?.InnerText.Trim() ?? "false");
                        bool removable = bool.Parse(objectNode.SelectSingleNode("Removable")?.InnerText.Trim() ?? "false");

                        switch (type)
                        {
                            case "AttackItem":
                                int hit = int.Parse(objectNode.SelectSingleNode("Hit")?.InnerText.Trim() ?? "0");
                                int range = int.Parse(objectNode.SelectSingleNode("Range")?.InnerText.Trim() ?? "0");
                                worldObject = new AttackItem
                                {
                                    Name = name,
                                    Hit = hit,
                                    Range = range,
                                    X = x,
                                    Y = y,
                                    Lootable = lootable,
                                    Removable = removable
                                };
                                break;

                            case "DefenceItem":
                                int reduceHitPoint = int.Parse(objectNode.SelectSingleNode("ReduceHitPoint")?.InnerText.Trim() ?? "0");
                                worldObject = new DefenceItem
                                {
                                    Name = name,
                                    ReduceHitPoint = reduceHitPoint,
                                    X = x,
                                    Y = y,
                                    Lootable = lootable,
                                    Removable = removable
                                };
                                break;

                            case "WorldObject":
                                worldObject = new WorldObject
                                {
                                    Name = name,
                                    X = x,
                                    Y = y,
                                    Lootable = lootable,
                                    Removable = removable
                                };
                                break;

                            default:
                                logger.LogWarning($"Ukendt WorldObject type: {type}. Objektet bliver ignoreret.");
                                continue; // Spring videre til næste objekt
                        }

                        world.AddWorldObject(worldObject); // Denne metode logger allerede tilføjelsen
                    }
                    catch (Exception ex)
                    {
                        logger.LogError($"Fejl ved indlæsning af WorldObject: {ex.Message}");
                        continue; // Fortsæt med næste objekt
                    }
                }
            }

            logger.LogInfo("Konfigurationsindlæsning fuldført.");
            return world;
        }
    }
}
