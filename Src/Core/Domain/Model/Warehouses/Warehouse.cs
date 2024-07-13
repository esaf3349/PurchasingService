using Domain.Common.Entities;
using Domain.Common.Exceptions;
using Domain.Model.RequisitionLines;
using Domain.Model.Warehouses.Constants;

namespace Domain.Model.Warehouses;

public sealed class Warehouse : BaseEntity<Guid>
{
    public string Name { get; private set; }
    public string Address { get; private set; }

    public ICollection<RequisitionLine> RequisitionLines { get; private set; }

    private Warehouse() { }

    public Warehouse(Guid id, string name, string address) : base(id)
    {
        SetName(name);
        SetAddress(address);
    }

    public void SetName(string name)
    {
        if (name.Length > NameConstants.MaxLength)
            throw new DomainException<Warehouse>($"{nameof(Name)} should not be longer than {NameConstants.MaxLength} symbols");

        if (name.Length < NameConstants.MinLength)
            throw new DomainException<Warehouse>($"{nameof(Name)} should be at least {NameConstants.MinLength} symbols");

        Name = name;
    }

    public void SetAddress(string address)
    {
        if (address.Length > AddressConstants.MaxLength)
            throw new DomainException<Warehouse>($"{nameof(Address)} should not be longer than {AddressConstants.MaxLength} symbols");

        if (address.Length < AddressConstants.MinLength)
            throw new DomainException<Warehouse>($"{nameof(Address)} should be at least {AddressConstants.MinLength} symbols");

        Address = address;
    }
}