#nullable enable

using System;

namespace Controls.AnalogControlsAdapter.Models
{
    public record Arrow(string Name, Enum? ControlType)
    {
        public Enum? ControlType { get; } = ControlType;
        public string Name { get; } = Name;
    }
}
