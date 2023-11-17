﻿using System;
using System.Net;
using System.Net.Sockets;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace WOLtool
{
    public class WakeOnLan : IDisposable
    {
        private readonly Socket _sock;
        private static readonly IReadOnlyList<IPEndPoint> _endpoints = new List<IPEndPoint>
        {
            new IPEndPoint(IPAddress.Broadcast, 7), // echo
            new IPEndPoint(IPAddress.Broadcast, 9) // discard
        };

        public WakeOnLan()
        {
            _sock = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp)
            {
                EnableBroadcast = true // Enable broadcast, required for macOS compatibility
            };
        }

        public void Send(string macAddress)
        {
            try
            {
                var macParse = PhysicalAddress.Parse(macAddress); // Parse string value
                byte[] magicPacket = BuildMagicPacket(macParse); // Get magic packet byte array based on MAC Address
                foreach (var ep in _endpoints) // Broadcast to *all* WOL Endpoints
                {
                    _sock.SendTo(magicPacket, ep); // Broadcast magic packet
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("ERROR broadcasting WakeOnLan Magic Packet.", ex);
            }
        }
        public void Send(PhysicalAddress macAddress)
        {
            try
            {
                byte[] magicPacket = BuildMagicPacket(macAddress); // Get magic packet byte array based on MAC Address
                foreach (var ep in _endpoints) // Broadcast to *all* WOL Endpoints
                {
                    _sock.SendTo(magicPacket, ep); // Broadcast magic packet
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("ERROR broadcasting WakeOnLan Magic Packet.", ex);
            }
        }

        public async Task SendAsync(string macAddress)
        {
            try
            {
                var macParse = PhysicalAddress.Parse(macAddress); // Parse string value
                byte[] magicPacket = BuildMagicPacket(macParse); // Get magic packet byte array based on MAC Address
                foreach (var ep in _endpoints) // Broadcast to *all* WOL Endpoints
                {
                    await _sock.SendToAsync(magicPacket, SocketFlags.None, ep); // Broadcast magic packet
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("ERROR broadcasting WakeOnLan Magic Packet.", ex);
            }
        }
        public async Task SendAsync(PhysicalAddress macAddress)
        {
            try
            {
                byte[] magicPacket = BuildMagicPacket(macAddress); // Get magic packet byte array based on MAC Address
                foreach (var ep in _endpoints) // Broadcast to *all* WOL Endpoints
                {
                    await _sock.SendToAsync(magicPacket, SocketFlags.None, ep); // Broadcast magic packet
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("ERROR broadcasting WakeOnLan Magic Packet.", ex);
            }
        }

        private static byte[] BuildMagicPacket(PhysicalAddress macAddress)
        {
            byte[] macBytes = macAddress.GetAddressBytes(); // Convert 48 bit MAC Address to array of bytes
            byte[] magicPacket = new byte[102];
            for (int i = 0; i < 6; i++) // 6 times 0xFF
            {
                magicPacket[i] = 0xFF;
            }
            for (int i = 6; i < 102; i += 6) // 16 times MAC Address
            {
                Buffer.BlockCopy(macBytes, 0, magicPacket, i, 6);
            }
            return magicPacket; // 102 Byte Magic Packet
        }

        #region IDisposable
        private bool _disposed = false;
        public void Dispose() => Dispose(true);

        // Protected implementation of Dispose pattern.
        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
                return;
            if (disposing)
            {
                _sock?.Dispose();
            }
            _disposed = true;
        }
        #endregion
    }
}
