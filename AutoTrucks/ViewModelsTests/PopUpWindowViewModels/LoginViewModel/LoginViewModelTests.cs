using Microsoft.VisualStudio.TestTools.UnitTesting;
using ViewModels.PopUpWindowViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics.CodeAnalysis;
using Service.ConnexionService;
using Moq;
using System.Windows.Input;
using Model.SendData;

namespace ViewModels.PopUpWindowViewModels.Tests
{
    [ExcludeFromCodeCoverage]
    [TestClass()]
    public class LoginViewModelTests
    {
        Mock<IConnectConnexionService> connectConnexionService;
        LoginViewModel loginViewModel;

        [TestInitialize]
        public void SetInitialValues()
        {
            connectConnexionService = new Mock<IConnectConnexionService>();
            loginViewModel = new LoginViewModel(connectConnexionService.Object);
        }

        [TestMethod()]
        public void LoginShortPasswordTest()
        {
            ICommand ic = loginViewModel.LoginCommand;
            loginViewModel.Username = "Petter";
            loginViewModel.Password = "a";
            ic.Execute(this);
            Assert.AreEqual("Username lenght (4-16) Password lenght (4-30)", loginViewModel.Message);
        }

        [TestMethod()]
        public void LoginTooLongPasswordTest()
        {
            ICommand ic = loginViewModel.LoginCommand;
            loginViewModel.Username = "Petter";
            loginViewModel.Password = "FivelFivelFivelFivelFivelFivelFivel";
            ic.Execute(this);
            Assert.AreEqual("Username lenght (4-16) Password lenght (4-30)", loginViewModel.Message);
        }

        [TestMethod()]
        public void LoginTooLongUsernameTest()
        {
            ICommand ic = loginViewModel.LoginCommand;
            loginViewModel.Username = "FourFourFourFourFour";
            loginViewModel.Password = "Password";
            ic.Execute(this);
            Assert.AreEqual("Username lenght (4-16) Password lenght (4-30)", loginViewModel.Message);
        }

        [TestMethod()]
        public void UsernameGetTest()
        {
            string value = "User";
            loginViewModel.Username = value;
            Assert.AreEqual(value, loginViewModel.Username);
        }

        [TestMethod()]
        public void PasswordGetTest()
        {
            string value = "Pass";
            loginViewModel.Password = value;
            Assert.AreEqual(value, loginViewModel.Password);
        }

        [TestMethod()]
        public void LoginTooShortUsernameTest()
        {
            ICommand ic = loginViewModel.LoginCommand;
            loginViewModel.Username = "Fou";
            loginViewModel.Password = "Password";
            ic.Execute(this);
            Assert.AreEqual("Username lenght (4-16) Password lenght (4-30)", loginViewModel.Message);
        }

        [TestMethod()]
        public void LoginWithNullUsernameTest()
        {
            ICommand ic = loginViewModel.LoginCommand;
            loginViewModel.Password = "Password";
            loginViewModel.Username = null;
            ic.Execute(this);
            Assert.AreEqual("Username lenght (4-16) Password lenght (4-30)", loginViewModel.Message);
        }

        [TestMethod()]
        public void LoginWithNullPasswordTest()
        {
            ICommand ic = loginViewModel.LoginCommand;
            loginViewModel.Password = null;
            loginViewModel.Username = "User";
            ic.Execute(this);
            Assert.AreEqual("Username lenght (4-16) Password lenght (4-30)", loginViewModel.Message);
        }

        [TestMethod()]
        public void IncorrectLoginTest()
        {
            ICommand ic = loginViewModel.LoginCommand;
            loginViewModel.Username = "Petter";
            loginViewModel.Password = "Password";
            connectConnexionService.Setup(x => x.CheckIfValidLoginToConnexion("Petter", "Password")).Returns(false);
            ic.Execute(this);
            Assert.AreEqual("Failed to log in", loginViewModel.Message);
        }

        [TestMethod()]
        public void SuccessLoginTest()
        {
            ICommand ic = loginViewModel.LoginCommand;
            loginViewModel.Username = "Petter";
            loginViewModel.Password = "Password";
            connectConnexionService.Setup(x => x.CheckIfValidLoginToConnexion("Petter", "Password")).Returns(true);
            ic.Execute(this);
            Assert.AreEqual("Log in succesful", loginViewModel.Message);
            Assert.AreEqual("Petter", loginViewModel.loginCredentials.loginId);
            Assert.AreEqual("Password", loginViewModel.loginCredentials.password);
            Assert.AreEqual(true, loginViewModel.loginCompleted);
        }

    }
}