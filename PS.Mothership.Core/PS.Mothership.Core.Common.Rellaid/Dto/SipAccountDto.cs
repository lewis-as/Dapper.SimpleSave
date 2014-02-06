﻿using System;
using System.Runtime.Serialization;

namespace PS.Mothership.Core.Common.Rellaid.Dto
{
    [DataContract]
    public class SipAccountDto
    {
        [DataMember]
        public Guid SipAccountGuid { get; set; }

        [DataMember]
        public string Extension { get; set; }
    }
}
