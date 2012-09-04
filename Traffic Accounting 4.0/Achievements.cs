/*   
 *  Traffic Accounting 4.0
 *  Traffic reporting system
 *  Copyright (C) IT WORKS TEAM 2008-2012
 *  
 *  This program is free software; you can redistribute it and/or modify
 *  it under the terms of the GNU General Public License as published by
 *  the Free Software Foundation; either version 2 of the License, or
 *  (at your option) any later version.
 *  
 *  This program is distributed in the hope that it will be useful,
 *  but WITHOUT ANY WARRANTY; without even the implied warranty of
 *  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 *  GNU General Public License for more details.
 *  
 *  You should have received a copy of the GNU General Public License along
 *  with this program; if not, write to the Free Software Foundation, Inc.,
 *  51 Franklin Street, Fifth Floor, Boston, MA 02110-1301 USA.
 *  
 *  IT WORKS TEAM, hereby disclaims all copyright
 *  interest in the program ".NET Assemblies Collection"
 *  (which makes passes at compilers)
 *  written by Alexander Fuks.
 * 
 *  Alexander Fuks, 01 July 2010
 *  IT WORKS TEAM, Founder of the team.
 */

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
