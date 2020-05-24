﻿using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Packit.Exceptions
{
    public class NetworkConnectionException : Exception, ISerializable
    {
        public NetworkConnectionException()
        {
        }

        public NetworkConnectionException(string message)
            : base(message)
        {
        }

        public NetworkConnectionException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        protected NetworkConnectionException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
