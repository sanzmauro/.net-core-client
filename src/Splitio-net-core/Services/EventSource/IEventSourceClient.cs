﻿using System;

namespace Splitio.Services.EventSource
{
    public interface IEventSourceClient
    {
        void Connect(string url);
        void Disconnect();
        bool IsConnected();
        
        event EventHandler<EventReceivedEventArgs> EventReceived;
        event EventHandler<FeedbackEventArgs> ConnectedEvent;
        event EventHandler<FeedbackEventArgs> DisconnectEvent;
    }
}