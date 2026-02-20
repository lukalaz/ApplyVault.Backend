namespace ApplyVault.Application.Common;

public sealed class OperationResult<T>
{
    public OperationStatus Status { get; }
    public T? Data { get; }
    public string? ErrorMessage { get; }
    public IReadOnlyDictionary<string, string[]>? ValidationErrors { get; }

    private OperationResult(
        OperationStatus status,
        T? data = default,
        string? errorMessage = null,
        IReadOnlyDictionary<string, string[]>? validationErrors = null)
    {
        Status = status;
        Data = data;
        ErrorMessage = errorMessage;
        ValidationErrors = validationErrors;
    }

    public static OperationResult<T> Ok(T data) =>
        new(OperationStatus.Success, data);

    public static OperationResult<T> NotFound(string? message = null) =>
        new(OperationStatus.NotFound, default, message ?? "Not found.");

    public static OperationResult<T> Validation(string field, string message) =>
        new(OperationStatus.ValidationError, default, "Validation error.",
            new Dictionary<string, string[]> { [field] = [message] });

    public static OperationResult<T> Validation(IReadOnlyDictionary<string, string[]> errors) =>
        new(OperationStatus.ValidationError, default, "Validation error.", errors);

    public static OperationResult<T> Error(string message) =>
        new(OperationStatus.Error, default, message);
}
