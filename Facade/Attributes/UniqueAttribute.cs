using Abc.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace Abc.Facade.Attributes {
    public sealed class UniqueAttribute :ValidationAttribute {
        internal readonly dynamic repo;
        internal PropertyInfo pi;
        internal string id;
        internal readonly Type type;
        internal dynamic existingObj;
        internal dynamic currentObj;
        public UniqueAttribute(Type repoInterface) {
            repo = GetRepo.Instance(repoInterface);
            type = repo?.Get(string.Empty)?.GetType();
        }
        protected override ValidationResult IsValid(object value, ValidationContext c) {
            initValues(value, c);
            if (isEditing()) return ValidationResult.Success;
            if (!isCreating()) return validationResult();
            existingObj = getByProperty();
            return validationResult();
        }
        internal bool isEditing() {
            existingObj = repo?.Get(id);
            return existingObj.Id == currentObj?.Id;
        }
        internal bool isCreating() {
            if (existingObj is null) return true;
            return existingObj.IsUnspecified;
        }
        internal void initValues(object value, ValidationContext c) {
            id = value?.ToString();
            pi = type?.GetProperty(c?.MemberName ?? string.Empty);
            currentObj = c.ObjectInstance;
        }
        internal dynamic getByProperty() =>
            (repo?.Get() as IEnumerable<dynamic>)?.FirstOrDefault(x => pi?.GetValue(x)?.ToString() == id);
        internal ValidationResult validationResult() => isCreating()
            ? ValidationResult.Success
            : new ValidationResult($"The <{pi?.Name}> == <{id}> is already registered");
        internal ValidationResult isValid(object value, ValidationContext c) => IsValid(value, c);
    }
}
