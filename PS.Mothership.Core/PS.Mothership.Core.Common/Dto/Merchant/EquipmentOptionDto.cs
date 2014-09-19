﻿using System;
using System.Runtime.Serialization;
using PS.Mothership.Core.Common.Template.Gen;
using PS.Mothership.Core.Common.Template.Opp;
using PS.Mothership.Core.Common.Template.Prod;

namespace PS.Mothership.Core.Common.Dto.Merchant
{
    [DataContract]
    public class EquipmentOptionDto
    {
        [DataMember]
        public Guid EquipmentOptionTrnGuid { get; set; }

        [DataMember]
        public Guid OfferGuid { get; set; }

        [DataMember]
        public Guid EquipmentOfferGuid { get; set; }

        [DataMember]
        public int EquipmentOptionKey { get; set; }

        [DataMember]
        public int Quantity { get; set; }

        [DataMember]
        public double SellPrice { get; set; }

        [DataMember]
        public double WholesaleCost { get; set; }

        [DataMember]
        public GenRecStatusEnum RecStatusKey { get; set; }
        
    }
}
