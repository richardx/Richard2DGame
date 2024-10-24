// Fil: ConfigReader.cs
using Richard2DGameFramework.Logging;
using Richard2DGameFramework.Model.Attack;
using Richard2DGameFramework.Model.Creatures;
using Richard2DGameFramework.Model.Defence;
using Richard2DGameFramework.Model.WorldObjects;
using Richard2DGameFramework.Worlds;
using System;
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
                logger.LogInfo($"Roden i XML: {configDoc.DocumentElement.Name}");
            }
            catch (Exception ex)
            {
                logger.LogError($"Fejl ved indlæsning af konfigurationsfil: {ex.Message}");
                throw;
            }

            // Find <World> elementet
            XmlNode worldNode = configDoc.SelectSingleNode("//World");
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
            if (creatureNodes == null || creatureNodes.Count == 0)
            {
                logger.LogWarning("Ingen skabninger fundet i konfigurationsfilen.");
            }
            else
            {
                foreach (XmlNode creatureNode in creatureNodes)
                {
                    try
                    {
                        Creature creature = new Creature();

                        creature.Name = creatureNode.SelectSingleNode("Name")?.InnerText.Trim() ?? "UnknownCreature";
                        creature.HitPoint = int.Parse(creatureNode.SelectSingleNode("HitPoint")?.InnerText.Trim() ?? "100");
                        creature.X = int.Parse(creatureNode.SelectSingleNode("X")?.InnerText.Trim() ?? "0");
                        creature.Y = int.Parse(creatureNode.SelectSingleNode("Y")?.InnerText.Trim() ?? "0");

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
                                    X = creature.X,
                                    Y = creature.Y
                                };
                                creature.AddDefence(defenceItem);
                            }
                        }

                        // Læs Attacks
                        XmlNodeList attackNodes = creatureNode.SelectNodes("Attacks/AttackItem");
                        if (attackNodes != null)
                        {
                            foreach (XmlNode attackNode in attackNodes)
                            {
                                string attackName = attackNode.SelectSingleNode("Name")?.InnerText.Trim() ?? "UnknownAttack";
                                int hit = int.Parse(attackNode.SelectSingleNode("Hit")?.InnerText.Trim() ?? "0");
                                int range = int.Parse(attackNode.SelectSingleNode("Range")?.InnerText.Trim() ?? "1");

                                AttackItem attackItem = new AttackItem
                                {
                                    Name = attackName,
                                    Hit = hit,
                                    Range = range,
                                    X = creature.X,
                                    Y = creature.Y
                                };
                                creature.AddAttack(attackItem);
                            }
                        }

                        // Læs MagicItems
                        XmlNodeList magicNodes = creatureNode.SelectNodes("MagicItems/MagicItem");
                        if (magicNodes != null)
                        {
                            foreach (XmlNode magicNode in magicNodes)
                            {
                                string magicName = magicNode.SelectSingleNode("Name")?.InnerText.Trim() ?? "UnknownMagic";
                                int magicPower = int.Parse(magicNode.SelectSingleNode("MagicPower")?.InnerText.Trim() ?? "0");

                                MagicItem magicItem = new MagicItem
                                {
                                    Name = magicName,
                                    MagicPower = magicPower,
                                    X = creature.X,
                                    Y = creature.Y
                                };
                                creature.AddMagic(magicItem);
                            }
                        }

                        world.AddCreature(creature);
                        logger.LogInfo($"Tilføjet skabning: {creature.Name} til position ({creature.X},{creature.Y}).");
                    }
                    catch (Exception ex)
                    {
                        logger.LogError($"Fejl ved indlæsning af skabning: {ex.Message}");
                        continue; // Fortsæt med næste skabning
                    }
                }
            }

            // Læs WorldObjects
            XmlNodeList objectNodes = worldNode.SelectNodes("WorldObjects/*");
            if (objectNodes == null || objectNodes.Count == 0)
            {
                logger.LogWarning("Ingen WorldObjects fundet i konfigurationsfilen.");
            }
            else
            {
                foreach (XmlNode objectNode in objectNodes)
                {
                    try
                    {
                        string type = objectNode.Name; // F.eks. "AttackItem", "DefenceItem", "MagicItem", "WorldObject"
                        string name = objectNode.SelectSingleNode("Name")?.InnerText.Trim() ?? "UnknownObject";
                        int x = int.Parse(objectNode.SelectSingleNode("X")?.InnerText.Trim() ?? "0");
                        int y = int.Parse(objectNode.SelectSingleNode("Y")?.InnerText.Trim() ?? "0");
                        bool lootable = bool.Parse(objectNode.SelectSingleNode("Lootable")?.InnerText.Trim() ?? "false");
                        bool removable = bool.Parse(objectNode.SelectSingleNode("Removable")?.InnerText.Trim() ?? "false");

                        WorldObject worldObject = null;

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

                            case "MagicItem":
                                int magicPower = int.Parse(objectNode.SelectSingleNode("MagicPower")?.InnerText.Trim() ?? "0");
                                worldObject = new MagicItem
                                {
                                    Name = name,
                                    MagicPower = magicPower,
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

                        world.AddWorldObject(worldObject);
                        logger.LogInfo($"Tilføjet objekt: {worldObject.Name} til position ({worldObject.X},{worldObject.Y}).");
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
