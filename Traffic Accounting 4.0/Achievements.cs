using System.Collections.Generic;

namespace Traffic_Accounting
{
    internal class Achievements
    {
        private List<Achievement> AvailableAchievements = new List<Achievement>();


        public Achievements()
        {
            // specify available achievements
            Achievement LiveForTheMoment = new Achievement();
            LiveForTheMoment.UniqueCode = 001;
            LiveForTheMoment.Name = "Live for the moment";
            LiveForTheMoment.Description = "Present in case over 90 MB are spent on Monday.";
            LiveForTheMoment.Image = null;

            Achievement Incompetent = new Achievement();
            Incompetent.UniqueCode = 002;
            Incompetent.Name = "Incompetent";
            Incompetent.Description = "Present in case over 100 MB are spent over a week.";
            Incompetent.Image = null;

            Achievement Unstoppable = new Achievement();
            Unstoppable.UniqueCode = 003;
            Unstoppable.Name = "Unstoppable";
            Unstoppable.Description = "Present in case over 150 MB are spent over a week.";
            Unstoppable.Image = null;

            Achievement GodMode = new Achievement();
            GodMode.UniqueCode = 004;
            GodMode.Name = "God mode";
            GodMode.Description = "Present in case over 200 MB are spent over a week.";
            GodMode.Image = null;

            Achievement BoringFriday = new Achievement();
            BoringFriday.UniqueCode = 005;
            BoringFriday.Name = "Boring Friday";
            BoringFriday.Description = "Present in case over 95 MB are spent over a week but less than 1 MB on Friday.";
            BoringFriday.Image = null;

            Achievement Hardworker = new Achievement();
            Hardworker.UniqueCode = 006;
            Hardworker.Name = "Hardworker";
            Hardworker.Description = "Present in case less than 5 MB are spent over a week.";
            Hardworker.Image = null;

            Achievement Headshooter = new Achievement();
            Headshooter.UniqueCode = 007;
            Headshooter.Name = "Headshooter";
            Headshooter.Description = "Present in case 99 MB are spent over a week.";
            Headshooter.Image = null;
            // read achievements
        }
    }
}
