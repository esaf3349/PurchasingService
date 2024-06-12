namespace Domain.Model.Requisitions.ValueObjects;

public enum Status : int
{
    Composing = 1,
    Review = 2,
    Approved = 3,
    Received = 4
}