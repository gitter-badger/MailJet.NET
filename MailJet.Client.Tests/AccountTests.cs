﻿using NUnit.Framework;
using System;
using System.Linq;

namespace MailJet.Client.Tests
{
    [TestFixture]
    public class AccountTests
    {
        private MailJetClient _client;

        [SetUp]
        public void Setup()
        {
#if DEBUG
            var publicKey = Environment.GetEnvironmentVariable("MailJetPub", EnvironmentVariableTarget.User);
            var privateKey = Environment.GetEnvironmentVariable("MailJetPri", EnvironmentVariableTarget.User);
#else
			var publicKey = Environment.GetEnvironmentVariable("MailJetPub");
			var privateKey = Environment.GetEnvironmentVariable("MailJetPri");
#endif


            if (String.IsNullOrWhiteSpace(publicKey))
                throw new InvalidOperationException("Add your MailJet public API Key to the Environment Variable \"MailJetPub\".");
            if (String.IsNullOrWhiteSpace(privateKey))
                throw new InvalidOperationException("Add your MailJet private API Key to the Environment Variable \"MailJetPri\".");
			
            _client = new MailJetClient(publicKey, privateKey);
        }

        [Test]
        public void GetAllDNS()
        {
            var result = _client.GetDNS();
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Data.Any());
            Assert.AreEqual(result.Count, result.Data.Count);
        }

        [Test]
        public void GetSingleDNS_ById()
        {
            var data = _client.GetDNS().Data.First();
            var result = _client.GetDNS(data.ID);
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Data.Any());
            Assert.AreEqual(result.Count, 1);
            Assert.AreEqual(result.Data.Count, 1);
        }

        [Test]
        public void GetSingleDNS_ByDomain()
        {
            var data = _client.GetDNS().Data.First();
            var result = _client.GetDNS(data.Domain);
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Data.Any());
            Assert.AreEqual(data.Domain, result.Data[0].Domain);
            Assert.AreEqual(result.Count, 1);
            Assert.AreEqual(result.Data.Count, 1);
        }

        [Test]
        public void ForceDNSRecheck()
        {
            var data = _client.GetDNS().Data.First();
            var result = _client.ForceDNSRecheck(data.ID);
            Assert.IsNotNull(result);
            Assert.AreEqual(result.Count, 1);
            Assert.AreEqual(result.Data.Count, 1);
        }
    }
}
