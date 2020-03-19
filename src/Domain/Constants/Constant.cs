using System;
using System.Collections.Generic;

namespace Domain.Constants
{
    public class Constant<T> : IEquatable<Constant<T>>
    {
        public T Value { get; }

        protected Constant(T value)
        {
            Value = value;
        }

        public override string ToString()
        {
            return Value.ToString();
        }

        public bool Equals(Constant<T> other)
        {
            return other.Value.ToString() == Value.ToString();
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return -1937169414 + EqualityComparer<T>.Default.GetHashCode(Value);
        }

        public static implicit operator T(Constant<T> a)
        {
            return a.Value;
        }

        public static implicit operator Constant<T>(T a)
        {
            return new Constant<T>(a);
        }

        public static bool operator !=(Constant<T> a, Constant<T> b)
        {
            return a.ToString() != b.ToString();
        }

        public static bool operator ==(Constant<T> a, Constant<T> b)
        {
            return a.ToString() == b.ToString();
        }
    }
}