using System;
using System.Runtime.Serialization;

namespace DatabaseImporter.WPF.Infastructure.Services
{
    public class NavigationNotFoundException : Exception
    {

        public string TargetViewModel
        {
            get => Data[nameof(TargetViewModel)]?.ToString();
            set
            {
                if (Data.Contains(nameof(TargetViewModel)))
                    Data[nameof(TargetViewModel)] = value;
                else
                    Data.Add(nameof(TargetViewModel), value);
            }
        }

        public NavigationNotFoundException()
        {
        }

        public NavigationNotFoundException(Type targetViewModelType):this($"The target view model:{targetViewModelType?.Name}. Does not exist on map. ")
        {
            TargetViewModel = targetViewModelType?.FullName;
        }

        public NavigationNotFoundException(string message) : base(message)
        {
        }

        public NavigationNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected NavigationNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}