
using Richard2DGameFramework.Logging;
using Richard2DGameFramework.Model.Creatures;
using Richard2DGameFramework.Model.WorldObjects;

namespace Richard2DGameFramework.Worlds
{

    public class World
    {

        public int MaxX { get; set; }
        public int MaxY { get; set; }

        private readonly List<WorldObject> _worldObjects;
        private readonly List<Creature> _creatures;
        private readonly ILogger _logger;

        public World(int maxX, int maxY, ILogger logger)
        {
            MaxX = maxX;
            MaxY = maxY;
            _worldObjects = new List<WorldObject>();
            _creatures = new List<Creature>();
            _logger = logger;

            _logger.LogInfo($"Verden oprettet med dimensioner MaxX={MaxX}, MaxY={MaxY}.");
        }

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

        public void RemoveCreature(Creature creature)
        {
            if (_creatures.Remove(creature))
            {
                _logger.LogInfo($"Fjernet skabning: {creature.Name} fra verdenen.");

                // Afmeld eventen
                creature.Died -= Creature_Died;
            }
            else
            {
                _logger.LogWarning($"Skabningen: {creature.Name} findes ikke i verdenen.");
            }
        }

        // Event-handler metode
        private void Creature_Died(object sender, CreatureEventArgs e)
        {
            var deadCreature = e.Creature;

            _logger.LogInfo($"Observer: {deadCreature.Name} er død og fjernes fra verdenen.");

            // Fjern creature fra verdenen
            RemoveCreature(deadCreature);
        }

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

        public WorldObject GetObjectAt(int x, int y)
        {
            return _worldObjects.FirstOrDefault(obj => obj.X == x && obj.Y == y);
        }


        public Creature GetCreatureAt(int x, int y)
        {
            return _creatures.FirstOrDefault(creature => creature.X == x && creature.Y == y);
        }

        private bool IsWithinBounds(int x, int y)
        {
            return x >= 0 && x <= MaxX && y >= 0 && y <= MaxY;
        }

        public void DisplayCreatures()
        {
            _logger.LogInfo("Skabninger i verdenen:");
            foreach (var creature in _creatures)
            {
                _logger.LogInfo(creature.ToString());
            }
        }

        public void DisplayWorldObjects()
        {
            _logger.LogInfo("Objekter i verdenen:");
            foreach (var obj in _worldObjects)
            {
                _logger.LogInfo(obj.ToString());
            }
        }

        public List<Creature> GetCreatures()
        {
            return new List<Creature>(_creatures);
        }

        public List<WorldObject> GetWorldObjects()
        {
            return new List<WorldObject>(_worldObjects);
        }

        public override string ToString()
        {
            return $"{{MaxX={MaxX}, MaxY={MaxY}, Creatures={_creatures.Count}, WorldObjects={_worldObjects.Count}}}";
        }
    }
}
