using System;

namespace Task3_АТS.ATS.NetworkEntity
{
    public abstract class NetworkEntityBase : INetworkEntity
    {
        public string MACAddress { get; }


        protected NetworkEntityBase(string macAddress)
        {
            if (string.IsNullOrWhiteSpace(macAddress))
                throw new ArgumentException("Empty MACAddress");


            MACAddress = macAddress;
        }

        public override string ToString()
        {
            return MACAddress;
        }
    }
}
