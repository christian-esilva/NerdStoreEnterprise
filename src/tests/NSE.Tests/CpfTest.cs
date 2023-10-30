using NSE.Core.DomainObjects;

namespace NSE.Tests
{
    [TestClass]
    public class CpfTest
    {
        public Cpf Cpf { get; set; }

        [TestMethod]
        [ExpectedException(typeof(DomainException))]
        public void CpfMustBeInvalid()
        {
            string validCPF = "12345678901";
            Cpf = new Cpf(validCPF);

            Cpf.Validate(validCPF);
        }

        [TestMethod]
        public void CpfMustBeValid()
        {
            string validCPF = "25829092034";
            Cpf = new Cpf(validCPF);

            Assert.IsTrue(Cpf.Validate(validCPF));
        }
    }
}