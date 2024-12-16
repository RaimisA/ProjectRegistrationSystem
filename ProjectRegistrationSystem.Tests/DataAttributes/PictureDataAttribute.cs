using AutoFixture;
using AutoFixture.Xunit2;
using ProjectRegistrationSystem.Tests.SpecimenBuilders;

public class PictureDataAttribute : AutoDataAttribute
{
    public PictureDataAttribute() : base(() =>
    {
        var fixture = new Fixture();
        fixture.Customizations.Add(new PictureSpecimenBuilder());
        return fixture;
    })
    {
    }
}