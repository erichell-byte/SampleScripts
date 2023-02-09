namespace Controls.AnalogControlsAdapter.Models
{
    public record TriggerValue(float Trigger, float Value)
    {
        public float Value {get;} = Value;
        public float Trigger {get;} = Trigger;
    }
}
