using Domain.Common.Entities;
using Domain.Common.Exceptions;
using Domain.Common.Time;
using Domain.Model.Departments;
using Domain.Model.RequisitionLines;
using Domain.Model.RequisitionLines.ValueObjects;
using Domain.Model.Requisitions.Constants;
using Domain.Model.Requisitions.ValueObjects;
using Domain.Model.Suppliers;
using Domain.Model.Users;

namespace Domain.Model.Requisitions;

public sealed class Requisition : BaseEntity<Guid>
{
    public int Number { get; private set; }
    public string Title { get; private set; }
    public Status Status { get; private set; }
    public Guid SupplierId { get; private set; }
    public Supplier? Supplier { get; private set; }
    public Guid DepartmentId { get; private set; }
    public Department? Department { get; private set; }
    public Guid RequesterId { get; private set; }
    public User? Requester { get; private set; }
    public DateTime DueDate { get; private set; }

    public ICollection<RequisitionLine> Lines { get; private set; }

    private Requisition() { }

    public Requisition(Guid id, string title, Guid supplierId, Guid departmentId, DateTime deliveryDueDate) : base(id)
    {
        Status = Status.Composing;
        RequesterId = Guid.NewGuid();

        SetTitle(title);
        SetSupplier(supplierId);
        SetDepartment(departmentId);
        SetDueDate(deliveryDueDate);

        Lines = [];
    }

    public void SetTitle(string title)
    {
        if (Status != Status.Composing)
            throw new DomainException<Requisition>($"Could not set {nameof(Title)}: {nameof(Status)} is not {Status.Composing}");

        if (title.Length > TitleConstants.MaxLength)
            throw new DomainException<Requisition>($"{nameof(Title)} should not be longer than {TitleConstants.MaxLength} symbols");

        if (title.Length < TitleConstants.MinLength)
            throw new DomainException<Requisition>($"{nameof(Title)} should be at least {TitleConstants.MinLength} symbols");

        Title = title;
    }

    public void SetSupplier(Guid supplierId)
    {
        if (Status != Status.Composing)
            throw new DomainException<Requisition>($"Could not set {nameof(SupplierId)}: {nameof(Status)} is not {Status.Composing}");

        SupplierId = supplierId;
    }

    public void SetDepartment(Guid departmentId)
    {
        if (Status != Status.Composing)
            throw new DomainException<Requisition>($"Could not set {nameof(DepartmentId)}: {nameof(Status)} is not {Status.Composing}");

        DepartmentId = departmentId;
    }

    public void SetDueDate(DateTime dueDate)
    {
        if (Status != Status.Composing)
            throw new DomainException<Requisition>($"Could not set {nameof(DueDate)}: {nameof(Status)} is not {Status.Composing}");

        if (dueDate <= MachineDateTime.Now)
            throw new DomainException<Requisition>($"{nameof(DueDate)} should be in the future");

        DueDate = dueDate;
    }

    public void AddLine(RequisitionLine line)
    {
        if (Status != Status.Composing)
            throw new DomainException<Requisition>($"Could not add {nameof(RequisitionLine)}: {nameof(Status)} is not {Status.Composing}");

        var maxOrdinalNumber = Lines.Where(l => l.IsActive).Max(l => l.OrdinalNumber);
        if (line.OrdinalNumber + 1 != maxOrdinalNumber)
            throw new DomainException<Requisition>($"Could not add {nameof(RequisitionLine)}: expected {nameof(line.OrdinalNumber)} is {maxOrdinalNumber + 1}");

        Lines.Add(line);
    }

    public void SetLinePrice(Guid lineId, decimal quantity, Price price)
    {
        if (Status != Status.Composing)
            throw new DomainException<Requisition>($"Could not update {nameof(RequisitionLine)}: {nameof(Status)} is not {Status.Composing}");

        var line = Lines.FirstOrDefault(l => l.Id == lineId && l.IsActive);
        if (line == null)
            throw new DomainException<Requisition>($"{nameof(RequisitionLine)} does not exist");

        line.SetPrice(quantity, price);
    }

    public void DeleteLine(Guid lineId)
    {
        if (Status != Status.Composing)
            throw new DomainException<Requisition>($"Could not delete {nameof(RequisitionLine)}: {nameof(Status)} is not {Status.Composing}");

        var line = Lines.FirstOrDefault(l => l.Id == lineId && l.IsActive);
        if (line == null)
            throw new DomainException<Requisition>($"{nameof(RequisitionLine)} does not exist");

        line.Delete();
    }
}