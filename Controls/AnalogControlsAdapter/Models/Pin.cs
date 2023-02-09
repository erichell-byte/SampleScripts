#nullable enable

using System;
using System.ComponentModel;
using Controls.AnalogControlsAdapter.Enums;
using Extensions;
using Unity.VisualScripting;
using UnityEngine;

namespace Controls.AnalogControlsAdapter.Models
{
    public record Pin(string Name, PinType PinType, Enum? ControlType, TriggerValue[]? Triggers = null)
    {
        public Enum? ControlType { get; } = ControlType;

        public string Name { get; } = Name;

        public string? RealName { get; } = ControlType?.GetAttribute<DescriptionAttribute>().Description;

        public PinType PinType { get; } = PinType;

        private TriggerValue[] Triggers { get; } = Triggers ?? Array.Empty<TriggerValue>();

        public float NormalizeValue(int value)
        {
            if (Triggers.Length == 0)
                return value;

            for (var triggerIdx = 0; triggerIdx < Triggers.Length; triggerIdx++)
            {
                if (value > Triggers[triggerIdx].Trigger) continue;

                if (triggerIdx == 0)
                    return Triggers[triggerIdx].Value;

                var position = Mathf.InverseLerp(Triggers[triggerIdx - 1].Trigger, Triggers[triggerIdx].Trigger, value);
                return Mathf.Lerp(Triggers[triggerIdx - 1].Value, Triggers[triggerIdx].Value, position);
            }

            return Triggers[^1].Value;
        }
    }
}
