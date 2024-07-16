using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;

namespace MainWindow.Validation
{
    public class ValidationInput
    {
        public bool InputName(string name)
        {
            name = name.Trim();
            if(name == "")
            {
                return false;
            }
            return true;
        }

        public bool InputEmail(string email)
        {
            email = email.Trim();
            if (email == "")
            {
                Notification("Please enter email");
                return false;
            }
            else
            {
                var emailRegex = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
                Regex regexEmail = new Regex(emailRegex);
                if (!regexEmail.IsMatch(email))
                {
                    Notification("Email is not valid!");
                    return false;
                }
            }
            return true;
        }

        public bool InputPhone(string phone)
        {
            phone = phone.Trim();
            if (phone == "")
            {
                Notification("Please enter phone number");
                return false;
            }
            else
            {
                string phoneRegex = @"^0[1-9][0-9]{8,9}$";
                Regex regexPhone = new Regex(phoneRegex);
                if (!regexPhone.IsMatch(phone))
                {
                    Notification("Phone number is not valid!");
                    return false;
                }
            }
            return true;
        }

        public bool InputPassword(string password)
        {
            password = password.Trim();
            if (password == "")
            {
                Notification("Please enter password");
                return false;
            }
            else
            {
                if(password.Length < 4)
                {
                    Notification("Password must be at least 6 characters");
                    return false;
                }
            }
            return true;
        }

        public bool InputAddress(string address)
        {
            address = address.Trim();
            if (address == "")
            {
                Notification("Please enter address");
                return false;
            }
            return true;
        }

        public bool InputNumber(string number)
        {
            number = number.Trim();
            if (number == "")
            {
                Notification("Please enter number");
                return false;
            }
            else
            {
                if (!int.TryParse(number, out int result))
                {
                    Notification("Please enter a valid number");
                    return false;
                }
            }
            return true;
        }

        public bool InputDate(string date)
        {
            date = date.Trim();
            if (date == "")
            {
                Notification("Please enter date");
                return false;
            }
            else
            {
                DateTime selectedDate = DateTime.Parse(date);
                DateOnly onLyDate = DateOnly.FromDateTime(selectedDate);
                DateOnly currentDate = DateOnly.FromDateTime(DateTime.Now);
                if (onLyDate >= currentDate)
                {
                    Notification("Please select date before current date!");
                    return false;
                }

            }
            return true;
        }

        private void Notification(string message)
        {
            MessageBox.Show(message, "Notification", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
