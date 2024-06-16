using System.ComponentModel;

namespace MauiAppTest.ViewModels
{
	public class Customer : INotifyPropertyChanged
	{
		long _id;
		public long Id
		{
			get => _id;
			set
			{
				if (_id == value)
					return;
				_id = value;
				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Id)));
			}
		}

		string _brand;
		public string FirstName
		{
			get => _brand;
			set
			{
				if (_brand == value)
					return;
				_brand = value;
				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(FirstName)));
			}
		}

		string _lastName;
		public string LastName
		{
			get => _lastName;
			set
			{
				if (_lastName == value)
					return;
				_lastName = value;
				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(LastName)));
			}
		}

		string _phoneNumber;
		public string PhoneNumber
		{
			get => _phoneNumber;
			set
			{
				if (_phoneNumber == value)
					return;
				_phoneNumber = value;
				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(PhoneNumber)));
			}
		}

		public event PropertyChangedEventHandler? PropertyChanged;
	}
}
