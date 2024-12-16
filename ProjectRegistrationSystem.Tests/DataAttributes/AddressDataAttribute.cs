using AutoFixture;
using AutoFixture.Xunit2;
using ProjectRegistrationSystem.Tests.SpecimenBuilders;

public class AddressDataAttribute : AutoDataAttribute
{
    public AddressDataAttribute() : base(() =>
    {
        var fixture = new Fixture();
        fixture.Customizations.Add(new AddressSpecimenBuilder());
        return fixture;
    })
    {
    }
}