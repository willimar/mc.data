using FluentValidation;
using mc.core.domain.register.Entity.Person;
using System;
using System.Collections.Generic;
using System.Text;

namespace mc.core.domain.register.Validations
{
    public class PersonValidation: AbstractValidator<Person>
    {
        private static int _minAge;

        public PersonValidation ValidateName()
        {
            this.RuleFor(r => r.Name)
                .NotNull().WithMessage(RequiredFieldMessage("Name"))
                .NotEmpty().WithMessage(RequiredFieldMessage("Name"))
                .Length(5, 150).WithMessage(FieldSizeMessage("Name", 5, 150));
            return this;
        }

        public PersonValidation ValidateBirthDate(int minAge)
        {
            _minAge = minAge;

            this.RuleFor(r => r.BirthDate)
                .NotNull().WithMessage(RequiredFieldMessage("BirthDate"))
                .NotEmpty().WithMessage(RequiredFieldMessage("BirthDate"))
                .Must(HaveMinimumAge).WithMessage(MinAgeMessage(minAge));
            return this;
        }

        public PersonValidation ValidateGender()
        {
            this.RuleFor(r => r.Gender)
                .NotNull().WithMessage(RequiredFieldMessage("Gender"));
            return this;
        }

        protected static bool HaveMinimumAge(DateTime birthDate)
        {
            return birthDate <= DateTime.Now.AddYears(_minAge * -1);
        }

        protected static string RequiredFieldMessage(string fieldName)
        {
            return $"Please ensure you have entered the '{fieldName}'";
        }

        protected static string FieldSizeMessage(string fieldName, int minVal, int maxVal)
        {
            return $"The '{fieldName}' must have between {minVal.ToString()} and {maxVal.ToString()} characters";
        }

        protected static string MinAgeMessage(int age)
        {
            return $"Person must have {age.ToString()} years or more";
        }
    }
}
