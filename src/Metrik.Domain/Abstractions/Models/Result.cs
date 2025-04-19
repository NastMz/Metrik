using System.Diagnostics.CodeAnalysis;

namespace Metrik.Domain.Abstractions.Models
{
    /// <summary>
    /// Represents the result of an operation, which can either be successful or a failure.
    /// </summary>
    public class Result
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Result"/> class.
        /// </summary>
        /// <param name="isSuccess">Indicates whether the operation was successful.</param>
        /// <param name="error">The error associated with the operation, if any.</param>
        /// <exception cref="InvalidOperationException">If the operation is successful but an error is provided, or if the operation is a failure but no error is provided.</exception>
        protected internal Result(bool isSuccess, Error error)
        {
            if (isSuccess && error != Error.None)
                throw new InvalidOperationException();

            if (!isSuccess && error == Error.None)
                throw new InvalidOperationException();

            IsSuccess = isSuccess;
            Error = error;
        }

        /// <summary>
        /// Indicates whether the operation was successful.
        /// </summary>
        public bool IsSuccess { get; }

        /// <summary>
        /// Indicates whether the operation was a failure.
        /// </summary>
        public bool IsFailure => !IsSuccess;

        /// <summary>
        /// The error associated with the operation, if any.
        /// </summary>
        public Error Error { get; }

        /// <summary>
        /// Creates a successful result.
        /// </summary>
        /// <returns>A successful result.</returns>
        public static Result Success() => new(true, Error.None);

        /// <summary>
        /// Creates a failed result with the specified error.
        /// </summary>
        /// <param name="error">The error associated with the failure.</param>
        /// <returns>A failed result.</returns>
        public static Result Failure(Error error) => new(false, error);

        /// <summary>
        /// Creates a successful result with the specified value.
        /// </summary>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="value">The value associated with the successful result.</param>
        /// <returns>A successful result with the specified value.</returns>
        public static Result<TValue> Success<TValue>(TValue value) => new(value, true, Error.None);

        /// <summary>
        /// Creates a failed result with the specified error.
        /// </summary>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="error">The error associated with the failure.</param>
        /// <returns>A failed result with the specified error.</returns>
        public static Result<TValue> Failure<TValue>(Error error) => new(default, false, error);

        /// <summary>
        /// Creates a successful result with the specified value, or a failure if the value is null.
        /// </summary>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="value">The value associated with the successful result.</param>
        /// <returns>The result, which is either successful with the specified value or a failure if the value is null.</returns>
        public static Result<TValue> Create<TValue>(TValue? value) =>
            value is not null
                ? Success(value)
                : Failure<TValue>(Error.NullValue);
    }

    /// <summary>
    /// Represents the result of an operation that returns a value, which can either be successful or a failure.
    /// </summary>
    /// <typeparam name="TValue">The type of the value.</typeparam>
    public class Result<TValue> : Result
    {
        /// <summary>
        /// The value associated with the successful result (if any).
        /// </summary>
        private readonly TValue? _value;

        /// <summary>
        /// Initializes a new instance of the <see cref="Result{TValue}"/> class.
        /// </summary>
        /// <param name="value">The value associated with the successful result.</param>
        /// <param name="isSuccess">Indicates whether the operation was successful.</param>
        /// <param name="error">The error associated with the operation, if any.</param>
        protected internal Result(TValue? value, bool isSuccess, Error error) : base(isSuccess, error)
        {
            _value = value;
        }

        /// <summary>
        /// Gets the value associated with the successful result.
        /// </summary>
        /// <exception cref="InvalidOperationException">If the operation is a failure.</exception>
        [NotNull]
        public TValue Value =>
            IsSuccess
                ? _value!
                : throw new InvalidOperationException("Cannot access value when result is a failure.");

        /// <summary>
        /// Implicitly converts a value to a Result<TValue>.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        public static implicit operator Result<TValue>(TValue value) => Create(value);
    }
}
