/* 
 * Revision: 1 [24.07.2010] 
 * Author: Fuks Alexander
 * Reason: New assembly 
 * 
 * Revision: 2 [17.04.2012]
 * Author: Fuks Alexander
 * Reason: Cosmetic changes
 */ 

using System;
using System.Collections.Generic;
using System.Text;
using System.Resources;
using System.Reflection;

namespace Traffic_Accounting
{
    public class Messages
    {
        private ResourceManager resource;

        public enum Language
        {
            English, 
            Русский
        }

        public Messages(Language language)
        {
            selectLanguage(language);
        }

        public Messages()
        {

        }

        public void selectLanguage(Language language)
        {
            Assembly assembly = Assembly.GetExecutingAssembly();

            switch (language)
            {
                case Language.English:
                    resource = new ResourceManager("EN", assembly);
                    break;
                case Language.Русский:
                    resource = new ResourceManager("RU", assembly);
                    break;
            }
        }

        public string getMessage(string messageCode)
        {
            return resource.GetString(messageCode);
        }
    }
}
