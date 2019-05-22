namespace shootstep.model.config
{
    public class EnemyOptions
    {
        private int _count;
        private int _cooldown;
        private readonly int _cooldownDefaultDelta;
        public int MaxEnemy { get; set; }
        public int Count 
        { 
            get => _count;
            set
            {
                _count = value;
                _cooldown = _cooldownDefaultDelta;
            }
        }
        public bool SpawnGranted => _cooldown == 0 && Count < MaxEnemy;

        public EnemyOptions(int cooldown, int maxEnemyCount)
        {
            _cooldownDefaultDelta = cooldown;
            MaxEnemy = maxEnemyCount;
        }

        public void Update()
        {
            if (_cooldown > 0) _cooldown--;
        }
    }
}