using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaceReg.Model
{
    public class Participant : ObservableObject, INotifyPropertyChanged, IDataErrorInfo
    {
        public Dictionary<string, string> errors = new Dictionary<string, string>();

        public string Error => throw new NotImplementedException();
        public string this[string columnName] => errors.ContainsKey(columnName) ? errors[columnName] : null;

        public enum GenderType { Male, Female, Other };
        public IEnumerable<GenderType> GenderTypes
        {
            get
            {
                return Enum.GetValues(typeof(GenderType)).Cast<GenderType>().ToList<GenderType>();
            }
        }

        private int _id;
        private string _firstName;
        private string _lastName;
        private Affiliation _affiliation;
        private GenderType _gender;
        private DateTime _birthDate;
        
        public int Id
        {
            get
            {
                return _id;
            }
            set
            {
                Set(ref _id, value);
                setValid();
            }
        }

        public Affiliation Affiliation
        {
            get
            {
                return _affiliation;
            }
            set
            {
                Set(ref _affiliation, value);
                setValid();
            }
        }

        public String FirstName
        {
            get
            {
                return _firstName;
            }
            set
            {
                Set(ref _firstName, value);
                setValid();
            }
        }

        public String LastName
        {
            get
            {
                return _lastName;
            }
            set
            {
                Set(ref _lastName, value);
                setValid();
            }
        }

        public DateTime BirthDate
        {
            get
            {
                return _birthDate;
            }
            set
            {
                Set(ref _birthDate, value);
                setValid();
            }
        }

        public GenderType Gender
        {
            get
            {
                return _gender;
            }
            set
            {
                Set(ref _gender, value);
                setValid();
            }
        }

        //VALIDATION
        private bool isValid;
        public bool IsValid
        {
            get
            {
                return isValid;
            }
            set
            {
                isValid = value;
                RaisePropertyChanged(nameof(IsValid));
            }
        }

        private void setValid()
        {
            bool fN = ValidateFirstName();
            bool lN = ValidateLastName();
            bool aG = ValidateBirthDate();


            IsValid = fN && lN && aG;
        }

        private bool ValidateFirstName()
        {
            if (FirstName == null || FirstName.Equals(String.Empty) || FirstName.Any(Char.IsWhiteSpace))
            {
                errors[nameof(FirstName)] = "First name must contain no spaces, and cannot be empty.";
                return false;
            }
            else
            {
                errors[nameof(FirstName)] = null;
                return true;
            }
        }

        private bool ValidateLastName()
        {
            if (LastName == null || LastName.Equals(String.Empty) || LastName.Any(Char.IsWhiteSpace))
            {
                errors[nameof(LastName)] = "Last name must contain no spaces, and cannot be empty.";
                return false;
            }
            else
            {
                errors[nameof(LastName)] = null;
                return true;
            }
        }

        private bool ValidateBirthDate()
        {
            var age = DateTime.Today.Year - BirthDate.Year;
            if (age < 1 || age > 150)
            {
                errors[nameof(age)] = "Age must be between 1 and 150.";
                return false;
            }
            else
            {
                errors[nameof(age)] = null;
                return true;
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        public virtual void RaisePropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;
            if(handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
