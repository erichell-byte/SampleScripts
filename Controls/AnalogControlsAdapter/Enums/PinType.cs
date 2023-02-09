namespace Controls.AnalogControlsAdapter.Enums
{
    // Нельзя менять значения, они используются в конфигурации
    public enum PinType
    {
        Disabled = 0,
        In = 0b01,
        Out = 0b10,
        Adc = 0b11
    }
}
