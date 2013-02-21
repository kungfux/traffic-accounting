/*   
 *  Traffic Accounting 4.0
 *  Traffic reporting system
 *  Copyright (C) IT WORKS TEAM 2008-2013
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

using System.Drawing;
using System;
using Traffic_Accounting.Properties;

namespace Traffic_Accounting
{
    internal class SystemTray
    {
        /// <summary>
        /// draw icon with numbers
        /// </summary>
        public Icon getIcon(int value)
        {
            Color IconFontColor = Color.FromName(ClientParams.Parameters.TrayIconFontColor);
            Color IconBackColor = Color.FromArgb(ClientParams.Parameters.TrayIconBackColor);
            // Calculate font size.
            // If we will draw 1 digit - size can be big,
            // but if we will draw 2 digit - we should use smaller font size.
            // One more limit: we can draw only 1 and 2 digits, no more
            if (value > 999) value = 999;
            if (value < -999) value = -999;
            Font font = new Font(ClientParams.Parameters.TrayFontName, 
                ClientParams.Parameters.TrayFontSize, 
                FontStyle.Bold);

            // Change fore color according to traffic ranges
            if (ClientParams.Parameters.TrayDigitsColorRangesEnabled)
            {
                IconFontColor = getRangesColor(value);
            }
            if (ClientParams.Parameters.TrayBackColorRangesEnabled)
            {
                IconBackColor = getRangesColor(value);
            }
            // Draw new icon
            Bitmap bitmap = new Bitmap(32, 32);
            Graphics graphic = Graphics.FromImage(bitmap);
            SolidBrush backBrush = new SolidBrush(IconBackColor);
            SolidBrush foreBrush = new SolidBrush(IconFontColor);
            RectangleF canvas = new RectangleF(0, 0, 32, 32);

            switch(ClientParams.Parameters.IconFashion)
            {
                case 0: // nothing
                    break;
                case 1: // square
                    graphic.FillRectangle(backBrush, canvas);
                    break;
                case 2: // circle
                    graphic.FillEllipse(backBrush, canvas);
                    break;
                case 3: // triangle
                    if (IsColorIsAbnormal(value) == 0)
                    {
                        graphic.FillPolygon(backBrush,
                            new Point[] { new Point(16, 0), new Point(0, 32), new Point(32, 32) },
                             System.Drawing.Drawing2D.FillMode.Winding);
                    }
                    else
                    {
                        graphic.FillPolygon(backBrush,
                            new Point[] { new Point(0, 0), new Point(32, 0), new Point(16, 32) },
                             System.Drawing.Drawing2D.FillMode.Winding);
                    }
                    break;
                //case 3: // smiles
                //    switch(IsColorIsAbnormal(value))
                //    {
                //        case 1:
                //            graphic.DrawImage(Resources.exceded, 0, 0);
                //            break;
                //        case 2:
                //            graphic.DrawImage(Resources.panic, 0, 0);
                //            break;
                //        case 3:
                //            graphic.DrawImage(Resources.warning, 0, 0);
                //            break;
                //        default:
                //            graphic.DrawImage(Resources.smile_normal, 0, 0);
                //            break;
                //    }
                //    break;

            }
           
            if (ClientParams.Parameters.TrayDisplayDigits)
            {
                SizeF size = graphic.MeasureString(string.Format("{0:00}", Math.Abs(value)), font);
                graphic.DrawString(string.Format("{0:00}", Math.Abs(value)), font, foreBrush, (32 - size.Width)/2, (32 - size.Height) / 2);
            }
            return Icon.FromHandle(bitmap.GetHicon());
        }

        /// <summary>
        /// 0 - absurd (black)
        /// 1 - critical (red)
        /// 2 - warning (yellow)
        /// 3 - normal
        /// </summary>
        public int getRangesColorRepsentation(int value)
        {
            // { 0, 20, 50}
            for (int a = ClientParams.Parameters.TrayTrafficRanges.Length - 1; a >= 0; a--)
            {
                if (value > ClientParams.Parameters.TrayTrafficRanges[a])
                {
                    return ++a;
                }
            }
            return 0;
        }

        /// <summary>
        /// 0 - absurd (black)
        /// 1 - critical (red)
        /// 2 - warning (yellow)
        /// 3 - normal
        /// </summary>
        private Color getRangesColor(int value)
        {
            switch (getRangesColorRepsentation(value))
            {
                case 0:
                    return Color.Black;
                case 1:
                    return Color.Maroon; 
                case 2:
                    return Color.Goldenrod;
                case 3:
                    return Color.DarkGreen;
            }
            return Color.Transparent;
        }

        private int IsColorIsAbnormal(int value)
        {
            switch (getRangesColorRepsentation(value))
            {
                case 0:
                    return 1;
                case 1:
                    return 2;
                case 2:
                    return 3;
                case 3:
                    return 0;
            }
            return 0;
        }
    }
}
