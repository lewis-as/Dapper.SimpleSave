﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace PS.Mothership.Core.Common.Dto.Contact
{
    [DataContract]
    public class CountyDto
    {
        [DataMember]
        public long CountyKey { get; set; }

        [DataMember]
        public string CountyName { get; set; }

        [DataMember]
        public long CountryKey { get; set; }

        [DataMember]
        public CountryDto Country { get; set; }
    }
}
