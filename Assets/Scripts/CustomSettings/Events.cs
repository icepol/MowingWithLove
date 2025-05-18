namespace pixelook
{
    public static class Events
    {
        // level progression events
        public const string RESTART_GAME = "RestartGame";
        public const string PRE_GAME_STARTED = "PreGameStarted";
        public const string GAME_STARTED = "GameStarted";
        public const string POST_GAME_STARTED = "PostGameStarted";
        public const string LEVEL_STARTED = "LevelStarted";
        public const string GAME_OVER = "GameOver";
        
        public const string PLAYER_FIRED = "PlayerFired";
        public const string PLAYER_MOVED_LEFT = "PlayerMovedLeft";
        public const string PLAYER_MOVED_RIGHT = "PlayerMovedRight";
        public const string PLAYER_DIED = "PlayerDied";
        public const string PLAYER_WALKED_STEP = "PlayerWalkedStep"; 
        
        public const string ENEMY_DIED = "EnemyDied";
        
        public const string SCORE_CHANGED = "ScoreChanged";

        // settings events
        public const string MUSIC_SETTINGS_CHANGED = "MusicSettingsChanged";
    }
}