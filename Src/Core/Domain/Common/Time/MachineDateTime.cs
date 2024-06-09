namespace Domain.Common.Time;

public static class MachineDateTime
{
    public static DateTime Now => DateTime.UtcNow;
    public static int CurrentYear => Now.Year;
}