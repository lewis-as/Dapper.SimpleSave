﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PS.Mothership.Core.Common.Constants
{
    public class GlobalConstants
    {        
        public const char Dot = '.';
        public const char Comma = ',';
        public const char Space = ' ';
        public const char Star = '*';
        public const char At = '@';
        public const int IPAddressPadding = 3;
        public const int InitalMatch = 2;
        public const int ExactMatchScore = 5;
        public const int WildCardMatchScore = 1;
        public const double ExpiryDays = 30;
        public const int FirstNameScore = 2;
        public const int LastNameScore = 2;
    }
}
