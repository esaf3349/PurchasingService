namespace Domain.Model.Permissions.ValueObjects;

public enum AllowedEntity : int
{
    Requisition = 1,
    RequisitionLine = 2,
    User = 3,
    Supplier = 4,
    Departnment = 5,
    Good = 6,
    Measure = 7,
    Currency = 8
}