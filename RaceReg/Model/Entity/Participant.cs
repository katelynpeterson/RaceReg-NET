using GalaSoft.MvvmLight;
using RaceReg.Model.Value_Object;
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
        private Name _firstName;
        private Name _lastName;
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

        public Name FirstName
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

        public Name LastName
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
            bool aG = ValidateBirthDate();


            IsValid = aG;
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


        public override bool Equals(object obj)
        {
            var item = obj as Participant;

            if (item == null)
            {
                return false;
            }

            bool areEqual = true;

            if(this.Id != item.Id || 
                !this.FirstName.Equals(item.FirstName) ||
                !this.LastName.Equals(item.LastName) || 
                this.Gender != item.Gender ||
                !this.Affiliation.Equals(item.Affiliation) ||
                this.BirthDate.CompareTo(item.BirthDate) != 0)
            {
                areEqual = false;
            }

            return areEqual;
        }

    }
}
