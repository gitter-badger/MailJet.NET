﻿using MailJet.Client.Response.Data;
using System.Collections.Generic;

namespace MailJet.Client.Response
{
    public class SendResponse : BaseResponse
    {
        public List<DataItem> Data { get; set; }
    }
}