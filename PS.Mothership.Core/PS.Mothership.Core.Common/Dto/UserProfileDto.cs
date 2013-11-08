﻿using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using PS.Mothership.Core.Common.Template.Usr;

namespace PS.Mothership.Core.Common.Dto
{
    /// <summary>
    /// User Proile Dto
    /// </summary>
    /// <remarks>
    ///     1. For caching purpose, we use the object as cache key, if the client want
    ///     to ignore certain property which are not any value, decorate those properties
    ///     with JsonIgnore, as the cache server convert the object to json for generating key
    /// </remarks>
    [DataContract]
    public class UserProfileDto
    {
        [DataMember]
        public bool IsLoggedIn { get; set; }
        //[DataMember]
        //public long UserId { get; set; }
        [DataMember]
        public Guid UserGuid { get; set; }
        [DataMember]
        public string UserName { get; set; }
        [DataMember]
        public bool IsPersistent { get; set; }
        [DataMember]
        public bool IsValid { get; set; }
        [DataMember]
        public bool CanImpersonate { get; set; }
        [DataMember]
        public bool IsImpersonate { get; set; }
        [DataMember]
        public LoginResultEnum Status { get; set; }
        [DataMember]
        public string Message { get; set; }
        [DataMember]
        public int TotalCount { get; set; } // gives the total count based on selection criteria
        
        private Guid _sessionId = Guid.NewGuid();        
        [JsonIgnore]     
        [DataMember]
        public Guid SessionId
        {
            get { return _sessionId; }  
            set { _sessionId = value; }
        }

        [JsonIgnore]
        [DataMember]
        public StatusOptionFlagEnum? Options { get; set; }
        [JsonIgnore]
        [DataMember]
        public string ValidationCode { get; set; }
    }
}