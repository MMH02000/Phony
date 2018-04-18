﻿using MahApps.Metro.Controls.Dialogs;
using Phony.Kernel;
using Phony.Persistence;
using Phony.Utility;
using System;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Security;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Phony.ViewModel.Users
{
    public class LoginVM : CommonBase
    {
        static int _id;
        static string _name;
        static UserGroup _group;

        public int Id
        {
            get => _id;
            set
            {
                if (value != _id)
                {
                    _id = value;
                    RaisePropertyChanged(nameof(Id));
                }
            }
        }
        bool _isLogging;

        BackgroundWorker LoginBGW = new BackgroundWorker();

        public string Name
        {
            get => _name;
            set
            {
                if (value != _name)
                {
                    _name = value;
                    RaisePropertyChanged(nameof(Name));
                }
            }
        }

        public UserGroup Group
        {
            get => _group;
            set
            {
                if (value != _group)
                {
                    _group = value;
                    RaisePropertyChanged(nameof(Group));
                }
            }
        }

        public bool IsLogging
        {
            get => _isLogging;
            set
            {
                if (value != _isLogging)
                {
                    _isLogging = value;
                    RaisePropertyChanged(nameof(IsLogging));
                }
            }
        }

        public SecureString SecurePassword { private get; set; }

        public ICommand LogIn { get; set; }

        ViewModel.MainWindowVM v = new ViewModel.MainWindowVM();
        public LoginVM()
        {
            LoadCommands();
        }

        private void LoadCommands()
        {
            LogIn = new CustomCommand(DoLogIn, CanDoLogIn);
        }

        private async void DoLogIn(object obj)
        {
            IsLogging = true;
            var pass = Encryption.EncryptText(new NetworkCredential("", SecurePassword).Password);
            try
            {
                using (var db = new UnitOfWork(new PhonyDbContext()))
                {
                    Model.User u = db.Users.GetLoginCredentials(Name, pass);
                    if (u == null)
                    {
                        var LoginErrorMassage = Application.Current.Windows.OfType<View.MainWindow>().FirstOrDefault();
                        await LoginErrorMassage.ShowMessageAsync("خطا", "تاكد من اسم المستخدم او كلمة المرور و ان المستخدم نشط").ConfigureAwait(false);
                    }
                    else
                    {
                        Id = u.Id;
                        Name = u.Name;
                        Group = u.Group;
                        v.PageName = "Main";
                    }
                }
            }
            catch (Exception ex)
            {
                await Core.SaveExceptionAsync(ex).ConfigureAwait(false);
            }
        }

        private bool CanDoLogIn(object obj)
        {
            if (string.IsNullOrWhiteSpace(Name))
            {
                return false;
            }
            return true;
        }
    }
}