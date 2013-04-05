/*   
 *  Traffic Accounting 4.0
 *  Traffic reporting system
 *  Copyright (C) Fuks Alexander 2008-2013
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
 *  Fuks Alexander, hereby disclaims all copyright
 *  interest in the program "Traffic Accounting"
 *  (which makes passes at compilers)
 *  written by Alexander Fuks.
 */

using System.Resources;
using Traffic_Accounting.MultiLanguages;

namespace Traffic_Accounting
{
    internal class Languages
    {
        private ResourceManager _rm;

        public Languages(string Language)
        {
            switch(Language)
            {
                case "Русский":
                    _rm = LangRU.ResourceManager;
                    break;
                default:
                    _rm = LangEN.ResourceManager;
                    break;
            }
        }

        public string GetMessage(string CODE)
        {
            return _rm.GetString(CODE);
        }
    }
}
