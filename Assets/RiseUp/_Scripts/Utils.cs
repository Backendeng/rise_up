namespace Superpow
{
    public class Utils
    {
        public static void SetGameMode(int value)
        {
            CPlayerPrefs.SetInt("game_mode", value);
        }

        public static int GetGameMode()
        {
            return CPlayerPrefs.GetInt("game_mode", 0);
        }

        public static int GetChallengeLevel()
        {
            return CPlayerPrefs.GetInt("challenge_level", 1);
        }

        public static void SetChallengeLevel(int value)
        {
            CPlayerPrefs.SetInt("challenge_level", value);
        }

        public static void SetBestScore(int value)
        {
            CPlayerPrefs.SetInt("best_scored", value);
        }

        public static int GetBestScore()
        {
            return CPlayerPrefs.GetInt("best_scored", 0);
        }

        public static readonly int CLASSIC_MODE = 0, CHALLENGE_MODE = 1;

        public static int GetGroupObIndex(string name)
        {
            var arr = name.Split(null);
            if (arr.Length != 2) return 0;

            int result;
            bool success = int.TryParse(arr[1], out result);
            if (success) return result;

            return 0;
        }
    }
}