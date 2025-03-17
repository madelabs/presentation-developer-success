namespace BBQ.Application.ValueObjects
{
    public record Money(decimal Amount)
    {

        // Unary negation operator
        public static Money operator -(Money money) => new Money(-money.Amount);

        // Addition operator
        public static Money operator +(Money a, Money b) => new Money(a.Amount + b.Amount);

        // Subtraction operator
        public static Money operator -(Money a, Money b) => new Money(a.Amount - b.Amount);

        // Multiplication operator
        public static Money operator *(Money a, decimal multiplier) => new Money(a.Amount * multiplier);

        // Division operator
        public static Money operator /(Money a, decimal divisor) => new Money(a.Amount / divisor);


        // Comparison operators
        public static bool operator <(Money a, Money b) => a.Amount < b.Amount;
        public static bool operator >(Money a, Money b) => a.Amount > b.Amount;
        public static bool operator <=(Money a, Money b) => a.Amount <= b.Amount;
        public static bool operator >=(Money a, Money b) => a.Amount >= b.Amount;

        // Implicit conversion operators
        public static implicit operator Money(decimal amount) => new Money(amount);
        public static implicit operator decimal(Money money) => money?.Amount ?? 0;

        public override string ToString()
        {
            return $"{Amount:C}";
        }

        public string ToObfuscatedString()
        {
            return $"XXX.XX";
        }
    }

}
