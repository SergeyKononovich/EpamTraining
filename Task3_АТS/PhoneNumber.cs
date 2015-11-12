using System;

namespace Task3_АТS
{
    public struct PhoneNumber : IEquatable<PhoneNumber>
    {
        private readonly string _phoneNumber;
        public string Value => _phoneNumber;


        public PhoneNumber(string phoneNumber)
        {
            _phoneNumber = phoneNumber;
        }

        public override string ToString()
        {
            return Value;
        }
        public override bool Equals(object obj)
        {
            if (obj is PhoneNumber)
            {
                return _phoneNumber == ((PhoneNumber)obj)._phoneNumber;
            }
            else
            {
                return false;
            }
        }
        public override int GetHashCode()
        {
            return _phoneNumber.GetHashCode();
        }
        public bool Equals(PhoneNumber other)
        {
            return _phoneNumber == other._phoneNumber;
        }

        public static bool operator ==(PhoneNumber p1, PhoneNumber p2)
        {
            return p1.Equals(p2);
        }
        public static bool operator !=(PhoneNumber p1, PhoneNumber p2)
        {
            return !(p1 == p2);
        }
    }
}
