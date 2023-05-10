﻿using EvernoteClone.Model;
using EvernoteClone.ViewModel.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents.DocumentStructures;

namespace EvernoteClone.ViewModel
{
    public class LoginVM : INotifyPropertyChanged
    {
		private bool isShowingRegister = false;
		
		private User user;
		public User User
		{
			get { return user; }
			set 
			{
				user = value;
                OnPropertyChanged("User");
            }
		}

		private Visibility loginVis;

        public event PropertyChangedEventHandler PropertyChanged;

        public Visibility LoginVis
        {
			get { return loginVis; }
			set 
			{ 
				loginVis = value;
				OnPropertyChanged("LoginVis");
			}
		}

		private string username;
		public string Username
		{
			get { return username; }
			set 
			{ 
				username = value;
				User = new User
				{
					Username = username,
					Password = this.Password,
					Name = this.Name,
					Lastname = this.Lastname,
                    ConfirmPassword = this.ConfirmPassword
                };
                OnPropertyChanged("Username");
            }
		}

        private string password;
        public string Password
        {
            get { return password; }
            set
            {
                password = value;
                User = new User
                {
                    Username = this.Username,
                    Password = password,
					Name = this.Name,
					Lastname = this.Lastname,
                    ConfirmPassword = this.ConfirmPassword
                };
                OnPropertyChanged("Password");
            }
        }

        private string name;
        public string Name
        {
            get { return name; }
            set
            {
                name = value;
				User = new User
				{
					Username = this.Username,
					Password = this.Password,
					Name = name,
					Lastname = this.lastname,
                    ConfirmPassword = this.ConfirmPassword
                };

                OnPropertyChanged("Name");
            }
        }

        private string lastname;
        public string Lastname
        {
            get { return lastname; }
            set
            {
                lastname = value;
				User = new User
				{
					Username = this.Username,
					Password = this.Password,
					Name = this.Name,
					Lastname = lastname,
					ConfirmPassword = this.ConfirmPassword
                };

                OnPropertyChanged("Lastname");
            }
        }

        private string confirmPassword;
        public string ConfirmPassword
        {
            get { return confirmPassword; }
            set
            {
                confirmPassword = value;
                User = new User
                {
                    Username = this.Username,
                    Password = this.Password,
                    Name = this.Name,
                    Lastname = this.Lastname,
					ConfirmPassword = confirmPassword
                };

                OnPropertyChanged("ConfirmPassword");
            }
        }

        private Visibility registerVis;

		public Visibility RegisterVis
        {
			get { return registerVis; }
			set
			{
				registerVis = value;
                OnPropertyChanged("RegisterVis");
            }
		}


		public RegisterCommand RegisterCommand { get; set; }
		public LoginCommand LoginCommand { get; set; }
		public ShowRegisterCommand ShowRegisterCommand { get; set; }

		public LoginVM()
		{
			loginVis = Visibility.Visible;
			registerVis = Visibility.Collapsed;
			
			RegisterCommand = new RegisterCommand(this);
			LoginCommand = new LoginCommand(this);
			ShowRegisterCommand = new ShowRegisterCommand(this);

			User = new User();
		}

		public void SwitchViews()
		{
			isShowingRegister = !isShowingRegister;
			if (isShowingRegister)
			{
				RegisterVis = Visibility.Visible;
				LoginVis = Visibility.Collapsed;
			}
			else
			{
                RegisterVis = Visibility.Collapsed;
                LoginVis = Visibility.Visible;
            }
		}

		public void Login()
		{
			// TODO: Login
		}

		public void Register()
		{
			// TODO: Register
		}

		private void OnPropertyChanged(string propertyName)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

	}
}
