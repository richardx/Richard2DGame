// File: World.cs
using Richard2DGameFramework.Logging;
using Richard2DGameFramework.Model.Creatures;
using Richard2DGameFramework.Model.WorldObjects;

namespace Richard2DGameFramework.Worlds
{
    /// <summary>
    /// Repræsenterer spillets verden, der indeholder skabninger og objekter.
    /// </summary>
    public class World
    {
        /// <summary>
        /// Maksimale X-koordinat for verdenen.
        /// </summary>
        public int MaxX { get; set; }

        /// <summary>
        /// Maksimale Y-koordinat for verdenen.
        /// </summary>
        public int MaxY { get; set; }

        private readonly List<WorldObject> _worldObjects;
        private readonly List<Creature> _creatures;
        private readonly ILogger _logger;

        /// <summary>
        /// Initialiserer en ny instans af <see cref="World"/> klassen med angivne dimensioner.
        /// </summary>
        /// <param name="maxX">Maksimale X-koordinat.</param>
        /// <param name="maxY">Maksimale Y-koordinat.</param>
        /// <param name="logger">Logger til brug for verdenens aktiviteter.</param>
        public World(int maxX, int maxY, ILogger logger)
        {
            MaxX = maxX;
            MaxY = maxY;
            _worldObjects = new List<WorldObject>();
            _creatures = new List<Creature>();
            _logger = logger;

            _logger.LogInfo($"Verden oprettet med dimensioner MaxX={MaxX}, MaxY={MaxY}.");
        }

        /// <summary>
        /// Tilføjer en skabning til verdenen, hvis positionen er inden for grænserne.
        /// </summary>
        /// <param name="creature">Skabningen der skal tilføjes.</param>
        public void AddCreature(Creature creature)
        {
            if (IsWithinBounds(creature.X, creature.Y))
            {
                _creatures.Add(creature);
                _logger.LogInfo($"Tilføjet skabning: {creature.Name} til position ({creature.X},{creature.Y}).");
            }
            else
            {
                _logger.LogWarning($"Kan ikke tilføje {creature.Name}. Position ({creature.X},{creature.Y}) er uden for verdenens grænser.");
            }
        }

        /// <summary>
        /// Fjerner en skabning fra verdenen.
        /// </summary>
        /// <param name="creature">Skabningen der skal fjernes.</param>
        public void RemoveCreature(Creature creature)
        {
            if (_creatures.Remove(creature))
            {
                _logger.LogInfo($"Fjernet skabning: {creature.Name} fra verdenen.");
            }
            else
            {
                _logger.LogWarning($"Skabningen: {creature.Name} findes ikke i verdenen.");
            }
        }

        /// <summary>
        /// Tilføjer et objekt til verdenen, hvis positionen er inden for grænserne.
        /// </summary>
        /// <param name="obj">Objektet der skal tilføjes.</param>
        public void AddWorldObject(WorldObject obj)
        {
            if (IsWithinBounds(obj.X, obj.Y))
            {
                _worldObjects.Add(obj);
                _logger.LogInfo($"Tilføjet objekt: {obj.Name} til position ({obj.X},{obj.Y}).");
            }
            else
            {
                _logger.LogWarning($"Kan ikke tilføje {obj.Name}. Position ({obj.X},{obj.Y}) er uden for verdenens grænser.");
            }
        }

        /// <summary>
        /// Fjerner et objekt fra verdenen.
        /// </summary>
        /// <param name="obj">Objektet der skal fjernes.</param>
        public void RemoveWorldObject(WorldObject obj)
        {
            if (_worldObjects.Remove(obj))
            {
                _logger.LogInfo($"Fjernet objekt: {obj.Name} fra verdenen.");
            }
            else
            {
                _logger.LogWarning($"Objektet: {obj.Name} findes ikke i verdenen.");
            }
        }

        /// <summary>
        /// Henter et objekt på en given position.
        /// </summary>
        /// <param name="x">X-koordinaten.</param>
        /// <param name="y">Y-koordinaten.</param>
        /// <returns>Objektet hvis fundet, ellers null.</returns>
        public WorldObject GetObjectAt(int x, int y)
        {
            return _worldObjects.FirstOrDefault(obj => obj.X == x && obj.Y == y);
        }

        /// <summary>
        /// Henter en skabning på en given position.
        /// </summary>
        /// <param name="x">X-koordinaten.</param>
        /// <param name="y">Y-koordinaten.</param>
        /// <returns>Skabningen hvis fundet, ellers null.</returns>
        public Creature GetCreatureAt(int x, int y)
        {
            return _creatures.FirstOrDefault(creature => creature.X == x && creature.Y == y);
        }

        /// <summary>
        /// Tjekker om en position er inden for verdenens grænser.
        /// </summary>
        /// <param name="x">X-koordinaten.</param>
        /// <param name="y">Y-koordinaten.</param>
        /// <returns>True hvis inden for grænserne, ellers false.</returns>
        private bool IsWithinBounds(int x, int y)
        {
            return x >= 0 && x <= MaxX && y >= 0 && y <= MaxY;
        }

        /// <summary>
        /// Viser alle skabninger i verdenen.
        /// </summary>
        public void DisplayCreatures()
        {
            _logger.LogInfo("Skabninger i verdenen:");
            foreach (var creature in _creatures)
            {
                _logger.LogInfo(creature.ToString());
            }
        }

        /// <summary>
        /// Viser alle objekter i verdenen.
        /// </summary>
        public void DisplayWorldObjects()
        {
            _logger.LogInfo("Objekter i verdenen:");
            foreach (var obj in _worldObjects)
            {
                _logger.LogInfo(obj.ToString());
            }
        }

        /// <summary>
        /// Henter en kopi af listen over skabninger.
        /// </summary>
        /// <returns>Liste over skabninger.</returns>
        public List<Creature> GetCreatures()
        {
            return new List<Creature>(_creatures);
        }

        /// <summary>
        /// Henter en kopi af listen over objekter.
        /// </summary>
        /// <returns>Liste over objekter.</returns>
        public List<WorldObject> GetWorldObjects()
        {
            return new List<WorldObject>(_worldObjects);
        }

        /// <summary>
        /// Returnerer en strengrepræsentation af verdenen.
        /// </summary>
        /// <returns>En streng, der repræsenterer verdenen.</returns>
        public override string ToString()
        {
            return $"{{MaxX={MaxX}, MaxY={MaxY}, Creatures={_creatures.Count}, WorldObjects={_worldObjects.Count}}}";
        }
    }
}
