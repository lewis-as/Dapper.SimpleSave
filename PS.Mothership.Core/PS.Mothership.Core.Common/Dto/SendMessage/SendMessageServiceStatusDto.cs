﻿using System;
using System.Runtime.Serialization;
using PS.Mothership.Core.Common.Template.Comm;

namespace PS.Mothership.Core.Common.Dto.SendMessage
{
	[DataContract]
	public class SendMessageServiceStatusDto
	{
		[DataMember]
		public CommMessageServiceStatusEnum MessageStatus { get; set; }
		[DataMember]
		public CommMessageTypeEnum MessageType { get; set; }
		[DataMember]
		public DateTimeOffset LastUpdateTime { get; set; }
	}
}
