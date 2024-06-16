﻿using Microsoft.JSInterop;

namespace FunTrophy.Web.Models
{
    public class WakeLockSentinel
    {
        public WakeLockSentinel(int id, IJSObjectReference jsObjectReference)
        {
            Id = id;
            JsObjectReference = jsObjectReference;
        }

        public int Id { get; }

        public IJSObjectReference JsObjectReference { get; }
    }
}
