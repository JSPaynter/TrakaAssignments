using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrelloAPI.Tests
{
    public class SetupTeardown
    {
        internal Steps steps;

        [SetUp]
        public void BaseSetUp()
        {
            steps = new Steps();
        }
    }
}
