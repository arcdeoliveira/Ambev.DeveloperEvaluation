using Ambev.DeveloperEvaluation.Common.Validation;

namespace Ambev.DeveloperEvaluation.Domain.Common;

public class BaseRelationalDatabaseEntity<T> 
{
    public required T Id { get; set; }

    /// <summary>
    /// Gets the date and time when the user was created.
    /// </summary>
    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;

    /// <summary>
    /// Gets the date and time of the last update to the user's information.
    /// </summary>
    public DateTime? UpdatedAt { get; private set; }


    public void AlterDateUpdate()
    {
        UpdatedAt = DateTime.UtcNow;
    }

    public Task<IEnumerable<ValidationErrorDetail>> ValidateAsync()
    {
        return Validator.ValidateAsync(this);
    }
}
