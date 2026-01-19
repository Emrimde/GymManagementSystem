using Microsoft.AspNetCore.Mvc;

namespace GymManagementSystem.WPF.Result;
public sealed class Result<T>
{
    public bool IsSuccess { get; }
    public T? Value { get; }
    public ProblemDetails? Problem { get; }
    public ValidationProblemDetails? ValidationProblem { get; }

    private Result(T value)
    {
        IsSuccess = true;
        Value = value;
    }

    private Result(ProblemDetails problem, ValidationProblemDetails? validation = null)
    {
        IsSuccess = false;
        Problem = problem;
        ValidationProblem = validation;
    }

    public static Result<T> Success(T value) => new(value);
    public static Result<T> Failure(ProblemDetails problem) => new(problem);
    public static Result<T> ValidationFailure(ValidationProblemDetails validation)
        => new(validation, validation);

    public string GetUserMessage()
    {
        if (ValidationProblem != null)
            return string.Join(
                "\n",
                ValidationProblem.Errors.SelectMany(item => item.Value));

        return Problem?.Detail
               ?? Problem?.Title
               ?? "Unexpected error occur";
    }
}



