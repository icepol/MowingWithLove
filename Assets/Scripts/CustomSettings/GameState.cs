namespace pixelook
{
    public static class GameState
    {
        private static int _flowersPlanted;
        private static int _enemyKilled;
        private static int _enemyShots;

        public static bool IsGameRunning { get; set; }
        
        public static bool IsLevelReady { get; set; }
        
        public static bool IsGameOver { get; set; }
        
        public static int FlowersPlanted
        {
            get => _flowersPlanted;
            set
            {
                _flowersPlanted = value;
                
                EventManager.TriggerEvent(Events.SCORE_CHANGED);
            }
        }
        
        public static int EnemyKilled
        {
            get => _enemyKilled;
            set
            {
                _enemyKilled = value;
                
                EventManager.TriggerEvent(Events.SCORE_CHANGED);
            }
        }
        
        public static int EnemyShots
        {
            get => _enemyShots;
            set
            {
                _enemyShots = value;
                
                EventManager.TriggerEvent(Events.SCORE_CHANGED);
            }
        }

        public static int Score => EnemyShots * 5 + EnemyKilled * 10 + FlowersPlanted * 25;
        
        public static void OnApplicationStarted()
        {
            EnemyShots = 0;
            FlowersPlanted = 0;
            IsGameRunning = false;
            IsLevelReady = false;
            IsGameOver = false;
        }

        public static void OnGameStarted()
        {
            EnemyShots = 0;
            FlowersPlanted = 0;
            IsGameRunning = true;
            IsLevelReady = false;
            IsGameOver = false;
        }
    }
}