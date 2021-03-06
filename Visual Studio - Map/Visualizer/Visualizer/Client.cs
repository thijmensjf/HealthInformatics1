﻿// <copyright file="Client.cs" company="HI1 aka Geen naam">
//     Copyright (c) HI1 aka Geen naam. All rights reserved.
// </copyright>

namespace Visualizer
{
    using System;
    using System.Net;
    using System.Net.Sockets;
    using System.Text;
    using Visualizer;

    /// <summary>
    /// The client class is the receiver part of the connection.
    /// The client receives packets from the unity environment and sends it to the handler.
    /// </summary>
    /// <autogeneratedoc />
    public class Client
    {
        /// <summary>
        /// The UDP
        /// </summary>
        /// <autogeneratedoc />
        private UdpClient udp;

        /// <summary>
        /// The UDPEP
        /// </summary>
        /// <autogeneratedoc />
        private IPEndPoint udpep;

        /// <summary>
        /// The logger
        /// </summary>
        /// <autogeneratedoc />
        private Logger logger = Logger.GetInstance();

        /// <summary>
        /// The port
        /// </summary>
        /// <autogeneratedoc />
        private int port;

        /// <summary>
        /// Initializes a new instance of the <see cref="Client" /> class.
        /// </summary>
        /// <param name="p">The p.</param>
        /// <autogeneratedoc />
        public Client(int p)
        {
            this.port = p;

            try
            {
                while (this.udp == null)
                {
                    this.udp = new UdpClient(this.port);
                }

                this.logger.Info("UDP Client Initialized");
                while (true)
                {
                    this.udp.BeginReceive(new AsyncCallback(this.UDP_IncomingData), this.udpep);
                }
            }
            catch (SocketException e)
            {
                Console.WriteLine(e.StackTrace.ToString());
                this.logger.Error("Problem with incoming packets");
            }

            this.logger.Info("UDP Client Closed");
        }

        /// <summary>
        /// Waits for incoming data and sends it to the handler.
        /// </summary>
        /// <param name="ar">The arguments for receiving data.</param>
        /// <autogeneratedoc />
        private void UDP_IncomingData(IAsyncResult ar)
        {
            this.udpep = new IPEndPoint(IPAddress.Any, this.port);

            Console.Write("Waiting for incoming data...");
            try
            {
                byte[] byteReceived = this.udp.EndReceive(ar, ref this.udpep);
                string stringReceived = Encoding.ASCII.GetString(byteReceived);
                Console.WriteLine("Packet#: Received String: " + stringReceived);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace.ToString());
                this.logger.Info("Problem receiving data");
            }
        }
    }
}