using AutoFixture;
using AutoFixture.Xunit2;
using ProjectRegistrationSystem.Tests.SpecimenBuilders;

public class PersonDataAttribute : AutoDataAttribute
{
    public PersonDataAttribute() : base(() =>
    {
        var fixture = new Fixture();
        fixture.Customizations.Add(new PersonSpecimenBuilder());

        // Handle circular references
        fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
            .ForEach(b => fixture.Behaviors.Remove(b));
        fixture.Behaviors.Add(new OmitOnRecursionBehavior());

        return fixture;
    })
    {
    }
}