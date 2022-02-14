namespace SES.Projects.Sanitarium.Chameleon.FactoryPlanning.Validation
{
    using System.Threading.Tasks;
    using Catel.Data;
    using global::Chameleon.Validation;
    using Orc.ProjectManagement;

    public class FactoryPlanningValidator : ValidatorBase
    {
        protected override async Task<IValidationContext> ValidateAsync(IProject project)
        {
            var validationContext = new ValidationContext();

            // TODO: implement business rule validations

            return validationContext;
        }
    }
}