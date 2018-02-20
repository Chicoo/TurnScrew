using System;

namespace TurnScrew.Wiki.SearchEngine
{
    /// <summary>
    /// Represents the relevance of a search result with two states: non-finalized and finalized.
    /// When the state is non-finalized, the value of the relevance has an unknown meaning. When the state
    /// is finalized, the value of the relevance is a percentage value representing the relevance of a search result.
    /// </summary>
    /// <remarks>All members are <b>not thread-safe</b>.</remarks>
    public class Relevance
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Relevance" /> class.
        /// </summary>
        public Relevance() : this(0) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Relevance" /> class.
        /// </summary>
        /// <param name="value">The initial relevance value, non-finalized.</param>
        /// <exception cref="ArgumentOutOfRangeException">If <paramref name="value"/> is less than zero.</exception>
        public Relevance(float value)
        {
            if (value < 0) throw new ArgumentOutOfRangeException("value", "Value must be greater than or equal to zero.");
            Value = value;
            IsFinalized = false;
        }

        /// <summary>
        /// Sets the non-finalized value of the relevance.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <exception cref="ArgumentOutOfRangeException">If <paramref name="value"/> is less than zero.</exception>
        /// <exception cref="InvalidOperationException">If <see cref="M:IsFinalized"/> is <c>true</c> (<see cref="M:Finalize"/> was called).</exception>
        public void SetValue(float value)
        {
            if (value < 0) throw new ArgumentOutOfRangeException("value", "Value must be greater than or equal to zero.");
            if (IsFinalized) throw new InvalidOperationException("After finalization, the value cannot be changed anymore.");
            Value = value;
        }

        /// <summary>
        /// Finalizes the value of the relevance.
        /// </summary>
        /// <param name="total">The global relevance value.</param>
        /// <remarks>The method sets the finalized value of the relevance to <b>value / total * 100</b>.</remarks>
        /// <exception cref="ArgumentOutOfRangeException">If <paramref name="total"/> is less than zero.</exception>
        /// <exception cref="InvalidOperationException">If <see cref="IsFinalized"/> is <c>true</c> (<see cref="M:Finalize"/> was called).</exception>
        public void Finalize(float total)
        {
            if (total < 0) throw new ArgumentOutOfRangeException("total", "Total must be greater than or equal to zero.");
            if (IsFinalized) throw new InvalidOperationException("Finalization already performed.");
            Value = Value / total * 100;
            IsFinalized = true;
        }

        /// <summary>
        /// Normalizes the relevance after finalization.
        /// </summary>
        /// <param name="factor">The normalization factor.</param>
        /// <exception cref="InvalidOperationException">If <see cref="IsFinalized"/> is <c>false</c> (<see cref="M:Finalize"/> was not called).</exception>
        public void NormalizeAfterFinalization(float factor)
        {
            if (factor < 0) throw new ArgumentOutOfRangeException("factor", "Factor must be greater than or equal to zero.");
            if (!IsFinalized) throw new InvalidOperationException("Normalization can be performed only after finalization.");
            Value = Value * factor;
        }

        /// <summary>
        /// Gets a value indicating whether the relevance value is finalized.
        /// </summary>
        public bool IsFinalized { get; protected set; }

        /// <summary>
        /// Gets the value of the relevance.
        /// </summary>
        public float Value { get; protected set; }
    }
}
