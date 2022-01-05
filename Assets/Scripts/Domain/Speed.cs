using Models;

namespace Domain
{
    public static class Speed
    {
        public static float Get(float score)
        {
            var timeScale = 1f;
            
            if (score > (float) Level.Level1 && score < (float) Level.Level2) timeScale = 1.1f;
            else if (score > (float) Level.Level2 && score < (float) Level.Level3) timeScale = 1.2f;
            else if (score > (float) Level.Level3 && score < (float) Level.Level4) timeScale = 1.3f;
            else if (score > (float) Level.Level4 && score < (float) Level.Level5) timeScale = 1.4f;
            else if (score > (float) Level.Level5) timeScale = 1.6f;

            return timeScale;
        }
    }
}