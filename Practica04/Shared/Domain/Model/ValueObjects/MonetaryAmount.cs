namespace Practica04.Shared.Domain.Model.ValueObjects;

/// <summary>Represents a nonnegative monetary value and a nonblank currency.</summary>
public sealed record MonetaryAmount
{
    /// <summary>Gets the nonnegative monetary value.</summary>
    public decimal Value { get; }

    /// <summary>Gets the currency associated with the value.</summary>
    public string Currency { get; }

    /// <summary>Creates a monetary amount with a nonnegative value and a currency.</summary>
    /// <param name="value">The monetary value, greater than or equal to zero.</param>
    /// <param name="currency">The nonblank currency text.</param>
    /// <exception cref="ArgumentOutOfRangeException">The value is negative.</exception>
    /// <exception cref="ArgumentException">The currency is null, empty or whitespace.</exception>
    public MonetaryAmount(decimal value, string currency)
    {
        if (value < decimal.Zero)
            throw new ArgumentOutOfRangeException(nameof(value), value, "Value must be greater than or equal to zero.");
        if (string.IsNullOrWhiteSpace(currency))
            throw new ArgumentNullException(nameof(currency), "Currency must not be null, empty or whitespace.");

        Value = value;
        Currency = currency;
    }

    /// <summary>Creates a new amount multiplied by a factor, preserving the currency.</summary>
    /// <param name="factor">The factor applied to the monetary value.</param>
    /// <returns>A new valid monetary amount in the same currency.</returns>
    /// <exception cref="ArgumentOutOfRangeException">The resulting value is negative.</exception>
    /// <exception cref="OverflowException">The multiplication exceeds the decimal range.</exception>
    public MonetaryAmount Multiply(decimal factor) => new(Value * factor, Currency);
    /// <summary>Returns a new amount with the priority surcharge applied.</summary>
    /// <param name="factor">The multiplier for night transport or advanced life support.</param>
    /// <returns>A new monetary amount in the same currency.</returns>
    public MonetaryAmount ApplyPrioritySurcharge(decimal factor) => Multiply(factor);
}




